using System;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();

                string filtro = txtFiltroNombre.Text.Trim();
                string cp = txtFiltroCp.Text.Trim();

                dgvProveedores.DataSource = negocio.filtrar(filtro, cp);
                dgvProveedores.DataBind();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al cargar proveedores: " + ex.Message);
            }
        }
        private void mostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;

            string script = "setTimeout(function() { " + "var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); " + "if (mensaje) { mensaje.innerHTML = ''; } " + "}, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje", script, true);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarProveedores();
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            txtFiltroCp.Text = "";

            cargarProveedores();
        }
        protected void dgvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idProveedor = int.Parse(e.CommandArgument.ToString());

                    hfIdProveedorEliminar.Value = idProveedor.ToString();

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "abrirModalEliminarProveedor",
                        "$('#modalEliminarProveedor').modal('show');",
                        true
                    );
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al seleccionar el proveedor: " + ex.Message);
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idProveedor;

                if (!int.TryParse(hfIdProveedorEliminar.Value, out idProveedor))
                {
                    mostrarMensaje("No se pudo identificar el proveedor a eliminar.");
                    return;
                }

                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.eliminar(idProveedor);

                hfIdProveedorEliminar.Value = "";

                cargarProveedores();

                mostrarMensaje("Proveedor eliminado correctamente.");
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar proveedor: " + ex.Message);
            }
        }
    }
}