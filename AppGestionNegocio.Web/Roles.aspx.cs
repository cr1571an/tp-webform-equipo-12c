using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;
using System;
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

        protected void dgvRoles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvRoles.EditIndex = e.NewEditIndex;
            cargarRoles();
        }

        protected void dgvRoles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvRoles.EditIndex = -1;
            cargarRoles();
        }

        protected void dgvRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idRol = (int)dgvRoles.DataKeys[e.RowIndex].Value;

                GridViewRow fila = dgvRoles.Rows[e.RowIndex];

                TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");
                TextBox txtDescripcionEdit = (TextBox)fila.FindControl("txtDescripcionEdit");

                if (string.IsNullOrWhiteSpace(txtNombreEdit.Text) || string.IsNullOrWhiteSpace(txtDescripcionEdit.Text))
                {
                    mostrarMensaje(lblMensaje, "Debe ingresar el nombre del rol y descripción.");
                    return;
                }

                Rol rol = new Rol();
                rol.IdRol = idRol;
                rol.Nombre = txtNombreEdit.Text.Trim();
                rol.Descripcion = txtDescripcionEdit.Text.Trim();
                rol.Activo = true;

                RolNegocio negocio = new RolNegocio();
                negocio.modificar(rol);

                dgvRoles.EditIndex = -1;
                cargarRoles();
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensaje, "Error al modificar el rol: " + ex.Message);
            }
        }

        protected void dgvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idRol = int.Parse(e.CommandArgument.ToString());

                    hfIdRolEliminar.Value = idRol.ToString();

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "abrirModalEliminarRol",
                        "$('#modalEliminarRol').modal('show');",
                        true
                    );
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensaje, "Error al seleccionar el rol: " + ex.Message);
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idRol;

                if (!int.TryParse(hfIdRolEliminar.Value, out idRol))
                {
                    mostrarMensaje(lblMensaje, "No se pudo identificar el rol a eliminar.");
                    return;
                }

                RolNegocio negocio = new RolNegocio();
                negocio.eliminar(idRol);

                hfIdRolEliminar.Value = "";

                dgvRoles.EditIndex = -1;
                cargarRoles();

                mostrarMensaje(lblMensaje, "Rol eliminado correctamente.");
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensaje, "Error al eliminar el rol: " + ex.Message);
            }
        }
    }
}