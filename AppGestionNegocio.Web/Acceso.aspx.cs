using System;
using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;

namespace AppGestionNegocio.Web
{
    public partial class Acceso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["logout"] == "1")
                {
                    Session.Clear();
                    Session.Abandon();
                    return;
                }

                if (Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Inicio.aspx", false);
                    return;
                }
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtContrasenia.Text))
                {
                    mostrarMensaje("Debés completar usuario/correo y contraseña.");
                    return;
                }

                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.Nombre = txtCorreo.Text.Trim();
                usuario.PasswordHash = txtContrasenia.Text.Trim();

                if (negocio.Login(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Inicio.aspx", false);
                }
                else
                {
                    mostrarMensaje("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception)
            {
                mostrarMensaje("Ocurrió un error al intentar iniciar sesión.");
            }
        }

        private void mostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); if (mensaje) { mensaje.style.display = 'none'; } }, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje", script, true);
        }
    }
}