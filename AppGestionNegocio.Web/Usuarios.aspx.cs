using AppGestionNegocio.Negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppGestionNegocio.Web
{
    public partial class Usuarios : System.Web.UI.Page
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
                UsuarioNegocio negocio = new UsuarioNegocio();
                Session["listaUsuarios"] = negocio.listar();

                dgvUsuarios.DataSource = Session["listaUsuarios"];
                dgvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.DataSource = Session["listaUsuarios"];
            dgvUsuarios.PageIndex = e.NewPageIndex;
            dgvUsuarios.DataBind();
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idUsuario = int.Parse(e.CommandArgument.ToString());

                    hfIdUsuarioEliminar.Value = idUsuario.ToString();

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "abrirModalEliminarUsuario",
                        "$('#modalEliminarUsuario').modal('show');",
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
                int idUsuario;

                if (!int.TryParse(hfIdUsuarioEliminar.Value, out idUsuario))
                {
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.eliminar(idUsuario);

                hfIdUsuarioEliminar.Value = "";

                CargarGridV();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}