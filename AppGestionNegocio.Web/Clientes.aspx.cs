using System;
using System.Collections.Generic;
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
                contenedorInactivos.Visible = Seguridad.esAdmin(Session["usuario"]);
                cargarClientes();
            }
        }

        private void cargarClientes()
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();

                string filtro = txtFiltroCliente.Text.Trim();
                bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

                lnkNuevoCliente.Visible = !verInactivos;

                if (verInactivos)
                {
                    lblTituloListado.Text = "Clientes inactivos";
                    dgvClientes.EmptyDataText = "No hay clientes inactivos registrados.";
                }
                else
                {
                    lblTituloListado.Text = "Clientes registrados";
                    dgvClientes.EmptyDataText = "No hay clientes activos registrados.";
                }

                List<Cliente> lista = negocio.filtrar(filtro, verInactivos);

                Session["listaClientes"] = lista;

                dgvClientes.DataSource = lista;
                dgvClientes.DataBind();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al cargar clientes: " + ex.Message, true);
            }
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

        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
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

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalEliminarCliente", "$('#modalEliminarCliente').modal('show');", true);
                }

                if (e.CommandName == "Restaurar")
                {
                    int idCliente = int.Parse(e.CommandArgument.ToString());

                    ClienteNegocio negocio = new ClienteNegocio();
                    negocio.restaurar(idCliente);

                    cargarClientes();

                    mostrarMensaje("Cliente restaurado correctamente.", false);
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al seleccionar el cliente: " + ex.Message, true);
            }
        }

        protected void dgvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

                HyperLink lnkModificar = (HyperLink)e.Row.FindControl("lnkModificar");
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");
                Button btnRestaurar = (Button)e.Row.FindControl("btnRestaurar");

                if (lnkModificar != null)
                    lnkModificar.Visible = !verInactivos;

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

                int idCliente;

                if (!int.TryParse(hfIdClienteEliminar.Value, out idCliente))
                {
                    mostrarMensaje("No se pudo identificar el cliente a eliminar.", true);
                    return;
                }

                ClienteNegocio negocio = new ClienteNegocio();
                negocio.eliminar(idCliente);

                hfIdClienteEliminar.Value = "";

                dgvClientes.PageIndex = 0;
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
            lblMensaje.CssClass = "message text-danger";

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); if (mensaje) { mensaje.innerHTML = ''; } }, 4000);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ocultarMensajeCliente", script, true);
        }
    }
}