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

                int? id = int.TryParse(Request.QueryString["id"], out int aux) ? aux : (int?)null;
                if (id.HasValue && !IsPostBack) {
                    btnEliminar.Visible = true;
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    Usuario seleccionado = (negocio.listar(id))[0];

                    Session.Add("usuarioSeleccionado", seleccionado);

                    ddlEstado.Items.Add(new ListItem("Activo", "true"));
                    ddlEstado.Items.Add(new ListItem("Inactivo", "false"));

                    txtNombre.Text = seleccionado.Nombre;

                    ddlEmpleado.SelectedValue = seleccionado.Empleado.IdEmpleado.ToString();
                    ddlRol.SelectedValue = seleccionado.Rol.IdRol.ToString();
                    ddlEstado.SelectedValue = seleccionado.Activo.ToString().ToLower();
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}