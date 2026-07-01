using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;

namespace AppGestionNegocio.Web
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                contenedorInactivos.Visible = Seguridad.esAdmin(Session["usuario"]);
                cargarMarcas();
            }
        }

        private void cargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            string nombre = txtFiltroNombre.Text.Trim();
            bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

            cardNuevaMarca.Visible = !verInactivos;

            if (verInactivos)
            {
                lblTituloListado.Text = "Marcas inactivas";
                dgvMarcas.EmptyDataText = "No hay marcas inactivas registradas.";
            }
            else
            {
                lblTituloListado.Text = "Marcas registradas";
                dgvMarcas.EmptyDataText = "No hay marcas activas registradas.";
            }

            dgvMarcas.DataSource = negocio.filtrar(nombre, verInactivos);
            dgvMarcas.DataBind();
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
                    mostrarMensaje("Debe ingresar el nombre de la marca.");
                    return;
                }

                Marca marca = new Marca();
                marca.Nombre = txtNombre.Text.Trim();
                marca.Activo = true;

                MarcaNegocio negocio = new MarcaNegocio();
                negocio.agregar(marca);

                txtNombre.Text = "";
                txtFiltroNombre.Text = "";

                cargarMarcas();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al agregar la marca: " + ex.Message);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarMarcas();
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            cargarMarcas();
        }

        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            dgvMarcas.EditIndex = -1;
            lblMensaje.Text = "";
            lblMensajeListado.Text = "";
            cargarMarcas();
        }

        protected void dgvMarcas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (chkVerInactivos.Checked)
            {
                e.Cancel = true;
                return;
            }

            dgvMarcas.EditIndex = e.NewEditIndex;
            cargarMarcas();
        }

        protected void dgvMarcas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvMarcas.EditIndex = -1;
            cargarMarcas();
        }

        protected void dgvMarcas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idMarca = (int)dgvMarcas.DataKeys[e.RowIndex].Value;

                GridViewRow fila = dgvMarcas.Rows[e.RowIndex];

                TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");

                if (string.IsNullOrWhiteSpace(txtNombreEdit.Text))
                {
                    mostrarMensaje("Debe ingresar el nombre de la marca.");
                    return;
                }

                Marca marca = new Marca();
                marca.IdMarca = idMarca;
                marca.Nombre = txtNombreEdit.Text.Trim();
                marca.Activo = true;

                MarcaNegocio negocio = new MarcaNegocio();
                negocio.modificar(marca);

                dgvMarcas.EditIndex = -1;
                cargarMarcas();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al modificar la marca: " + ex.Message);
            }
        }

        protected void dgvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                lblMensajeListado.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idMarca = int.Parse(e.CommandArgument.ToString());

                    hfIdMarcaEliminar.Value = idMarca.ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalEliminarMarca", "$('#modalEliminarMarca').modal('show');", true);
                }

                if (e.CommandName == "Restaurar")
                {
                    int idMarca = int.Parse(e.CommandArgument.ToString());

                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.restaurar(idMarca);

                    cargarMarcas();

                    mostrarMensajeListado("Marca restaurada correctamente.");
                }
            }
            catch (Exception ex)
            {
                mostrarMensajeListado("Error al seleccionar la marca: " + ex.Message);
            }
        }

        protected void dgvMarcas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
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

                int idMarca;

                if (!int.TryParse(hfIdMarcaEliminar.Value, out idMarca))
                {
                    mostrarMensaje("No se pudo identificar la marca a eliminar.");
                    return;
                }

                MarcaNegocio negocio = new MarcaNegocio();
                negocio.eliminar(idMarca);

                hfIdMarcaEliminar.Value = "";

                dgvMarcas.EditIndex = -1;
                cargarMarcas();

                mostrarMensaje("Marca eliminada correctamente.");
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar la marca: " + ex.Message);
            }
        }
    }
}