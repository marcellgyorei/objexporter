# ObjExporter
Exports metadata from Revit to obj by selection.

Supported Revit versions: 2018.0, 2019.2  
Prerequisites: .NET Framework 4.6.2.  
Windows Installer (.msi) file is attached.  

To test the source code with Revit 2018:

(1) Copy configObjModule project folder into C:\ProgramData\Autodesk\Revit\Macros\2018\Revit\AppHookup  
(2) Copy RevitAPI.dll and RevitAPIUI.dll to C:\ProgramData\Autodesk\Revit\Macros\2018  
(3) Copy configObjModule.addin manifest file to C:\USER_PROFILE\AppData\Roaming\Autodesk\Revit\Addins\2018  
(4) Run Revit 2018  
(5) Open the solution file (C:\ProgramData\Autodesk\Revit\Macros\2018\Revit\AppHookup\configObjModule\Source\configObjModule.sln)  
(6) Attach Revit.exe (Debug>Attach to Process>Revit.exe)  

You can open the obj file using reality converter application:

![alt text](https://github.com/marcellgyorei/ObjExporter/blob/master/usd.bmp?raw=true)
