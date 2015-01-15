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
    public partial class IncidenceListU : System.Web.UI.Page
    {
        DBContext context = null;
        IncidenceManager incidenceManager = null;
        Guid id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCombo();
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JsonDataTable GetIncidencesU()
        {
            try
            {
                //Creamos el contexto de datos y el servicio
                DBContext dbcontext = new DBContext("DefaultConnection");
                IncidenceManager inManager = new IncidenceManager(dbcontext);

                //Cogemos la información del usuario actual, en userId cogeremos su id o un guid vacio si no esta logueado
                MembershipUser user = Membership.GetUser();
                Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;

                IEnumerable<Incidence> list = inManager.GetByUserId((Guid)Membership.GetUser().ProviderUserKey);
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
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Equipo", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Estado", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Fecha", Class = "String" });
                jsDT.add_Column(new JsonDataTable.JsDataColumn() { Title = "Fecha de Cierre", Class = "String" });
                for (var i=0; i<c; i++)
                { //Crea una fila por cada elemento de la lista
                    List<object> vl = new List<object>();
                    Incidence incidence = lista.ElementAt(i);
                    vl.Add(incidence.Id);
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

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            //Se crea directamente la incidencia, no hace falta introducir datos
            try
            {
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
                MembershipUser user = Membership.GetUser();
                Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                Guid equip;
                Guid.TryParse(DropEquipment.SelectedValue, out equip);
                EquipmentManager eManager = new EquipmentManager(context);
                IQueryable<Equipment> equipList = eManager.GetWithEquipmentType(equip);
                Incidence incidence = new Incidence()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = IncidenceStatus.Abierta,
                    Date = DateTime.Now,
                    CloseDate = DateTime.MaxValue,
                    //Messages = listMes,
                    Equipment = equipList.First()
                };
                incidenceManager.Add(incidence);
                context.SaveChanges();
                Response.Redirect(Request.RawUrl, true);
            }
            catch (Exception ex)
            {
                //Guardar ex.Message en un log
                LblError.Text = Messages.txtError; //Messages es un archivo de recursos con datos, como el mensaje de error
            }
        }

        /// <summary>
        /// Carga el combo de equipos
        /// </summary>
        private void CargarCombo()
        {
            DataTable equipment = new DataTable();
            MembershipUser user = Membership.GetUser();
            equipment.Columns.Add("Id");
            equipment.Columns.Add("Descripcion");
            using (DBContext db = new DBContext("DefaultConnection"))
            {
                Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                var consulta = from i in db.Equipments
                               where i.UserId == userId
                               select new { i.Id, i.Description };
                consulta.ToList().ForEach((n) =>
                {
                    DataRow row = equipment.NewRow();
                    row.SetField<Guid>("Id", n.Id);
                    row.SetField<String>("Descripcion", n.Description);
                    equipment.Rows.Add(row);
                });
            }
            DropEquipment.DataTextField = "Descripcion";
            DropEquipment.DataValueField = "Id";
            DropEquipment.DataSource = equipment;
            DropEquipment.DataBind();
        }
    }
}