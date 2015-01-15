using CallCenter.Application;
using CallCenter.CORE;
using CallCenter.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Callcenter.User
{
    public partial class EquipmentTypeNewEditU : System.Web.UI.Page
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
                            BtnNew.Visible = false;
                            BtnEdit.Visible = true;
                            TxtName.Text = eq.Type;
                            TxtDescription.Text = eq.Description;
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
            if (TxtName.Text == "" || TxtDescription.Text == "")
            {
                LblError.Text = "Rellena los campos";
            }
            else
            {
                try
                {
                    context = new DBContext("DefaultConnection");
                    equipmentTypeManager = new EquipmentTypeManager(context);
                    EquipmentType equipmentType = new EquipmentType()
                    {
                        Id = Guid.NewGuid(),
                        Type = TxtName.Text,
                        Description = TxtDescription.Text
                    };
                    equipmentTypeManager.Add(equipmentType);
                    context.SaveChanges();
                    LblExito.Text = "Añadido";
                    TxtName.Text = "";
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
            if (TxtName.Text == "" || TxtDescription.Text == "")
            {
                LblError.Text = "Rellena los campos";
            }
            else
            {
                try
                {
                    string idQueryString = Request.QueryString["Id"];
                    Guid.TryParse(idQueryString, out id);
                    context = new DBContext("DefaultConnection");
                    equipmentTypeManager = new EquipmentTypeManager(context);
                    EquipmentType equipmentType = new EquipmentType()
                    {
                        Id = id,
                        Type = TxtName.Text,
                        Description = TxtDescription.Text
                    };
                    equipmentTypeManager.Update(equipmentType);
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

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("EquipmentTypeListU.aspx");
        }

       
    }
}