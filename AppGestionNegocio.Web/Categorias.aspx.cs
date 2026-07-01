using System;
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
                contenedorInactivos.Visible = Seguridad.esAdmin(Session["usuario"]);
                cargarCategorias();
            }
        }

        private void cargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            string nombre = txtFiltroNombre.Text.Trim();
            bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

            cardNuevaCategoria.Visible = !verInactivos;

            if (verInactivos)
            {
                lblTituloListado.Text = "Categorías inactivas";
                dgvCategorias.EmptyDataText = "No hay categorías inactivas registradas.";
            }
            else
            {
                lblTituloListado.Text = "Categorías registradas";
                dgvCategorias.EmptyDataText = "No hay categorías activas registradas.";
            }

            dgvCategorias.DataSource = negocio.filtrar(nombre, verInactivos);
            dgvCategorias.DataBind();
        }

        private void mostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;

            string script = "setTimeout(function() { " + "var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); " + "if (mensaje) { mensaje.innerHTML = ''; } " + "}, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje", script, true);
        }

        private void mostrarMensajeListado(string mensaje)
        {
            lblMensajeListado.Text = mensaje;

            string script = "setTimeout(function() { " + "var mensaje = document.getElementById('" + lblMensajeListado.ClientID + "'); " + "if (mensaje) { mensaje.innerHTML = ''; } " + "}, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensajeListado", script, true);
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

        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            dgvCategorias.EditIndex = -1;
            lblMensaje.Text = "";
            lblMensajeListado.Text = "";
            cargarCategorias();
        }

        protected void dgvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (chkVerInactivos.Checked)
            {
                e.Cancel = true;
                return;
            }

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
                lblMensajeListado.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idCategoria = int.Parse(e.CommandArgument.ToString());

                    hfIdCategoriaEliminar.Value = idCategoria.ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalEliminarCategoria", "$('#modalEliminarCategoria').modal('show');", true);
                }

                if (e.CommandName == "Restaurar")
                {
                    int idCategoria = int.Parse(e.CommandArgument.ToString());

                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.restaurar(idCategoria);

                    cargarCategorias();

                    mostrarMensajeListado("Categoría restaurada correctamente.");
                }
            }
            catch (Exception ex)
            {
                mostrarMensajeListado("Error al seleccionar la categoría: " + ex.Message);
            }
        }

        protected void dgvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState != DataControlRowState.Edit && e.Row.RowState != (DataControlRowState.Alternate | DataControlRowState.Edit))
            {
                bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

                Button btnEditar = (Button)e.Row.FindControl("btnEditar");
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");
                Button btnRestaurar = (Button)e.Row.FindControl("btnRestaurar");

                if (btnEditar != null)
                    btnEditar.Visible = !verInactivos;

                if (btnEliminar != null)
                    btnEliminar.Visible = !verInactivos;

                if (btnRestaurar != null)
                    btnRestaurar.Visible = verInactivos;
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                lblMensajeListado.Text = "";

                int idCategoria;

                if (!int.TryParse(hfIdCategoriaEliminar.Value, out idCategoria))
                {
                    mostrarMensaje("No se pudo identificar la categoría a eliminar.");
                    return;
                }

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.eliminar(idCategoria);

                hfIdCategoriaEliminar.Value = "";

                dgvCategorias.EditIndex = -1;
                cargarCategorias();

                mostrarMensaje("Categoría eliminada correctamente.");
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar la categoría: " + ex.Message);
            }
        }
    }
}