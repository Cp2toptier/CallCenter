using Callcenter.Web;
using CallCenter.Application;
using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Callcenter.Admin
{
    public partial class EquipmentNewEdit : System.Web.UI.Page
    {
        DBContext context = null;
        EquipmentManager equipmentManager = null;
        EquipmentTypeManager etManager = null;
        Guid id;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Llenar el DropEquipment
            if (!Page.IsPostBack)
            {

                CargarCombo("");
                context = new DBContext("DefaultConnection");
                equipmentManager = new EquipmentManager(context);
                //Con request pillamos los parametros de la url
                string idQueryString = Request.QueryString["Id"];
                if (idQueryString != null)
                {//Aqui es que hay ?Id (si no, no hace falta hacer nada)
                    if (Guid.TryParse(idQueryString, out id)) //Parse convierte idQueryString en Guid. Si lo hace, lo mete en la variable id
                    { //Aqui es que es Guid
                        Equipment eq = equipmentManager.Get(id); //Devuelve de la base de datos el equipo con ID = id
                        CargarCombo(eq.Id.ToString());
                        EquipmentTypeManager etManager = new EquipmentTypeManager(context);
                        EquipmentType et = new EquipmentType();
                        if (eq != null)
                        {//Existe en la BD
                            BtnNew.Visible = false;
                            BtnEdit.Visible = true;
                            TxtPurchaseDate.Text = eq.PurchaseDate.ToString("dd/MM/yyyy");
                            TxtDescription.Text = eq.Description;
                            DropEquipmentType.SelectedValue = et.Id.ToString();
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
            //Reset de label
            LblError.Text = ""; LblExito.Text = "";
            //Validacion
            string sFecha = TxtPurchaseDate.Text;
            DateTime dFecha;
            bool ok = DateTime.TryParse(sFecha, out dFecha);
            if (TxtPurchaseDate.Text == "" || TxtDescription.Text == "")
            {
                LblError.Text = "Rellena los campos";
            }
            else if (!ok)
            {
                LblError.Text = "Usa una fecha válida con formato DD/MM/AAAA";
            }
            else
            {
                try
                {
                    context = new DBContext("DefaultConnection");
                    equipmentManager = new EquipmentManager(context);
                    etManager = new EquipmentTypeManager(context);
                    DateTime fecha = DateTime.ParseExact(TxtPurchaseDate.Text, "dd/MM/yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                    Guid eType;
                    Guid.TryParse(DropEquipmentType.SelectedValue, out eType);
                    MembershipUser user = Membership.GetUser();
                    Guid userId = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                    EquipmentType eqType = etManager.Get(eType);
                    Equipment equipment = new Equipment()
                    {
                        Id = Guid.NewGuid(),
                        PurchaseDate = fecha,
                        UserId = userId,
                        Description = TxtDescription.Text,
                        EquipmentType = eqType
                    };
                    equipmentManager.Add(equipment);
                    context.SaveChanges();
                    LblExito.Text = "Añadido";
                    TxtPurchaseDate.Text = "";
                    TxtDescription.Text = "";
                }
                catch (Exception ex)
                {
                    //Guardar ex.Message en un log
                    LblError.Text = Messages.txtError; //Messages es un archivo de recursos con datos, como el mensaje de error
                }
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //Reset de label
            LblError.Text = ""; LblExito.Text = "";
            //Validacion
            string sFecha = TxtPurchaseDate.Text;
            DateTime dFecha;
            bool ok = DateTime.TryParse(sFecha, out dFecha);
            if (TxtPurchaseDate.Text == "" || TxtDescription.Text == "")
            {
                LblError.Text = "Rellena los campos";
            }
            else if (!ok)
            {
                LblError.Text = "Usa una fecha válida con formato DD/MM/AAAA";
            }
            else
            {
                try
                {
                    string idQueryString = Request.QueryString["Id"];
                    Guid.TryParse(idQueryString, out id);
                    context = new DBContext("DefaultConnection");
                    equipmentManager = new EquipmentManager(context);
                    etManager = new EquipmentTypeManager(context);
                    DateTime fecha;
                    DateTime.TryParse(TxtPurchaseDate.Text, out fecha);
                    Guid eType;
                    Guid.TryParse(DropEquipmentType.SelectedValue, out eType);
                    EquipmentType eqType = etManager.Get(eType);
                    Equipment equipment = equipmentManager.GetOneWithEquipmentType(id);
                    equipment.PurchaseDate = fecha;
                    equipment.Description = TxtDescription.Text;
                    equipment.EquipmentType = eqType;
                    equipmentManager.Update(equipment);
                    context.SaveChanges();
                    LblExito.Text = "Editado";
                }
                catch (Exception ex)
                {
                    //Guardar ex.Message en un log
                    LblError.Text = Messages.txtError; //Messages es un archivo de recursos con datos, como el mensaje de error
                }
            }
        }
        /// <summary>
        /// Carga el combo de equipos
        /// </summary>
        /// <param name="id">Le pasa una string con a guid del equipo que se quiera seleccionado.
        /// Cadena vacía para no seleccionar</param>
        private void CargarCombo(String id)
        {
            DataTable eTypes = new DataTable();
            eTypes.Columns.Add("Id");
            eTypes.Columns.Add("Tipo");
            using (DBContext db = new DBContext("DefaultConnection"))
            {
                var consulta = from i in db.EquipmentTypes
                               select new { i.Id, i.Type };
                consulta.ToList().ForEach((n) =>
                {
                    DataRow row = eTypes.NewRow();
                    row.SetField<Guid>("Id", n.Id);
                    row.SetField<String>("Tipo", n.Type);
                    eTypes.Rows.Add(row);
                });
            }
            DropEquipmentType.DataTextField = "Tipo";
            DropEquipmentType.DataValueField = "Id";
            DropEquipmentType.DataSource = eTypes;
            DropEquipmentType.DataBind();
            if (id != "")
            {
                DropEquipmentType.SelectedValue = id.ToString();
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EquipmentList.aspx");
        }
    }
}