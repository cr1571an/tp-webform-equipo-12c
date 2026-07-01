using AppGestionNegocio.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppGestionNegocio.Web
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                cargarCompras();

            }
        }

        private void cargarCompras()
        {
            CompraNegocio negocio = new CompraNegocio();

            dgvCompras.DataSource = negocio.listar();
            dgvCompras.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void dgvCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void dgvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}