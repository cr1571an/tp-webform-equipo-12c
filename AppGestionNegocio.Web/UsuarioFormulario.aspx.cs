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
                if (id.HasValue && !IsPostBack)
                {
                    btnEliminar.Visible = true;
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    Usuario seleccionado = (negocio.listar(id))[0];

                    Session.Add("usuarioSeleccionado", seleccionado);

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

            ddlEstado.Items.Clear();
            ddlEstado.Items.Add(new ListItem("Activo", "true"));
            ddlEstado.Items.Add(new ListItem("Inactivo", "false"));
        }

        private bool ValidarCampo()
        {
            lblMensajeError.Visible = false;

            if (ddlEmpleado.SelectedValue == "0")
            {
                lblMensajeError.Text = "Error: Por favor, seleccione un Empleado.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (ddlRol.SelectedValue == "0")
            {
                lblMensajeError.Text = "Error: Por favor, seleccione un Rol.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMensajeError.Text = "Error: La contraseña es obligatoria.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                lblMensajeError.Text = "Error: La contraseña debe tener al menos 6 caracteres.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblMensajeError.Text = "Error: Las contraseñas no coinciden.";
                lblMensajeError.Visible = true;
                return false;
            }



            return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario seleccionado = (Usuario)Session["usuarioSeleccionado"];

                if (seleccionado != null)
                {
                    negocio.eliminar(seleccionado.IdUsuario);
                    Response.Redirect("Usuarios.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampo() == false) return;

                UsuarioNegocio negocio = new UsuarioNegocio();
                int? id = int.TryParse(Request.QueryString["id"], out int aux) ? aux : (int?)null;
                if (!id.HasValue)

                {
                    List<Usuario> listaUsuarios = negocio.listar();
                    foreach (Usuario usr in listaUsuarios)
                    {
                        if (usr.Nombre.Trim().ToUpper() == txtNombre.Text.Trim().ToUpper())
                        {
                            lblMensajeError.Text = "Error: Ya existe un ususario registrado con ese nombre.";
                            lblMensajeError.Visible = true;
                            return;
                        }
                    }
                }

                Usuario nuevo = new Usuario();

                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Empleado = new Empleado();
                nuevo.Empleado.IdEmpleado = int.Parse(ddlEmpleado.SelectedValue);
                nuevo.Rol = new Rol();
                nuevo.Rol.IdRol = int.Parse(ddlRol.SelectedValue);
                nuevo.Activo = bool.Parse(ddlEstado.SelectedValue);
                nuevo.PasswordHash = txtConfirmPassword.Text.ToString();


                if (id.HasValue)
                {
                    nuevo.IdUsuario = id.Value;
                    negocio.modificar(nuevo);
                }
                else
                {
                    negocio.agregar(nuevo);
                }
                Response.Redirect("Usuarios.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}