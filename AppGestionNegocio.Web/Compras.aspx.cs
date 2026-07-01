using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppGestionNegocio.Web
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                contenedorAnuladas.Visible = Seguridad.esAdmin(Session["usuario"]);
                cargarCompras();
            }
        }

        private void cargarCompras()
        {
            try
            {
                CompraNegocio negocio = new CompraNegocio();

                bool verAnuladas = Seguridad.esAdmin(Session["usuario"]) && chkVerAnuladas.Checked;

                lnkRegistrarCompra.Visible = !verAnuladas;

                if (verAnuladas)
                {
                    dgvCompras.EmptyDataText = "No hay compras anuladas registradas.";
                }
                else
                {
                    dgvCompras.EmptyDataText = "No se encontraron compras registradas.";
                }

                dgvCompras.Columns[5].Visible = !verAnuladas;

                List<Compra> lista = negocio.listar(verAnuladas);

                string filtro = txtFiltroCompra.Text.Trim();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    lista = lista.Where(compra =>
                        contiene(compra.Proveedor.Nombre, filtro) ||
                        contiene(compra.NumeroComprobante, filtro)
                    ).ToList();
                }

                Session["listaCompras"] = lista;

                dgvCompras.DataSource = lista;
                dgvCompras.DataBind();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al cargar compras: " + ex.Message, true);
            }
        }

        private bool contiene(string valor, string filtro)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            return valor.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        protected void chkVerAnuladas_CheckedChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            dgvCompras.PageIndex = 0;
            cargarCompras();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            dgvCompras.PageIndex = 0;
            cargarCompras();
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroCompra.Text = string.Empty;
            dgvCompras.PageIndex = 0;
            cargarCompras();
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idCompra;

                if (!int.TryParse(hfIdCompraEliminar.Value, out idCompra))
                {
                    mostrarMensaje("No se pudo identificar la compra a eliminar.", true);
                    return;
                }

                CompraNegocio negocio = new CompraNegocio();

                negocio.eliminar(idCompra);

                hfIdCompraEliminar.Value = "";

                dgvCompras.PageIndex = 0;
                cargarCompras();

                mostrarMensaje("Compra eliminada correctamente.", false);
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar compra: " + ex.Message, true);
            }
        }

        protected void dgvCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCompras.PageIndex = e.NewPageIndex;

            if (Session["listaCompras"] != null)
            {
                dgvCompras.DataSource = Session["listaCompras"];
                dgvCompras.DataBind();
            }
            else
            {
                cargarCompras();
            }
        }

        protected void dgvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idCompra = int.Parse(e.CommandArgument.ToString());

                    hfIdCompraEliminar.Value = idCompra.ToString();

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalEliminarCompra", "$('#modalEliminarCompra').modal('show');", true);
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al seleccionar la compra: " + ex.Message, true);
            }
        }

        private void mostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "message text-danger d-block" : "message text-success d-block";
            lblMensaje.Style["display"] = "block";

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); if (mensaje) { mensaje.innerHTML = ''; mensaje.classList.remove('d-block'); mensaje.classList.add('d-none'); mensaje.style.display = 'none'; } }, 4000);";

            ScriptManager.RegisterStartupScript(this, GetType(), "ocultarMensajeCompra", script, true);
        }
    }
}