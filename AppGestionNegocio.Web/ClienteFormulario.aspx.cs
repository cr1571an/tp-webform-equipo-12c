using System;
using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;

namespace AppGestionNegocio.Web
{
    public partial class ClienteFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idCliente;

                    if (int.TryParse(Request.QueryString["id"], out idCliente))
                    {
                        ViewState["IdCliente"] = idCliente;
                        lblTitulo.Text = "Modificar cliente";
                        btnGuardar.Text = "Guardar cambios";

                        cargarCliente(idCliente);
                    }
                    else
                    {
                        mostrarMensaje("El cliente indicado no es válido.");
                    }
                }
                else
                {
                    lblTitulo.Text = "Registrar cliente";
                    btnGuardar.Text = "Guardar cliente";
                }
            }
        }

        private void cargarCliente(int idCliente)
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();
                Cliente cliente = negocio.obtenerPorId(idCliente);

                if (cliente == null)
                {
                    mostrarMensaje("No se encontró el cliente seleccionado.");
                    return;
                }

                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtCuit.Text = cliente.Cuit;
                txtTelefono.Text = cliente.Telefono;
                txtEmail.Text = cliente.Email;
                txtCodigoPostal.Text = cliente.Cp;
                txtDomicilio.Text = cliente.Domicilio;

                if (cliente.CondicionIva != null)
                {
                    string idCondicion = cliente.CondicionIva.IdCondicionIva.ToString();

                    if (ddlCondicionIva.Items.FindByValue(idCondicion) != null)
                    {
                        ddlCondicionIva.SelectedValue = idCondicion;
                    }
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al cargar el cliente: " + ex.Message);
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

                Cliente cliente = new Cliente();

                cliente.Nombre = txtNombre.Text.Trim();
                cliente.Apellido = txtApellido.Text.Trim();
                cliente.Cuit = txtCuit.Text.Trim();
                cliente.Telefono = txtTelefono.Text.Trim();
                cliente.Email = txtEmail.Text.Trim();
                cliente.Cp = txtCodigoPostal.Text.Trim();
                cliente.Domicilio = txtDomicilio.Text.Trim();
                cliente.Activo = true;

                cliente.CondicionIva = new CondicionIva();
                cliente.CondicionIva.IdCondicionIva = int.Parse(ddlCondicionIva.SelectedValue);

                ClienteNegocio negocio = new ClienteNegocio();

                if (ViewState["IdCliente"] != null)
                {
                    cliente.IdCliente = (int)ViewState["IdCliente"];
                    negocio.modificar(cliente);
                }
                else
                {
                    negocio.agregar(cliente);
                }

                Response.Redirect("Clientes.aspx");
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al guardar el cliente: " + ex.Message);
            }
        }

        private bool validarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                mostrarMensaje("Debe ingresar el nombre del cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                mostrarMensaje("Debe ingresar el apellido del cliente.");
                return false;
            }

            if (ddlCondicionIva.SelectedValue != "3" && string.IsNullOrWhiteSpace(txtCuit.Text))
            {
                mostrarMensaje("Debe ingresar el CUIT del cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                mostrarMensaje("Debe ingresar el teléfono del cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                mostrarMensaje("Debe ingresar el email del cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodigoPostal.Text))
            {
                mostrarMensaje("Debe ingresar el código postal del cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDomicilio.Text))
            {
                mostrarMensaje("Debe ingresar el domicilio del cliente.");
                return false;
            }

            return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx");
        }

        private void mostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;

            string script = "setTimeout(function() { " + "var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); " + "if (mensaje) { mensaje.innerHTML = ''; } " + "}, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensajeCliente", script, true);
        }
    }
}