using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace configObjModule.ViewModel
{
      class RibbonInterface
      {
            public void CreateRibbonPanel(UIControlledApplication application)
            {
                  #region Tab, Panel
                  //Initialize tab name
                  string tabName = "Exporter";
                  //Initialize panel name
                  string panelName = "OBJExportPanel";
                  //Initialize assembly path
                  string path = Assembly.GetExecutingAssembly().Location;
                  String assemblyPath = Path.GetDirectoryName(path) + "\\configObjModule.dll";
                  //Instantiate the CreateRibbonTab method
                  application.CreateRibbonTab(tabName);
                  //Instantiate the CreateRibbonPanel method to create a panel instance
                  var firstPanel = application.CreateRibbonPanel(tabName, panelName);
                  #endregion

                  #region User Interface
                  //Instantiate the PushButtonData class (and send it to the memory) to create a button instance
                  //Set object initializers to add extra properties to avoid messy overloading
                  PushButtonData firstButtonData = new PushButtonData("firstButtonData", "Export", assemblyPath, "configObjModule.Command");
                  {
                        //Object initializers
                  };
                  //Assign the instantiated AddItem method to the PushButton class to add the button to the panel
                  PushButton firstButton = firstPanel.AddItem(firstButtonData) as PushButton;
                  //Assign the BitmapImage instantiated class to the LargeImage property to add new icon to the button
                  firstButton.LargeImage = new BitmapImage(new Uri(Path.Combine(Path.GetDirectoryName(path) + "\\Icon_0309.ico"), UriKind.Absolute));
                  #endregion

            }
      }
}
