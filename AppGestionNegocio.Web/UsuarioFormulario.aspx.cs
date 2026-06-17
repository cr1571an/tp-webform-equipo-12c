using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppGestionNegocio.Web
{
    public partial class UsuarioFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    CargarDesplegables();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDesplegables()
        {

            EmpleadoNegocio empleadoNegocio = new EmpleadoNegocio();
            ddlEmpleado.DataSource = empleadoNegocio.listar();
            ddlEmpleado.DataValueField = "IdEmpleado";
            ddlEmpleado.DataTextField = "NombreCompleto";
            ddlEmpleado.DataBind();
            ddlEmpleado.Items.Insert(0, new ListItem("-- Seleccione una Empleado --", "0"));

            RolNegocio rolNegocio = new RolNegocio();
            ddlRol.DataSource = rolNegocio.listar();
            ddlRol.DataValueField = "IdRol";
            ddlRol.DataTextField = "Nombre";
            ddlRol.DataBind();

            ddlRol.Items.Insert(0, new ListItem("-- Seleccione una Rol --", "0"));

            
        }
    }
}