using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Callcenter.Web
{
    public static class Utils
    {
        public static JsonDataTable CreateJsDataTable(IEnumerable<object> list)
        {
            JsonDataTable jsDT = new JsonDataTable();

            var fistElement = list.FirstOrDefault();
            if (fistElement != null)
            { //Hay primer elemento
                System.Reflection.PropertyInfo[] prps = fistElement.GetType().GetProperties();
                foreach (var property in prps)
                { //Crea una columna por cada campo de los elemntos de la lista (cambiar para personalizar)
                    jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = property.Name, Class = property.PropertyType.Name });
                }
                foreach (var element in list)
                { //Crea una fila por cada elemento de la lista
                    List<object> vl = new List<object>();
                    foreach (var property in prps)
                    { // Coge los valores del elemento para rellenar datos (cambiar para personalizar)
                        vl.Add(property.GetValue(element, null));
                    }
                    jsDT.add_Row(vl);
                }
            }
            return jsDT;
        }
    }    
}