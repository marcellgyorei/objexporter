using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using configObjModule.ViewModel;

namespace configObjModule.View
{
      public partial class ModelessForm : System.Windows.Forms.Form
      {

            //Declare EventHandlers
            OnExportButton OnExportButtonParameter;
            ExternalEvent ExportActionParameter;


            OnSelectionButton OnSelectionButtonParameter;
            ExternalEvent SelectionActionParameter;

            public ModelessForm()
            {
                  InitializeComponent();
                  //Initialize EventHandlers

                  OnExportButtonParameter = new OnExportButton();
                  ExportActionParameter = ExternalEvent.Create(OnExportButtonParameter);


                  OnSelectionButtonParameter = new OnSelectionButton();
                  SelectionActionParameter = ExternalEvent.Create(OnSelectionButtonParameter);

                  //
            }

            private void ModelessForm_Load(object sender, EventArgs e)
            {

            }

            private void Cancel_Click(object sender, EventArgs e)
            {
                  Close();
            }

            private void Select_Click_1(object sender, EventArgs e)
            {
                  
                  SelectionActionParameter.Raise();
                  Hide();
            }

            private void Export_Click(object sender, EventArgs e)
            {
                  try
                  {
                        ExportActionParameter.Raise();
                  }

                  #region catch and finally
                  catch (Exception ex)
                  {
                        TaskDialog.Show("Catch", "Failed due to:" + Environment.NewLine + ex.Message);
                  }
                  finally
                  {

                  }
                  #endregion 
                  Close();
            }
      }

}