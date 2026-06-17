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
            dgvArticulos.DataSource = listaOrdenada;
            dgvArticulos.DataBind();
        }

    }
}