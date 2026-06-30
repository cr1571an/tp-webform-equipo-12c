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
    public partial class Articulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGridV();
            }
        }

        private void CargarGridV()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Session["listaArticulos"] = negocio.listar();

                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ArticuloFormulario.aspx");
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.DataSource = Session["listaArticulos"];
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataBind();
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            string filtroSeleccionado = ddlFiltro.SelectedValue;
            List<Articulo> listaOrdenada = negocio.listarOrdenado(filtroSeleccionado);

            Session["listaArticulos"] = listaOrdenada;

            dgvArticulos.DataSource = listaOrdenada;
            dgvArticulos.DataBind();
        }

        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idArticulo = int.Parse(e.CommandArgument.ToString());

                    hfIdArticuloEliminar.Value = idArticulo.ToString();

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "abrirModalEliminarArticulo",
                        "$('#modalEliminarArticulo').modal('show');",
                        true
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idArticulo;

                if (!int.TryParse(hfIdArticuloEliminar.Value, out idArticulo))
                {
                    return;
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.eliminarLogico(idArticulo);

                hfIdArticuloEliminar.Value = "";

                CargarGridV();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}