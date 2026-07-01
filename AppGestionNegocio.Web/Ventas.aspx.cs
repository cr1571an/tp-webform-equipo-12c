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
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGridView();
            }
        }

        private void CargarGridView()
        {
            VentaNegocio negocio = new VentaNegocio();

            Session["listaVentas"] = negocio.listar();

            dgvVentas.DataSource = Session["listaVentas"];
            dgvVentas.DataBind();
        }

        protected void dgvVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvVentas.PageIndex = e.NewPageIndex;
            dgvVentas.DataSource = Session["listaVentas"];
            dgvVentas.DataBind();
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            VentaNegocio negocio = new VentaNegocio();
            string criterio = ddlFiltro.SelectedValue;

            Session["listaVentas"] = negocio.listarOrdenadoPorFecha(criterio);

            dgvVentas.DataSource = Session["listaVentas"];
            dgvVentas.DataBind();
        }

        protected void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("VentaFormulario.aspx");
        }

        protected void dgvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AbrirModalEliminar")
            {
                string idVenta = e.CommandArgument.ToString();
                hfIdVentaEliminar.Value = idVenta;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#modalEliminarVenta').modal('show');", true);
            }
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idVenta = int.Parse(hfIdVentaEliminar.Value);
                VentaNegocio negocio = new VentaNegocio();
                negocio.eliminar(idVenta); 
                CargarGridView();
                hfIdVentaEliminar.Value = "";
            }
            catch (Exception ex)
            {
                Session["Error"] = "Error al intentar cancelar la venta: " + ex.Message;
            }
        }
    }
}