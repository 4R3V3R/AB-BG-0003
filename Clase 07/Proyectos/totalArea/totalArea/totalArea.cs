using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NameSpaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace totalArea
{
    [Transaction(TransactionMode.Manual)]
    public class totalArea : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
                              ref string message,
                              ElementSet elements)
        {
            /*Description: 
                - Aplicativo que permita seleccionar elementos y obtener la suma total de areas.
                    1. SELECCIONAR LOS ELEMENTOS
                    2. FILTRAR ELEMENTOS (WALLS)
                    3. OBTENER LA INFORMACION DE DE LOS MUROS
                    4. SUMAR LAS AREAS
                    5. MOSTRAR POR MENSAJE ESA SUMATORIA
             */

            //1.SELECCIONAR LOS ELEMENTOS

            //A. DECLARAR VARIABLES QUE ME PERMITAN OBTNER EL DOCUMENTO ACTUAL DONDE ESTOY TRABAJANDO
            UIApplication uiApp = commandData.Application;
            UIDocument uidDoc = uiApp.ActiveUIDocument;
            Document doc = uidDoc.Document;

            IList<Reference> pickedObjs;
            pickedObjs = uidDoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);
            List<ElementId> ids = (from Reference r in pickedObjs select r.ElementId).ToList();

            //2.FILTRAR ELEMENTOS(WALLS)
            List<Element> walls = new List<Element>();
            foreach (ElementId id in ids)
            {
                Element el = doc.GetElement(id);

                if (el.Category.Name == "Walls")
                {
                    walls.Add(el);
                }
            }

            //3.OBTENER LA INFORMACION DE DE LOS MUROS
            //4.SUMAR LAS AREAS

            double areaWalls = 0;
            foreach (Element el in walls)
            {
                areaWalls = areaWalls + el.LookupParameter("Area").AsDouble() * 0.092903;
            }

            TaskDialog.Show("Cantidad de Elementos Seleccionados",
                                "Cantidad de Elementos: " + ids.Count.ToString() +
                                "\nCantidad de Muros: " + walls.Count.ToString() + "\n" + "\n" +
                                "Area de Muros = " + Math.Round(areaWalls,2).ToString() + " m2");

            return Result.Succeeded;
        }
    }
}
