using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;

namespace AppGestionNegocio.Web
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarClientes();
            }
        }

        private void cargarClientes()
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();
                List<Cliente> lista = negocio.listar();

                string filtro = txtFiltroCliente.Text.Trim();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    lista = lista.Where(cliente =>
                        contiene(cliente.Nombre, filtro) ||
                        contiene(cliente.Apellido, filtro) ||
                        contiene(cliente.Cuit, filtro) ||
                        contiene(cliente.Nombre + " " + cliente.Apellido, filtro)
                    ).ToList();
                }

                Session["listaClientes"] = lista;

                dgvClientes.DataSource = lista;
                dgvClientes.DataBind();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al cargar clientes: " + ex.Message, true);
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

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            dgvClientes.PageIndex = 0;
            cargarClientes();
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroCliente.Text = "";
            dgvClientes.PageIndex = 0;
            cargarClientes();
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;

            if (Session["listaClientes"] != null)
            {
                dgvClientes.DataSource = Session["listaClientes"];
                dgvClientes.DataBind();
            }
            else
            {
                cargarClientes();
            }
        }

        protected void dgvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idCliente = int.Parse(e.CommandArgument.ToString());

                    hfIdClienteEliminar.Value = idCliente.ToString();

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "abrirModalEliminarCliente",
                        "$('#modalEliminarCliente').modal('show');",
                        true
                    );
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al seleccionar el cliente: " + ex.Message, true);
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idCliente;

                if (!int.TryParse(hfIdClienteEliminar.Value, out idCliente))
                {
                    mostrarMensaje("No se pudo identificar el cliente a eliminar.", true);
                    return;
                }

                ClienteNegocio negocio = new ClienteNegocio();
                negocio.eliminar(idCliente);

                hfIdClienteEliminar.Value = "";

                cargarClientes();

                mostrarMensaje("Cliente eliminado correctamente.", false);
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar cliente: " + ex.Message, true);
            }
        }

        private void mostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "message text-danger" : "message text-success";

            string script = "setTimeout(function() { " +
                            "var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); " +
                            "if (mensaje) { mensaje.innerHTML = ''; } " +
                            "}, 4000);";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "ocultarMensajeCliente",
                script,
                true
            );
        }
    }
}