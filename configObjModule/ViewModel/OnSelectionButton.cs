using System;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Autodesk.Revit.UI.Selection;
using configObjModule.View;

namespace configObjModule.ViewModel
{
      class OnSelectionButton : IExternalEventHandler
      {
            public void Execute(UIApplication uiapp)
            {
                  Document doc = uiapp.ActiveUIDocument.Document;
                  Application app = uiapp.Application;
                  UIDocument uidoc = uiapp.ActiveUIDocument;

                  Selection choices = uidoc.Selection;

                  IList<Element> pickedElements = uidoc.Selection.PickElementsByRectangle("Select by rectangle");
                  if (pickedElements.Count > 0)
                  {
                        // Collect Ids of all picked elements
                        IList<ElementId> idsToSelect = new List<ElementId>(pickedElements.Count);
                        foreach (Element element in pickedElements)
                        {
                              idsToSelect.Add(element.Id);
                        }

                        // Update the current selection
                        uidoc.Selection.SetElementIds(idsToSelect);
                        TaskDialog.Show("Revit", string.Format("{0} elements added to Selection.", idsToSelect.Count));
                        ModelessForm ModelessForm = new ModelessForm();
                        ModelessForm.Show();
                  }
            }

            public string GetName()
            {
                  return "xyz Event";
            }

      }
}



