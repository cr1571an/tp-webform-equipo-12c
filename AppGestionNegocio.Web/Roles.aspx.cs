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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    lblMensaje.Text = "";

            //    if (string.IsNullOrWhiteSpace(txtNombre.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el nombre del proveedor.");
            //        return;
            //    }

            //    if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el teléfono del proveedor.");
            //        return;
            //    }

            //    if (string.IsNullOrWhiteSpace(txtEmail.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el email del proveedor.");
            //        return;
            //    }

            //    Proveedor proveedor = new Proveedor();
            //    proveedor.Nombre = txtNombre.Text.Trim();
            //    proveedor.Telefono = txtTelefono.Text.Trim();
            //    proveedor.Email = txtEmail.Text.Trim();
            //    proveedor.Activo = true;

            //    RolNegocio negocio = new RolNegocio();
            //    negocio.agregar(proveedor);

            //    txtNombre.Text = "";
            //    txtTelefono.Text = "";
            //    txtEmail.Text = "";
            //    txtFiltroNombre.Text = "";

            //    cargarRoles();
            //}
            //catch (Exception ex)
            //{
            //    mostrarMensaje("Error al agregar el proveedor: " + ex.Message);
            //}
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
            //try
            //{
            //    lblMensaje.Text = "";

            //    int idProveedor = (int)dgvRoles.DataKeys[e.RowIndex].Value;

            //    GridViewRow fila = dgvRoles.Rows[e.RowIndex];

            //    TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");
            //    TextBox txtTelefonoEdit = (TextBox)fila.FindControl("txtTelefonoEdit");
            //    TextBox txtEmailEdit = (TextBox)fila.FindControl("txtEmailEdit");

            //    if (string.IsNullOrWhiteSpace(txtNombreEdit.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el nombre del proveedor.");
            //        return;
            //    }

            //    if (string.IsNullOrWhiteSpace(txtTelefonoEdit.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el teléfono del proveedor.");
            //        return;
            //    }

            //    if (string.IsNullOrWhiteSpace(txtEmailEdit.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el email del proveedor.");
            //        return;
            //    }

            //    Proveedor proveedor = new Proveedor();
            //    proveedor.IdProveedor = idProveedor;
            //    proveedor.Nombre = txtNombreEdit.Text.Trim();
            //    proveedor.Telefono = txtTelefonoEdit.Text.Trim();
            //    proveedor.Email = txtEmailEdit.Text.Trim();
            //    proveedor.Activo = true;

            //    RolNegocio negocio = new RolNegocio();
            //    negocio.modificar(proveedor);

            //    dgvRoles.EditIndex = -1;
            //    cargarRoles();
            //}
            //catch (Exception ex)
            //{
            //    mostrarMensaje("Error al modificar el proveedor: " + ex.Message);
            //}
        }
        protected void dgvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
            //    lblMensaje.Text = "";

            //    if (e.CommandName == "EliminarProveedor")
            //    {
            //        int idProveedor = int.Parse(e.CommandArgument.ToString());

            //        RolNegocio negocio = new RolNegocio();
            //        negocio.eliminar(idProveedor);

            //        dgvRoles.EditIndex = -1;
            //        cargarRoles();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    mostrarMensaje("Error al eliminar el proveedor: " + ex.Message);
            //}
        }
    }
}