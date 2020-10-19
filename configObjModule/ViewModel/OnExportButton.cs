using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;
using configObjModule.Model;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace configObjModule.ViewModel
{
      class OnExportButton : IExternalEventHandler
      {

            /// <summary>
            /// Default colour: grey.
            /// </summary>
            public static Color DefaultColor
              = new Color(127, 127, 127);

            /// <summary>
            /// Default transparency: opaque.
            /// </summary>
            public static int Opaque = 0;

            void InfoMsg(string msg)
            {
                  TaskDialog.Show("OBJ Exporter", msg);
            }

            /// <summary>
            /// Define the schedule export folder.
            /// All existing files will be overwritten.
            /// </summary>
            public static string _export_folder_name = null;
            //public string filename = null;

            /// <summary>
            /// Select an OBJ output file in the given folder.
            /// </summary>
            /// <param name="folder">Initial folder.</param>
            /// <param name="filename">Selected filename on success.</param>
            /// <returns>Return true if a file was successfully selected.</returns>
            static bool FileSelect(
              string folder,
              out string filename)
            {
                  SaveFileDialog dlg = new SaveFileDialog();
                  dlg.Title = "Select OBJ Output File";
                  dlg.CheckFileExists = false;
                  dlg.CheckPathExists = true;
                  //dlg.RestoreDirectory = true;
                  dlg.InitialDirectory = folder;
                  dlg.Filter = "OBJ Files (*.obj)|*.obj|All Files (*.*)|*.*";
                  bool rc = (DialogResult.OK == dlg.ShowDialog());
                  filename = dlg.FileName;
                  return rc;
            }
            
            /// <summary>
            /// Export a non-empty solid.
            /// </summary>
            bool ExportSolid(
              IJtFaceEmitter emitter,
              Document doc,
              Solid solid,
              Color color,
              int transparency)
            {
                  Material m;
                  Color c;
                  int t;

                  foreach (Face face in solid.Faces)
                  {
                        m = doc.GetElement(
                          face.MaterialElementId) as Material;

                        c = (null == m) ? color : m.Color;

                        t = (null == m)
                          ? transparency
                          : m.Transparency;

                        emitter.EmitFace(face,
                          (null == c) ? DefaultColor : c,
                          t);
                  }
                  return true;
            }

            /// <summary>
            /// Export all non-empty solids found for 
            /// the given element. Family instances may have 
            /// their own non-empty solids, in which case 
            /// those are used, otherwise the symbol geometry.
            /// The symbol geometry could keep track of the 
            /// instance transform to map it to the actual 
            /// project location. Instead, we ask for 
            /// transformed geometry to be returned, so the 
            /// resulting solids are already in place.
            /// </summary>
            int ExportSolids(
              IJtFaceEmitter emitter,
              Element e,
              Options opt,
              Color color,
              int transparency)
            {
                  int nSolids = 0;

                  GeometryElement geo = e.get_Geometry(opt);

                  Solid solid;

                  if (null != geo)
                  {
                        Document doc = e.Document;

                        if (e is FamilyInstance)
                        {
                              geo = geo.GetTransformed(
                                Transform.Identity);
                        }

                        GeometryInstance inst = null;
                        //Transform t = Transform.Identity;

                        // Some columns have no solids, and we have to
                        // retrieve the geometry from the symbol; 
                        // others do have solids on the instance itself
                        // and no contents in the instance geometry 

                        foreach (GeometryObject obj in geo)
                        {
                              solid = obj as Solid;

                              if (null != solid
                                && 0 < solid.Faces.Size
                                && ExportSolid(emitter, doc, solid,
                                  color, transparency))
                              {
                                    ++nSolids;
                              }

                              inst = obj as GeometryInstance;
                        }

                        if (0 == nSolids && null != inst)
                        {
                              geo = inst.GetSymbolGeometry();
                              //t = inst.Transform;

                              foreach (GeometryObject obj in geo)
                              {
                                    solid = obj as Solid;

                                    if (null != solid
                                      && 0 < solid.Faces.Size
                                      && ExportSolid(emitter, doc, solid,
                                        color, transparency))
                                    {
                                          ++nSolids;
                                    }
                              }
                        }
                  }
                  return nSolids;
            }

            /// <summary>
            /// Return a string describing the given element:
            /// .NET type name, category name, family and 
            /// symbol name for a family instance, element id 
            /// and element name.
            /// </summary>
            public static string ElementDescription(Element e)
            {
                  if (null == e)
                  {
                        return "<null>";
                  }

                  // For a wall, the element name equals the
                  // wall type name, which is equivalent to the
                  // family name ...

                  FamilyInstance fi = e as FamilyInstance;

                  string typeName = e.GetType().Name;

                  string categoryName = (null == e.Category)
                    ? string.Empty
                    : e.Category.Name + " ";

                  string familyName = (null == fi)
                    ? string.Empty
                    : fi.Symbol.Family.Name + " ";

                  string symbolName = (null == fi
                    || e.Name.Equals(fi.Symbol.Name))
                      ? string.Empty
                      : fi.Symbol.Name + " ";

                  return string.Format("{0} {1}{2}{3}<{4} {5}>",
                    typeName, categoryName, familyName, symbolName,
                    e.Id.IntegerValue, e.Name);
            }

            /// <summary>
            /// Return an English plural suffix 's' or 
            /// nothing for the given number of items.
            /// </summary>
            public static string PluralSuffix(int n)
            {
                  return 1 == n ? "" : "s";
            }

            /// <summary>
            /// Export an element, i.e. all non-empty solids
            /// encountered, and return the number of elements 
            /// exported.
            /// If the element is a group, this method is 
            /// called recursively on the group members.
            /// </summary>
            int ExportElement(
              IJtFaceEmitter emitter,
              Element e,
              Options opt,
              ref int nSolids)
            {
                  Group group = e as Group;

                  if (null != group)
                  {
                        int n = 0;

                        foreach (ElementId id
                          in group.GetMemberIds())
                        {
                              Element e2 = e.Document.GetElement(
                                id);

                              n += ExportElement(emitter, e2, opt, ref nSolids);
                        }
                        return n;
                  }

                  string desc = ElementDescription(e);

                  Category cat = e.Category;

                  if (null == cat)
                  {
                        Debug.Print("Element '{0}' has no "
                          + "category.", desc);

                        return 0;
                  }

                  Material material = cat.Material;

                  // Column category has no material, maybe all
                  // family instances have no defualt material,
                  // so we cannot simply skip them here:

                  //if( null == material )
                  //{
                  //  Debug.Print( "Category '{0}' of element '{1}' "
                  //    + "has no material.", cat.Name, desc );

                  //  return 0;
                  //}

                  Color color = (null == material)
                    ? null
                    : material.Color;

                  int transparency = (null == material)
                    ? 0
                    : material.Transparency;

                  //Debug.Assert( null != color,
                  //  "expected a valid category material colour" );

                  nSolids += ExportSolids(emitter, e, opt, color, transparency);

                  return 1;
            }


            void ExportElements(
              IJtFaceEmitter emitter,
              FilteredElementCollector collector,
              Options opt)
            {
                  int nElements = 0;
                  int nSolids = 0;

                  foreach (Element e in collector)
                  {
                        nElements += ExportElement(
                          emitter, e, opt, ref nSolids);
                  }

                  int nFaces = emitter.GetFaceCount();
                  int nTriangles = emitter.GetTriangleCount();
                  int nVertices = emitter.GetVertexCount();

                  string msg = string.Format(
                    "{0} element{1} with {2} solid{3}, "
                    + "{4} face{5}, {6} triangle{7} and "
                    + "{8} vertice{9} exported.",
                    nElements, PluralSuffix(nElements),
                    nSolids, PluralSuffix(nSolids),
                    nFaces, PluralSuffix(nFaces),
                    nTriangles, PluralSuffix(nTriangles),
                    nVertices, PluralSuffix(nVertices));

                  InfoMsg(msg);
            }


            public void Execute(UIApplication uiapp)
            {

                  Document doc = uiapp.ActiveUIDocument.Document;
                  Application app = uiapp.Application;
                  UIDocument uidoc = uiapp.ActiveUIDocument;

                  
                  // Determine elements to export

                  FilteredElementCollector collector = null;

                  // Access current selection

                  ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

                  if (0 < selectedIds.Count)

                  {
                        // If any elements were preselected,
                        // export those to OBJ

                        collector = new FilteredElementCollector(doc, selectedIds);
                  }
                  else
                  {
                        // If nothing was preselected, export 
                        // all model elements to OBJ

                        collector = new FilteredElementCollector(doc);
                  }

                  collector.WhereElementIsNotElementType()
                      .WhereElementIsViewIndependent();
                  
                  if (null == _export_folder_name)
                  {
                        _export_folder_name = Path.GetTempPath();
                  }
                  
                  string filename = null;

                  
                  if (!FileSelect(_export_folder_name,
                    out filename))
                  {
                        return;
                  }

                  _export_folder_name
                    = Path.GetDirectoryName(filename);

                  ObjExporter exporter = new ObjExporter();

                  Options opt = app.Create.NewGeometryOptions();

                  ExportElements(exporter, collector, opt);

                  exporter.ExportTo(filename);

                  return;
            }
            public string GetName()
            {
                  return "xyz Event";
            }
      }
}
