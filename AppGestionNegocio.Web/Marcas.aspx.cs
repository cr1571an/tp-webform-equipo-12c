using System;
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
                cargarMarcas();
            }
        }
        private void cargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            string nombre = txtFiltroNombre.Text.Trim();

            dgvMarcas.DataSource = negocio.filtrar(nombre);
            dgvMarcas.DataBind();
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
        protected void dgvMarcas_RowEditing(object sender, GridViewEditEventArgs e)
        {
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

                if (e.CommandName == "EliminarMarca")
                {
                    int idMarca = int.Parse(e.CommandArgument.ToString());

                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.eliminar(idMarca);

                    dgvMarcas.EditIndex = -1;
                    cargarMarcas();
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar la marca: " + ex.Message);
            }
        }
    }
}