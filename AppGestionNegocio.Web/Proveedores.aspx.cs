using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;

namespace AppGestionNegocio.Web
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProveedores();
            }
        }
        private void cargarProveedores()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();

            string nombre = txtFiltroNombre.Text.Trim();

            dgvProveedores.DataSource = negocio.filtrar(nombre);
            dgvProveedores.DataBind();
        }
        private void mostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;

            string script = "setTimeout(function() { " + "var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); " + "if (mensaje) { mensaje.innerHTML = ''; } " + "}, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje", script, true);
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    mostrarMensaje("Debe ingresar el nombre del proveedor.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    mostrarMensaje("Debe ingresar el teléfono del proveedor.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    mostrarMensaje("Debe ingresar el email del proveedor.");
                    return;
                }

                Proveedor proveedor = new Proveedor();
                proveedor.Nombre = txtNombre.Text.Trim();
                proveedor.Telefono = txtTelefono.Text.Trim();
                proveedor.Email = txtEmail.Text.Trim();
                proveedor.Activo = true;

                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.agregar(proveedor);

                txtNombre.Text = "";
                txtTelefono.Text = "";
                txtEmail.Text = "";
                txtFiltroNombre.Text = "";

                cargarProveedores();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al agregar el proveedor: " + ex.Message);
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarProveedores();
        }
        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            cargarProveedores();
        }
        protected void dgvProveedores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvProveedores.EditIndex = e.NewEditIndex;
            cargarProveedores();
        }
        protected void dgvProveedores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvProveedores.EditIndex = -1;
            cargarProveedores();
        }
        protected void dgvProveedores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idProveedor = (int)dgvProveedores.DataKeys[e.RowIndex].Value;

                GridViewRow fila = dgvProveedores.Rows[e.RowIndex];

                TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");
                TextBox txtTelefonoEdit = (TextBox)fila.FindControl("txtTelefonoEdit");
                TextBox txtEmailEdit = (TextBox)fila.FindControl("txtEmailEdit");

                if (string.IsNullOrWhiteSpace(txtNombreEdit.Text))
                {
                    mostrarMensaje("Debe ingresar el nombre del proveedor.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTelefonoEdit.Text))
                {
                    mostrarMensaje("Debe ingresar el teléfono del proveedor.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmailEdit.Text))
                {
                    mostrarMensaje("Debe ingresar el email del proveedor.");
                    return;
                }

                Proveedor proveedor = new Proveedor();
                proveedor.IdProveedor = idProveedor;
                proveedor.Nombre = txtNombreEdit.Text.Trim();
                proveedor.Telefono = txtTelefonoEdit.Text.Trim();
                proveedor.Email = txtEmailEdit.Text.Trim();
                proveedor.Activo = true;

                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.modificar(proveedor);

                dgvProveedores.EditIndex = -1;
                cargarProveedores();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al modificar el proveedor: " + ex.Message);
            }
        }
        protected void dgvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "EliminarProveedor")
                {
                    int idProveedor = int.Parse(e.CommandArgument.ToString());

                    ProveedorNegocio negocio = new ProveedorNegocio();
                    negocio.eliminar(idProveedor);

                    dgvProveedores.EditIndex = -1;
                    cargarProveedores();
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar el proveedor: " + ex.Message);
            }
        }
    }
}