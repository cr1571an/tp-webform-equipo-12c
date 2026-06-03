using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                try
                {
                    ClienteNegocio negocio = new ClienteNegocio();
                    Session.Add("listaClientes", negocio.listar());
                    dgvClientes.DataSource = Session["listaClientes"];
                    dgvClientes.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                }
            }
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.DataSource = Session["listaClientes"];
            dgvClientes.PageIndex = e.NewPageIndex;
            dgvClientes.DataBind();
        }
    }
}