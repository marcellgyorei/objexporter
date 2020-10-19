/*
 * Created by SharpDevelop
 * User: Marcell
 * Date: 04/03/2020
 * Time: 11:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;
using configObjModule.ViewModel;
using Autodesk.Revit.ApplicationServices;

namespace configObjModule
{
      [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
      [Autodesk.Revit.DB.Macros.AddInId("71FD1339-2B98-464B-8E78-41109F5A7DB4")]
      public partial class ThisApplication : IExternalApplication
      {

            public Autodesk.Revit.UI.Result OnStartup(UIControlledApplication application)
            {
                  //additional layer to prevent the app to crash Revit
                  try
                  {
                        //Instantiate the InterfaceSetup class
                        RibbonInterface ui = new RibbonInterface();
                        //Instantiate the CreateSampleRibbonTab method
                        ui.CreateRibbonPanel(application);

                        return Autodesk.Revit.UI.Result.Succeeded;
                  }
                  catch (Exception ex)
                  {
                        TaskDialog.Show("configObjModule", ex.ToString());

                        return Autodesk.Revit.UI.Result.Failed;
                  }
            }
            public Autodesk.Revit.UI.Result OnShutdown(UIControlledApplication application)
            {
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
      }
}