using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NameSpaces Revit
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace cmd_helloWorld
{
    [Transaction(TransactionMode.ReadOnly)]
    public class cmd_helloWorld : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, 
                              ref string message, 
                              ElementSet elements)
        {
            TaskDialog.Show("Mi primer mensaje", "Este es mi primer hola mundo con la API de Revit");
            return Result.Succeeded;
        }
    }
}
