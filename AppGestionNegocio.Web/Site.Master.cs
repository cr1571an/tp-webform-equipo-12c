using System;
using System.IO;
using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;

namespace AppGestionNegocio.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Acceso.aspx", false);
                return;
            }

            Usuario usuario = (Usuario)Session["usuario"];

            lblNombreUsuario.Text = usuario.Nombre;

            if (usuario.Rol != null)
                lblRolUsuario.Text = usuario.Rol.Nombre;
            else
                lblRolUsuario.Text = "Sin rol";

            if (!string.IsNullOrWhiteSpace(usuario.Nombre))
                lblInicialUsuario.Text = usuario.Nombre.Substring(0, 1).ToUpper();
            else
                lblInicialUsuario.Text = "U";

            lnkUsuarios.Visible = Seguridad.esAdmin(Session["usuario"]);
            lnkRoles.Visible = Seguridad.esAdmin(Session["usuario"]);

            string paginaActual = Path.GetFileName(Request.Url.AbsolutePath);

            if (!Seguridad.esAdmin(Session["usuario"]) && (paginaActual == "Usuarios.aspx" || paginaActual == "UsuarioFormulario.aspx" || paginaActual == "Roles.aspx"))
            {
                Response.Redirect("Inicio.aspx", false);
                return;
            }
        }
    }
}