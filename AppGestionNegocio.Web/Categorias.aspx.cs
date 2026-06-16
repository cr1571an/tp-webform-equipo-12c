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
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCategorias();
            }
        }
        private void cargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            string nombre = txtFiltroNombre.Text.Trim();

            dgvCategorias.DataSource = negocio.filtrar(nombre);
            dgvCategorias.DataBind();
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
                    mostrarMensaje("Debe ingresar el nombre de la categoría.");
                    return;
                }

                Categoria categoria = new Categoria();
                categoria.Nombre = txtNombre.Text.Trim();
                categoria.Activo = true;

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.agregar(categoria);

                txtNombre.Text = "";
                txtFiltroNombre.Text = "";

                cargarCategorias();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al agregar la categoría: " + ex.Message);
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarCategorias();
        }
        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            cargarCategorias();
        }
        protected void dgvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvCategorias.EditIndex = e.NewEditIndex;
            cargarCategorias();
        }
        protected void dgvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvCategorias.EditIndex = -1;
            cargarCategorias();
        }
        protected void dgvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idCategoria = (int)dgvCategorias.DataKeys[e.RowIndex].Value;

                GridViewRow fila = dgvCategorias.Rows[e.RowIndex];

                TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");

                if (string.IsNullOrWhiteSpace(txtNombreEdit.Text))
                {
                    mostrarMensaje("Debe ingresar el nombre de la categoría.");
                    return;
                }

                Categoria categoria = new Categoria();
                categoria.IdCategoria = idCategoria;
                categoria.Nombre = txtNombreEdit.Text.Trim();
                categoria.Activo = true;

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.modificar(categoria);

                dgvCategorias.EditIndex = -1;
                cargarCategorias();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al modificar la categoría: " + ex.Message);
            }
        }
        protected void dgvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "EliminarCategoria")
                {
                    int idCategoria = int.Parse(e.CommandArgument.ToString());

                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.eliminar(idCategoria);

                    dgvCategorias.EditIndex = -1;
                    cargarCategorias();
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar la categoría: " + ex.Message);
            }
        }
    }
}