using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Callcenter.Web
{
    public class JsonDataTable
    {

        public List<List<object>> aaData;
        public List<JsDataColumn> aoColumns;

        public JsonDataTable()
        {

            aaData = new List<List<object>>();
            aoColumns = new List<JsDataColumn>();
        }

        public void add_Row(List<object> cells)
        {
            this.aaData.Add(cells);
        }

        public class JsDataColumn
        {
            public string Title { get; set; }
            public string Class { get; set; }
        }

        public void add_Column(JsDataColumn col)
        {
            this.aoColumns.Add(col);
        }
    }
}