using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;
using System;
using System.Collections.Generic;
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
                contenedorInactivos.Visible = Seguridad.esAdmin(Session["usuario"]);
                cargarRoles();
                cargarUsuarios();
            }
        }

        private void cargarRoles()
        {
            RolNegocio rolNegocio = new RolNegocio();

            ddlFiltroRol.DataSource = rolNegocio.listar();
            ddlFiltroRol.DataTextField = "Nombre";
            ddlFiltroRol.DataValueField = "IdRol";
            ddlFiltroRol.DataBind();

            ddlFiltroRol.Items.Insert(0, new ListItem("Todos los roles", "0"));
        }

        private void cargarUsuarios()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                int idRol = int.Parse(ddlFiltroRol.SelectedValue);
                bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

                lnkNuevoUsuario.Visible = !verInactivos;

                if (verInactivos)
                {
                    lblTituloListado.Text = "Usuarios inactivos";
                    dgvUsuarios.EmptyDataText = "No hay usuarios inactivos registrados.";
                }
                else
                {
                    lblTituloListado.Text = "Usuarios registrados";
                    dgvUsuarios.EmptyDataText = "No hay usuarios activos registrados.";
                }

                List<Usuario> lista = negocio.filtrar(idRol, verInactivos);

                Session["listaUsuarios"] = lista;

                dgvUsuarios.DataSource = lista;
                dgvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al cargar usuarios: " + ex.Message);
            }
        }

        protected void ddlFiltroRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvUsuarios.PageIndex = 0;
            cargarUsuarios();
        }

        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            dgvUsuarios.PageIndex = 0;
            cargarUsuarios();
        }

        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.PageIndex = e.NewPageIndex;

            if (Session["listaUsuarios"] != null)
            {
                dgvUsuarios.DataSource = Session["listaUsuarios"];
                dgvUsuarios.DataBind();
            }
            else
            {
                cargarUsuarios();
            }
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idUsuario = int.Parse(e.CommandArgument.ToString());

                    hfIdUsuarioEliminar.Value = idUsuario.ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalEliminarUsuario", "$('#modalEliminarUsuario').modal('show');", true);
                }

                if (e.CommandName == "Restaurar")
                {
                    int idUsuario = int.Parse(e.CommandArgument.ToString());

                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.restaurar(idUsuario);

                    cargarUsuarios();

                    mostrarMensaje("Usuario restaurado correctamente.");
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al seleccionar el usuario: " + ex.Message);
            }
        }

        protected void dgvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
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

                int idUsuario;

                if (!int.TryParse(hfIdUsuarioEliminar.Value, out idUsuario))
                {
                    mostrarMensaje("No se pudo identificar el usuario a eliminar.");
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.eliminar(idUsuario);

                hfIdUsuarioEliminar.Value = "";

                dgvUsuarios.PageIndex = 0;
                cargarUsuarios();

                mostrarMensaje("Usuario eliminado correctamente.");
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar usuario: " + ex.Message);
            }
        }

        private void mostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "message text-danger";

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); if (mensaje) { mensaje.innerHTML = ''; } }, 4000);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ocultarMensajeUsuario", script, true);
        }
    }
}