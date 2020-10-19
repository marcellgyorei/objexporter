/*
 * Created by SharpDevelop.
 * User: Marcell
 * Date: 04/03/2020
 * Time: 11:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

#region Template Overview

/*ConfigObjRibbon.dll->ThisApplication.cs->InvokeCommand.cs->ConfigObjModule.dll
 *ConfigObjModule.dll->ThisApplication.cs->ModelessForm.cs(try-catch)->OnExportButton.cs->ObjExporter.cs
                                                                     ->OnSelectionButton.cs*/
#endregion

using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using configObjModule.View;

namespace configObjModule
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("2EFED30-FEAC-47E0-B7F7-F238E563512B")]

      public partial class Command : IExternalCommand
	{
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
            {
                  taskDialog(commandData.Application.ActiveUIDocument);
                  return Result.Succeeded;
            }
            private void Module_Startup(object sender, EventArgs e)
		{

		}

		private void Module_Shutdown(object sender, EventArgs e)
		{

		}

		#region Revit Macros generated code
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(Module_Startup);
			this.Shutdown += new System.EventHandler(Module_Shutdown);
		}
            #endregion

            public void taskDialog(UIDocument uidoc)
            {
                  ModelessForm ModelessForm = new ModelessForm();
                  ModelessForm.Show();

                  return;
            }
      }
}