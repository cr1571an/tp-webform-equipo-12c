using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppGestionNegocio.Web
{
    public partial class UsuarioFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtFechaIngreso.Attributes["max"] = DateTime.Today.ToString("yyyy-MM-dd");

                if (!IsPostBack)
                {
                    CargarDesplegables();

                    int? id = obtenerIdUsuario();

                    if (id.HasValue)
                    {
                        cargarUsuario(id.Value);
                    }
                    else
                    {
                        lblTituloPagina.Text = "Registrar usuario";
                        btnGuardar.Text = "Guardar usuario";
                        txtFechaIngreso.Text = DateTime.Today.ToString("yyyy-MM-dd");
                        lblPasswordHelper.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                mostrarError("Error al cargar el formulario: " + ex.Message);
            }
        }

        private int? obtenerIdUsuario()
        {
            int idUsuario;

            if (int.TryParse(Request.QueryString["id"], out idUsuario))
            {
                return idUsuario;
            }

            return null;
        }

        private void CargarDesplegables()
        {
            RolNegocio rolNegocio = new RolNegocio();

            ddlRol.DataSource = rolNegocio.listar();
            ddlRol.DataValueField = "IdRol";
            ddlRol.DataTextField = "Nombre";
            ddlRol.DataBind();

            ddlRol.Items.Insert(0, new ListItem("-- Seleccione un rol --", "0"));
        }

        private void cargarUsuario(int idUsuario)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            List<Usuario> lista = negocio.listar(idUsuario);

            if (lista.Count == 0)
            {
                Response.Redirect("Usuarios.aspx", false);
                return;
            }

            Usuario seleccionado = lista[0];
            Session["usuarioSeleccionado"] = seleccionado;

            lblTituloPagina.Text = "Modificar usuario";
            btnGuardar.Text = "Guardar cambios";
            lblPasswordHelper.Visible = true;

            txtEmpleadoNombre.Text = seleccionado.Empleado.Nombre;
            txtEmpleadoApellido.Text = seleccionado.Empleado.Apellido;
            txtEmpleadoDni.Text = seleccionado.Empleado.Dni;
            txtEmpleadoTelefono.Text = seleccionado.Empleado.Telefono;
            txtEmpleadoEmail.Text = seleccionado.Empleado.Email;

            if (seleccionado.Empleado.FechaIngreso != DateTime.MinValue)
            {
                txtFechaIngreso.Text = seleccionado.Empleado.FechaIngreso.ToString("yyyy-MM-dd");
            }
            else
            {
                txtFechaIngreso.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }

            txtNombre.Text = seleccionado.Nombre;
            ddlRol.SelectedValue = seleccionado.Rol.IdRol.ToString();
        }

        private bool ValidarCampo(bool esEdicion)
        {
            lblMensajeError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtEmpleadoNombre.Text))
            {
                mostrarError("Error: Por favor, ingrese el nombre del empleado.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmpleadoApellido.Text))
            {
                mostrarError("Error: Por favor, ingrese el apellido del empleado.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmpleadoDni.Text))
            {
                mostrarError("Error: Por favor, ingrese el DNI del empleado.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmpleadoTelefono.Text))
            {
                mostrarError("Error: Por favor, ingrese el teléfono del empleado.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmpleadoEmail.Text))
            {
                mostrarError("Error: Por favor, ingrese el email del empleado.");
                return false;
            }
                        
            if (string.IsNullOrWhiteSpace(txtFechaIngreso.Text))
            {
                mostrarError("Error: Por favor, ingrese la fecha de ingreso.");
                return false;
            }

            DateTime fechaIngreso;

            if (!DateTime.TryParse(txtFechaIngreso.Text, out fechaIngreso))
            {
                mostrarError("Error: La fecha de ingreso no tiene un formato válido.");
                return false;
            }

            if (fechaIngreso.Date > DateTime.Today)
            {
                mostrarError("Error: La fecha de ingreso no puede ser posterior al día de hoy.");
                return false;
            }

            if (ddlRol.SelectedValue == "0")
            {
                mostrarError("Error: Por favor, seleccione un rol.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                mostrarError("Error: Por favor, ingrese el nombre de usuario.");
                return false;
            }

            bool quiereCambiarPassword = !string.IsNullOrWhiteSpace(txtPassword.Text) || !string.IsNullOrWhiteSpace(txtConfirmPassword.Text);

            if (!esEdicion || quiereCambiarPassword)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    mostrarError("Error: La contraseña es obligatoria.");
                    return false;
                }

                if (txtPassword.Text.Length < 6)
                {
                    mostrarError("Error: La contraseña debe tener al menos 6 caracteres.");
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    mostrarError("Error: Las contraseñas no coinciden.");
                    return false;
                }
            }

            return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int? idUsuario = obtenerIdUsuario();
                bool esEdicion = idUsuario.HasValue;

                if (!ValidarCampo(esEdicion))
                {
                    return;
                }

                DateTime fechaIngreso;

                if (!DateTime.TryParse(txtFechaIngreso.Text, out fechaIngreso))
                {
                    mostrarError("Error: La fecha de ingreso no tiene un formato válido.");
                    return;
                }

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                EmpleadoNegocio empleadoNegocio = new EmpleadoNegocio();

                Usuario seleccionado = null;
                int? idEmpleadoActual = null;

                if (esEdicion)
                {
                    seleccionado = Session["usuarioSeleccionado"] as Usuario;

                    if (seleccionado == null)
                    {
                        List<Usuario> lista = usuarioNegocio.listar(idUsuario.Value);

                        if (lista.Count == 0)
                        {
                            mostrarError("Error: No se pudo encontrar el usuario seleccionado.");
                            return;
                        }

                        seleccionado = lista[0];
                    }

                    idEmpleadoActual = seleccionado.Empleado.IdEmpleado;
                }

                if (empleadoNegocio.existeDni(txtEmpleadoDni.Text.Trim(), idEmpleadoActual))
                {
                    mostrarError("Error: Ya existe un empleado registrado con ese DNI.");
                    return;
                }

                if (empleadoNegocio.existeEmail(txtEmpleadoEmail.Text.Trim(), idEmpleadoActual))
                {
                    mostrarError("Error: Ya existe un empleado registrado con ese email.");
                    return;
                }

                if (usuarioNegocio.existeNombreUsuario(txtNombre.Text.Trim(), idUsuario))
                {
                    mostrarError("Error: Ya existe un usuario registrado con ese nombre.");
                    return;
                }

                Empleado empleado = new Empleado();
                empleado.Nombre = txtEmpleadoNombre.Text.Trim();
                empleado.Apellido = txtEmpleadoApellido.Text.Trim();
                empleado.Telefono = txtEmpleadoTelefono.Text.Trim();
                empleado.Email = txtEmpleadoEmail.Text.Trim();
                empleado.Dni = txtEmpleadoDni.Text.Trim();
                empleado.FechaIngreso = fechaIngreso;
                empleado.Activo = true;

                Usuario usuario = new Usuario();
                usuario.Nombre = txtNombre.Text.Trim();

                usuario.Rol = new Rol();
                usuario.Rol.IdRol = int.Parse(ddlRol.SelectedValue);

                usuario.Activo = true;

                if (esEdicion)
                {
                    empleado.IdEmpleado = seleccionado.Empleado.IdEmpleado;
                    empleadoNegocio.modificar(empleado);

                    usuario.IdUsuario = idUsuario.Value;

                    usuario.Empleado = new Empleado();
                    usuario.Empleado.IdEmpleado = empleado.IdEmpleado;

                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        usuario.PasswordHash = seleccionado.PasswordHash;
                    }
                    else
                    {
                        usuario.PasswordHash = txtPassword.Text;
                    }

                    usuarioNegocio.modificar(usuario);
                }
                else
                {
                    int idEmpleado = empleadoNegocio.agregar(empleado);

                    if (idEmpleado == 0)
                    {
                        mostrarError("Error: No se pudo crear el empleado asociado.");
                        return;
                    }

                    usuario.Empleado = new Empleado();
                    usuario.Empleado.IdEmpleado = idEmpleado;
                    usuario.PasswordHash = txtPassword.Text;

                    usuarioNegocio.agregar(usuario);
                }

                Response.Redirect("Usuarios.aspx", false);
            }
            catch (Exception ex)
            {
                mostrarError("Error al guardar el usuario: " + ex.Message);
            }
        }

        private void mostrarError(string mensaje)
        {
            lblMensajeError.Text = mensaje;
            lblMensajeError.Visible = true;
            lblMensajeError.CssClass = "alert alert-danger d-block";

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lblMensajeError.ClientID + "'); if (mensaje) { mensaje.innerHTML = ''; mensaje.classList.remove('d-block'); mensaje.classList.add('d-none'); } }, 4000);";

            ScriptManager.RegisterStartupScript(upBotones, upBotones.GetType(), "ocultarMensajeUsuarioFormulario", script, true);
        }
    }
}