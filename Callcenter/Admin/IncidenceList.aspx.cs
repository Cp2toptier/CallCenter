using Callcenter.Web;
using CallCenter.Application;
using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Callcenter.Admin
{
    public partial class IncidenceList : System.Web.UI.Page
    {
        DBContext context = null;
        IncidenceManager incidenceManager = null;
        Guid id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JsonDataTable GetIncidences()
        {
            try
            {
                //Creamos el contexto de datos y el servicio
                DBContext dbcontext = new DBContext("DefaultConnection");
                IncidenceManager inManager = new IncidenceManager(dbcontext);

                IEnumerable<Incidence> list = inManager.GetAll();
                return IncidenceJsDataTable(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static JsonDataTable IncidenceJsDataTable(IEnumerable<Incidence> list)
        {
            JsonDataTable jsDT = new JsonDataTable();
            List<Incidence> lista = list.ToList<Incidence>();
            var fistElement = lista.FirstOrDefault();
            if (fistElement != null)
            { //Hay primer elemento
                //System.Reflection.PropertyInfo[] prps = fistElement.GetType().GetProperties();
                var c = lista.Count();
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Id", Class = "Guid" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Usuario", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Equipo", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Estado", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Fecha", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Fecha de Cierre", Class = "String" });
                for (var i = 0; i < c; i++)
                { //Crea una fila por cada elemento de la lista
                    List<object> vl = new List<object>();
                    Incidence incidence = lista.ElementAt(i);
                    vl.Add(incidence.Id);
                    MembershipUser user = Membership.GetUser(incidence.UserId);
                    vl.Add(user.UserName);
                    vl.Add(incidence.Equipment.Description);
                    if (incidence.Status == IncidenceStatus.Cerrada)
                    {
                        vl.Add("Cerrada");
                    }
                    else
                    {
                        vl.Add("Abierta");
                    }
                    vl.Add(incidence.Date.ToString());
                    if (incidence.CloseDate.Year == DateTime.MaxValue.Year)
                    {
                        vl.Add(" - ");
                    }
                    else
                    {
                        vl.Add(incidence.CloseDate.ToString());
                    }
                    jsDT.add_Row(vl);
                }
            }
            return jsDT;
        }
    }
}