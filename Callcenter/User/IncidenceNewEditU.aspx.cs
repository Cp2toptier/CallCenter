using CallCenter.Application;
using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Callcenter.User
{
    public partial class IncidenceNewEditU : System.Web.UI.Page
    {
        DBContext context = null;
        IncidenceManager incidenceManager = null;
        Guid id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCombo();
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
                //Con request pillamos los parametros de la url
                string idQueryString = Request.QueryString["Id"];
                if (idQueryString != null)
                {//Aqui es que hay ?Id (si no, no hace falta hacer nada)
                    if (Guid.TryParse(idQueryString, out id)) //Parse convierte idQueryString en Guid. Si lo hace, lo mete en la variable id
                    { //Aqui es que es Guid
                        Incidence incidence = incidenceManager.GetWithEquipment(id); //Devuelve de la base de datos el equipo con ID = id
                        if (incidence != null)
                        {//Existe en la BD, asi que ES EDIT
                            BtnNew.Visible = false;
                            LblNew.Visible = false;
                            BtnBackList.Visible = false;
                            BtnEdit.Visible = true;
                            lblEdit.Visible = true;
                            btnClose.Visible = true;
                            BtnBackView.Visible = true;
                            DropEquipment.SelectedValue = incidence.Equipment.ToString();
                            txtId.Text = id.ToString();
                            tablaIncidencia();
                            gridIncidence.Visible = true;
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

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            //Se crea directamente la incidencia, no hace falta introducir datos
            //Copia de incidence List, ver cual es valida y cual no
            try
            {
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
                MembershipUser user = Membership.GetUser();
                Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                Guid equip;
                Guid.TryParse(DropEquipment.SelectedValue, out equip);
                EquipmentManager eManager = new EquipmentManager(context);
                IncidenceStatus status = 0;
                IQueryable<Equipment> equipList = eManager.GetWithEquipmentType(equip);
                Incidence incidence = new Incidence()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = status,
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

        public void tablaIncidencia()
        {
            DataTable incidence = new DataTable();
            incidence.Columns.Add("Equipo");
            incidence.Columns.Add("Estado");
            incidence.Columns.Add("Fecha");
            incidence.Columns.Add("Fecha cierre");
            using (DBContext db = new DBContext("DefaultConnection"))
            {
                var consulta = from i in db.Incidences
                               where i.Id == id
                               select new { i.Id, i.Date, i.CloseDate, i.Status, i.Equipment };
                consulta.ToList().ForEach((n) =>
                {
                    DataRow row = incidence.NewRow();
                    row.SetField<String>("Equipo", n.Equipment.Description.ToString());
                    row.SetField<IncidenceStatus>("Estado", n.Status);
                    row.SetField<DateTime>("Fecha", n.Date);
                    if(n.CloseDate.Year==DateTime.MaxValue.Year)
                    {
                        row.SetField<String>("Fecha cierre", "-");
                    }
                    else
                    {
                        row.SetField<DateTime>("Fecha cierre", n.CloseDate);
                    }
                    incidence.Rows.Add(row);
                });
            }
            gridIncidence.DataSource = incidence;
            gridIncidence.DataBind();
        }

        protected void BtnBackList_Click(object sender, EventArgs e)
        {
            Response.Redirect("IncidenceListU.aspx");
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //Que haga algo cuando sepa coger el valor del dropdownlist y demas
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                string idQueryString = Request.QueryString["Id"];
                Guid.TryParse(idQueryString, out id);
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
                MembershipUser user = Membership.GetUser();
                Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                Incidence incidence = incidenceManager.GetWithEquipment(id);
                incidence.CloseDate = DateTime.Now;
                incidence.Status = IncidenceStatus.Cerrada;
                incidenceManager.Update(incidence);
                context.SaveChanges();
                Response.Redirect(Request.RawUrl, true);
            }
            catch (Exception ex)
            {
                //Guardar ex.Message en un log
                LblError.Text = Messages.txtError; //Messages es un archivo de recursos con datos, como el mensaje de error
            }
        }

        protected void BtnBackView_Click(object sender, EventArgs e)
        {
            String url = "IncidenceViewU.aspx?Id="+txtId.Text;
            Response.Redirect(url);
        }
    }
}