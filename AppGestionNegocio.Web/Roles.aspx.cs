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
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarRoles();
            }
        }

        private void cargarRoles()
        {
            RolNegocio negocio = new RolNegocio();

            string nombre = txtFiltroNombre.Text.Trim();

            dgvRoles.DataSource = negocio.filtrar(nombre);
            dgvRoles.DataBind();
        }

        private void mostrarMensaje(Label lbl, string mensaje)
        {
            lbl.Text = mensaje;

            string script = "setTimeout(function() { " + "var mensaje = document.getElementById('" + lbl.ClientID + "'); " + "if (mensaje) { mensaje.innerHTML = ''; } " + "}, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje", script, true);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    mostrarMensaje(lblMensaje, "Debe ingresar el nombre del rol y descripción.");
                    return;
                }

                Rol rol = new Rol();
                rol.Nombre = txtNombre.Text.Trim();
                rol.Descripcion = txtDescripcion.Text.Trim();
                rol.Activo = true;

                RolNegocio negocio = new RolNegocio();
                negocio.agregar(rol);

                txtNombre.Text = "";
                txtDescripcion.Text = "";
                txtFiltroNombre.Text = "";

                cargarRoles();
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensaje, "Error al agregar el rol: " + ex.Message);
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarRoles();
        }
        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            cargarRoles();
        }
        protected void dgvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RolNegocio negocio = new RolNegocio();
            int id = int.Parse(e.CommandArgument.ToString());

            lblMensajeModal.Text = "";
            if (e.CommandName == "EditarModal")
            {
                try
                {

                    Rol rol = (negocio.listar(id))[0];

                    hfIdRol.Value = rol.IdRol.ToString();
                    txtNombreModal.Text = rol.Nombre;
                    txtDescripcionModal.Text = rol.Descripcion;


                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "abrirModal",
                        "$('#modalEditar').modal('show');",
                        true);
                }
                catch (Exception ex)
                {
                    mostrarMensaje(lblMensajeModal, "Error al eliminar el rol: " + ex.Message);
                }
            }
            else if (e.CommandName == "EliminarRol")
            {
                try
                {                    
                    negocio.eliminar(id);
                    cargarRoles();
                }
                catch (Exception ex)
                {
                    mostrarMensaje(lblMensajeModal, "Error al actulizar el rol: " + ex.Message);
                }
            }

        }

        protected void btnGuardarModal_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensajeModal.Text = "";
                if (string.IsNullOrWhiteSpace(txtNombreModal.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcionModal.Text))
                {
                    mostrarMensaje(lblMensajeModal, "Nombre y descripción son obligatorios");

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "abrirModal",
                        "$('#modalEditar').modal('show');",
                        true);
                    return;
                }

                Rol rol = new Rol();
                rol.IdRol = int.Parse(hfIdRol.Value);
                rol.Nombre = txtNombreModal.Text.Trim();
                rol.Descripcion = txtDescripcionModal.Text;
                rol.Activo = true;

                RolNegocio negocio = new RolNegocio();
                negocio.modificar(rol);

                txtNombreModal.Text = "";
                txtDescripcionModal.Text = "";
                txtFiltroNombre.Text = "";

                cargarRoles();
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensajeModal, "Error al agregar el proveedor: " + ex.Message);
            }

        }
    }
}