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

namespace Callcenter.Admin
{
    public partial class IncidenceView : System.Web.UI.Page
    {
        DBContext context = null;
        IncidenceManager incidenceManager = null;
        Guid id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
                //Con request pillamos los parametros de la url
                string idQueryString = Request.QueryString["Id"];
                if (idQueryString != null)
                {//Aqui es que hay ?Id (si no, no hace falta hacer nada)
                    if (Guid.TryParse(idQueryString, out id)) //Parse convierte idQueryString en Guid. Si lo hace, lo mete en la variable id
                    { //Aqui es que es Guid
                        Incidence incidence = incidenceManager.Get(id); //Devuelve de la base de datos el incidence con ID = id
                        if (incidence != null)
                        {//Existe en la BD
                            //Tenemos la incidencia
                            tablaIncidencia();
                            datatableIncidenceMesages(id);
                            txtId.Text = id.ToString();
                            if (incidence.Status == IncidenceStatus.Cerrada)
                            {
                                LblMessage.Visible = false;
                                LblTitle.Visible = false;
                                LblText.Visible = false;
                                txtTitle.Visible = false;
                                areaText.Visible = false;
                                BtnSend.Visible = false;
                                BtnSendIntern.Visible = false;
                                btnClose.Visible = false;
                                BtnReabrir.Visible = true;
                            }
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

        public void tablaIncidencia()
        {
            DataTable incidence = new DataTable();
            incidence.Columns.Add("Usuario");
            incidence.Columns.Add("Equipo");
            incidence.Columns.Add("Estado");
            incidence.Columns.Add("Fecha");
            incidence.Columns.Add("Fecha cierre");
            using (DBContext db = new DBContext("DefaultConnection"))
            {
                var consulta = from i in db.Incidences
                               where i.Id == id
                               select new { i.Id,i.UserId, i.Date, i.CloseDate, i.Status, i.Equipment };
                consulta.ToList().ForEach((n) =>
                {
                    DataRow row = incidence.NewRow();
                    MembershipUser user = Membership.GetUser(n.UserId);
                    row.SetField<String>("Usuario", user.UserName);
                    row.SetField<String>("Equipo", n.Equipment.Description.ToString());
                    row.SetField<IncidenceStatus>("Estado", n.Status);
                    row.SetField<DateTime>("Fecha", n.Date);
                    if (n.CloseDate.Year == DateTime.MaxValue.Year)
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
            IncidenceData.DataSource = incidence;
            IncidenceData.DataBind();
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            //Reset de label
            LblError.Text = "";
            //Validacion
            if (txtTitle.Text == "" || areaText.InnerText == "")
            {
                LblError.Text = "Rellena los campos";
            }
            else
            {
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
                //Con request pillamos los parametros de la url
                string idQueryString = Request.QueryString["Id"];
                if (idQueryString != null)
                {//Aqui es que hay ?Id (si no, no hace falta hacer nada)
                    if (Guid.TryParse(idQueryString, out id)) //Parse convierte idQueryString en Guid. Si lo hace, lo mete en la variable id
                    { //Aqui es que es Guid
                        Incidence incidence = incidenceManager.Get(id); //Devuelve de la base de datos el equipo con ID = id
                        if (incidence != null)
                        {//Existe en la BD
                            IncidenceManager iManager = new IncidenceManager(context);
                            MembershipUser user = Membership.GetUser();
                            Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                            Message message = new Message()
                            {
                                Id = Guid.NewGuid(),
                                Title = txtTitle.Text,
                                UserId = userId,
                                Text = areaText.InnerText,
                                Date = DateTime.Now,
                                Incidence = iManager.Get(id)
                            };
                            MessageManager messageManager = new MessageManager(context);
                            messageManager.Add(message);
                            context.SaveChanges();
                            txtTitle.Text = "";
                            areaText.InnerText = "";
                            Response.Redirect(Request.RawUrl, true);
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

        protected void BtnSendIntern_Click(object sender, EventArgs e)
        {
            //Reset de label
            LblError.Text = "";
            //Validacion
            if (txtTitle.Text == "" || areaText.InnerText == "")
            {
                LblError.Text = "Rellena los campos";
            }
            else
            {
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
                //Con request pillamos los parametros de la url
                string idQueryString = Request.QueryString["Id"];
                if (idQueryString != null)
                {//Aqui es que hay ?Id (si no, no hace falta hacer nada)
                    if (Guid.TryParse(idQueryString, out id)) //Parse convierte idQueryString en Guid. Si lo hace, lo mete en la variable id
                    { //Aqui es que es Guid
                        Incidence incidence = incidenceManager.Get(id); //Devuelve de la base de datos el equipo con ID = id
                        if (incidence != null)
                        {//Existe en la BD
                            IncidenceManager iManager = new IncidenceManager(context);
                            MembershipUser user = Membership.GetUser();
                            Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                            Message message = new Message()
                            {
                                Id = Guid.NewGuid(),
                                Title = "$$$" + txtTitle.Text,
                                UserId = userId,
                                Text = areaText.InnerText,
                                Date = DateTime.Now,
                                Incidence = iManager.Get(id)
                            };
                            MessageManager messageManager = new MessageManager(context);
                            messageManager.Add(message);
                            context.SaveChanges();
                            txtTitle.Text = "";
                            areaText.InnerText = "";
                            Response.Redirect(Request.RawUrl, true);
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

        public void datatableIncidenceMesages(Guid IncId)
        {
            DataTable mensajes = new DataTable();
            //mensajes.Columns.Add("id");
            mensajes.Columns.Add("User");
            mensajes.Columns.Add("Fecha");
            mensajes.Columns.Add("Titulo");
            mensajes.Columns.Add("Mensaje");
            using (DBContext db = new DBContext("DefaultConnection"))
            {
                IncidenceManager incedenceManager = new IncidenceManager(context);
                Incidence inci = incidenceManager.Get(id);
                var consulta = from i in db.Messages
                               where i.Incidence.Id == inci.Id
                               orderby i.Date
                               select new { i.Id, i.Date, i.UserId, i.Title, i.Text };
                consulta.ToList().ForEach((n) =>
                {
                    String tit = n.Title;
                    DataRow row = mensajes.NewRow();
                    //row.SetField<Guid>("id", n.Id);
                    MembershipUser user = Membership.GetUser(n.UserId);
                    row.SetField<String>("User", user.UserName);
                    row.SetField<DateTime>("Fecha", n.Date);
                    row.SetField<String>("Titulo", n.Title);
                    row.SetField<String>("Mensaje", n.Text);
                    mensajes.Rows.Add(row);
                });
            }
            GridMessages.DataSource = mensajes;
            GridMessages.DataBind();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("IncidenceList.aspx");
        }

        protected void BtnReabrir_Click(object sender, EventArgs e)
        {
            try
            {
                string idQueryString = Request.QueryString["Id"];
                Guid.TryParse(idQueryString, out id);
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
                Incidence incidence = incidenceManager.GetWithEquipment(id);
                incidence.CloseDate = DateTime.MaxValue;
                incidence.Status = IncidenceStatus.Abierta;
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                string idQueryString = Request.QueryString["Id"];
                Guid.TryParse(idQueryString, out id);
                context = new DBContext("DefaultConnection");
                incidenceManager = new IncidenceManager(context);
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idQueryString = Request.QueryString["Id"];
            Guid.TryParse(idQueryString, out id);
            context = new DBContext("DefaultConnection");
            incidenceManager = new IncidenceManager(context);
            Incidence incidence = incidenceManager.GetWithEquipment(id);
            incidenceManager.Remove(incidence);
            MessageManager mManager = new MessageManager(context);
            IEnumerable<Message> list=mManager.GetByIncidence(incidence.Id);
            List<Message> lista = list.ToList<Message>();
            var fistElement = lista.FirstOrDefault();
            if (fistElement != null)
            { //Hay primer elemento
                for (var i = 0; i < lista.Count; i++)
                { //Para cada elemento
                    Message message = lista.ElementAt(i);
                    mManager.Remove(message);
                }
            }
            context.SaveChanges();
            Response.Redirect("IncidenceList.aspx");

        }
    }
}