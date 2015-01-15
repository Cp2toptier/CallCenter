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

namespace Callcenter.User
{
    public partial class EquipmentListU : System.Web.UI.Page
    {
        DBContext context = null;
        EquipmentManager equipmentManager = null;
        Guid id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                context = new DBContext("DefaultConnection");
                equipmentManager = new EquipmentManager(context);
                //Con request pillamos los parametros de la url
                string idQueryString = Request.QueryString["Id"];
                if (idQueryString != null)
                {//Aqui es que hay ?Id (si no, no hace falta hacer nada)
                    if (Guid.TryParse(idQueryString, out id)) //Parse convierte idQueryString en Guid. Si lo hace, lo mete en la variable id
                    { //Aqui es que es Guid
                        Equipment eq = equipmentManager.Get(id); //Devuelve de la base de datos el equipo con ID = id
                        if (eq != null)
                        {//Existe en la BD
                            try
                            {
                                equipmentManager.Remove(eq);
                                context.SaveChanges();
                            }
                            catch (Exception ex) {}
                        }
                        else
                        {//No existe en la BD
                            //Error
                            LblError.Text = Messages.txtError;
                        }
                    }
                    else
                    { //Aqui es que no es Guid
                        //Error
                        LblError.Text = Messages.txtError;
                    }
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JsonDataTable GetEquipments()
        {
            try
            {
                //Creamos el contexto de datos y el servicio
                DBContext dbcontext = new DBContext("DefaultConnection");
                EquipmentManager eManager = new EquipmentManager(dbcontext);

                //Cogemos la información del usuario actual, en userId cogeremos su id o un guid vacio si no esta logueado
                MembershipUser user = Membership.GetUser();
                Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;

                //IEnumerable<Incidence> list = incidenceService.GetIncidentByUser((Guid)Membership.GetUser().ProviderUserKey);
                IQueryable<Equipment> list = eManager.GetByUserId((Guid)Membership.GetUser().ProviderUserKey);

               
                return EquipmentJsDataTable(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static JsonDataTable EquipmentJsDataTable(IEnumerable<Equipment> list)
        {
            JsonDataTable jsDT = new JsonDataTable();
            List<Equipment> lista = list.ToList<Equipment>();
            var fistElement = lista.FirstOrDefault();
            if (fistElement != null)
            { //Hay primer elemento
                var c = lista.Count();
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Id", Class = "Guid" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Descripcion", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Tipo de equipo", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Fecha compra", Class = "String" });
                for (var i = 0; i < c; i++)
                { //Crea una fila por cada elemento de la lista
                    List<object> vl = new List<object>();
                    Equipment equipment = lista.ElementAt(i);
                    vl.Add(equipment.Id);
                    vl.Add(equipment.Description);
                    vl.Add(equipment.EquipmentType.Type);
                    vl.Add(equipment.PurchaseDate.Date.ToString());
                    jsDT.add_Row(vl);
                }
            }
            return jsDT;
        }

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("EquipmentNewEditU.aspx");
        }
    }
}