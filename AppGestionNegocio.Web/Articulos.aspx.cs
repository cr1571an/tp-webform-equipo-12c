using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppGestionNegocio.Web
{
    public partial class Articulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                contenedorInactivos.Visible = Seguridad.esAdmin(Session["usuario"]);
                CargarGridV();
            }
        }

        private void CargarGridV()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

                btnNuevoArticulo.Visible = !verInactivos;

                if (verInactivos)
                {
                    lblTituloListado.Text = "Artículos inactivos";
                    dgvArticulos.EmptyDataText = "No hay artículos inactivos registrados.";
                }
                else
                {
                    lblTituloListado.Text = "Artículos registrados";
                    dgvArticulos.EmptyDataText = "No hay artículos activos registrados.";
                }

                List<Articulo> lista = negocio.listarOrdenado(ddlFiltro.SelectedValue, verInactivos);

                Session["listaArticulos"] = lista;

                dgvArticulos.DataSource = lista;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al cargar artículos: " + ex.Message, true);
            }
        }

        protected void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ArticuloFormulario.aspx");
        }

        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            dgvArticulos.PageIndex = 0;
            CargarGridV();
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;

            if (Session["listaArticulos"] != null)
            {
                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();
            }
            else
            {
                CargarGridV();
            }
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvArticulos.PageIndex = 0;
            CargarGridV();
        }

        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idArticulo = int.Parse(e.CommandArgument.ToString());

                    hfIdArticuloEliminar.Value = idArticulo.ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalEliminarArticulo", "$('#modalEliminarArticulo').modal('show');", true);
                }

                if (e.CommandName == "Restaurar")
                {
                    int idArticulo = int.Parse(e.CommandArgument.ToString());

                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.restaurar(idArticulo);

                    CargarGridV();

                    mostrarMensaje("Artículo restaurado correctamente.", false);
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al seleccionar el artículo: " + ex.Message, true);
            }
        }

        protected void dgvArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
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

                int idArticulo;

                if (!int.TryParse(hfIdArticuloEliminar.Value, out idArticulo))
                {
                    mostrarMensaje("No se pudo identificar el artículo a eliminar.", true);
                    return;
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.eliminarLogico(idArticulo);

                hfIdArticuloEliminar.Value = "";

                dgvArticulos.PageIndex = 0;
                CargarGridV();

                mostrarMensaje("Artículo eliminado correctamente.", false);
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar artículo: " + ex.Message, true);
            }
        }

        private void mostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esError ? "message text-danger d-block" : "message text-success d-block";
            lblMensaje.Style["display"] = "block";

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); if (mensaje) { mensaje.innerHTML = ''; mensaje.classList.remove('d-block'); mensaje.classList.add('d-none'); mensaje.style.display = 'none'; } }, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensajeArticulo", script, true);
        }
    }
}