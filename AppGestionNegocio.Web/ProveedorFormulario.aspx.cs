using System;
using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;

namespace AppGestionNegocio.Web
{
    public partial class ProveedorFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idProveedor;

                    if (int.TryParse(Request.QueryString["id"], out idProveedor))
                    {
                        ViewState["IdProveedor"] = idProveedor;
                        lblTitulo.Text = "Modificar proveedor";
                        btnGuardar.Text = "Guardar cambios";

                        cargarProveedor(idProveedor);
                    }
                    else
                    {
                        mostrarMensaje("El proveedor indicado no es válido.");
                    }
                }
                else
                {
                    lblTitulo.Text = "Registrar proveedor";
                    btnGuardar.Text = "Guardar proveedor";
                }
            }
        }

        private void cargarProveedor(int idProveedor)
        {
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();
                Proveedor proveedor = negocio.obtenerPorId(idProveedor);

                if (proveedor == null)
                {
                    mostrarMensaje("No se encontró el proveedor seleccionado.");
                    return;
                }

                txtNombre.Text = proveedor.Nombre;
                txtCuit.Text = proveedor.Cuit;
                txtTelefono.Text = proveedor.Telefono;
                txtEmail.Text = proveedor.Email;
                txtDomicilio.Text = proveedor.Domicilio;
                txtCp.Text = proveedor.Cp;
                txtPersonaContacto.Text = proveedor.PersonaContacto;
                txtObservaciones.Text = proveedor.Observaciones;
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al cargar el proveedor: " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (!validarCampos())
                {
                    return;
                }

                int? idProveedorActual = null;

                if (ViewState["IdProveedor"] != null)
                {
                    idProveedorActual = (int)ViewState["IdProveedor"];
                }

                ProveedorNegocio negocio = new ProveedorNegocio();

                if (negocio.existeNombre(txtNombre.Text.Trim(), idProveedorActual))
                {
                    mostrarMensaje("Error: Ya existe un proveedor registrado con ese nombre.");
                    return;
                }

                if (negocio.existeCuit(txtCuit.Text.Trim(), idProveedorActual))
                {
                    mostrarMensaje("Error: Ya existe un proveedor registrado con ese CUIT.");
                    return;
                }

                if (negocio.existeEmail(txtEmail.Text.Trim(), idProveedorActual))
                {
                    mostrarMensaje("Error: Ya existe un proveedor registrado con ese email.");
                    return;
                }

                Proveedor proveedor = new Proveedor();

                proveedor.Nombre = txtNombre.Text.Trim();
                proveedor.Cuit = txtCuit.Text.Trim();
                proveedor.Telefono = txtTelefono.Text.Trim();
                proveedor.Email = txtEmail.Text.Trim();
                proveedor.Domicilio = txtDomicilio.Text.Trim();
                proveedor.Cp = txtCp.Text.Trim();
                proveedor.PersonaContacto = txtPersonaContacto.Text.Trim();
                proveedor.Observaciones = txtObservaciones.Text.Trim();
                proveedor.Activo = true;

                if (ViewState["IdProveedor"] != null)
                {
                    proveedor.IdProveedor = (int)ViewState["IdProveedor"];
                    negocio.modificar(proveedor);
                }
                else
                {
                    negocio.agregar(proveedor);
                }

                Response.Redirect("Proveedores.aspx");
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al guardar el proveedor: " + ex.Message);
            }
        }

        private bool validarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                mostrarMensaje("Error: Debe ingresar el nombre del proveedor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCuit.Text))
            {
                mostrarMensaje("Error: Debe ingresar el CUIT del proveedor.");
                return false;
            }

            if (!cuitValido(txtCuit.Text.Trim()))
            {
                mostrarMensaje("Error: El CUIT debe tener el formato 30-12345678-9.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                mostrarMensaje("Error: Debe ingresar el teléfono del proveedor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                mostrarMensaje("Error: Debe ingresar el email del proveedor.");
                return false;
            }

            if (!emailValido(txtEmail.Text.Trim()))
            {
                mostrarMensaje("Error: El email ingresado no tiene un formato válido.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDomicilio.Text))
            {
                mostrarMensaje("Error: Debe ingresar el domicilio del proveedor.");
                return false;
            }

            return true;
        }

        private bool cuitValido(string cuit)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(cuit, @"^\d{2}-\d{8}-\d{1}$");
        }

        private bool emailValido(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Proveedores.aspx");
        }

        private void mostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Style["display"] = "block";

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); if (mensaje) { mensaje.innerHTML = ''; mensaje.style.display = 'none'; } }, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje", script, true);
        }
    }
}