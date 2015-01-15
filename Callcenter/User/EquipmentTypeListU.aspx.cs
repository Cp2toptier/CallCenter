using Callcenter.Web;
using CallCenter.Application;
using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Callcenter.User
{
    public partial class EquipmentTypeListU : System.Web.UI.Page
    {
        DBContext context = null;
        EquipmentTypeManager equipmentTypeManager = null;
        Guid id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                context = new DBContext("DefaultConnection");
                equipmentTypeManager = new EquipmentTypeManager(context);
                //Con request pillamos los parametros de la url
                string idQueryString = Request.QueryString["Id"];
                if (idQueryString != null)
                {//Aqui es que hay ?Id (si no, no hace falta hacer nada)
                    if (Guid.TryParse(idQueryString, out id)) //Parse convierte idQueryString en Guid. Si lo hace, lo mete en la variable id
                    { //Aqui es que es Guid
                        EquipmentType eq = equipmentTypeManager.Get(id); //Devuelve de la base de datos el tipo de equipo con ID = id
                        if (eq != null)
                        {//Existe en la BD
                            try
                            {
                                equipmentTypeManager.Remove(eq);
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
        public static JsonDataTable GetEquipmentTypes()
        {
            try
            {
                //Creamos el contexto de datos y el servicio
                DBContext dbcontext = new DBContext("DefaultConnection");
                EquipmentTypeManager etManager = new EquipmentTypeManager(dbcontext);

                //Cogemos la información del usuario actual, en userId cogeremos su id o un guid vacio si no esta logueado
                MembershipUser user = Membership.GetUser();
                Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                LblError.Text = userId.ToString();

                //IEnumerable<Incidence> list = incidenceService.GetIncidentByUser((Guid)Membership.GetUser().ProviderUserKey);
                IQueryable<EquipmentType> list = etManager.GetAll();
                return Utils.CreateJsDataTable(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void eliminar(Guid id){
            DBContext context = null;
            EquipmentTypeManager equipmentTypeManager = null;
            try
            {
                context = new DBContext("DefaultConnection");
                equipmentTypeManager = new EquipmentTypeManager(context);
                EquipmentType equipmentType = new EquipmentType()
                {
                    Id = id
                };
                equipmentTypeManager.Remove(equipmentType);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("EquipmentTypeNewEditU.aspx");
        }
    }
}
