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
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarRoles();
            }
        }

        private void cargarRoles()
        {
            RolNegocio negocio = new RolNegocio();

            string nombre = txtFiltroNombre.Text.Trim();

            dgvRoles.DataSource = negocio.filtrar(nombre);
            dgvRoles.DataBind();
        }

        private void mostrarMensaje(Label lbl, string mensaje)
        {
            lbl.Text = mensaje;

            string script = "setTimeout(function() { " + "var mensaje = document.getElementById('" + lbl.ClientID + "'); " + "if (mensaje) { mensaje.innerHTML = ''; } " + "}, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje", script, true);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    mostrarMensaje(lblMensaje,"Debe ingresar el nombre del rol y descripción.");
                    return;
                }

                Rol rol = new Rol();
                rol.Nombre = txtNombre.Text.Trim();
                rol.Descripcion = txtDescripcion.Text.Trim();
                rol.Activo = true;

                RolNegocio negocio = new RolNegocio();
                negocio.agregar(rol);

                txtNombre.Text = "";
                txtDescripcion.Text = "";
                txtFiltroNombre.Text = "";

                cargarRoles();
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensaje,"Error al agregar el rol: " + ex.Message);
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarRoles();
        }
        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            cargarRoles();
        }
        //protected void dgvRoles_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    dgvRoles.EditIndex = e.NewEditIndex;
        //    cargarRoles();
        //}
        //protected void dgvRoles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    dgvRoles.EditIndex = -1;
        //    cargarRoles();
        //}
        protected void dgvRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //try
            //{
            //    lblMensaje.Text = "";

            //    int idProveedor = (int)dgvRoles.DataKeys[e.RowIndex].Value;

            //    GridViewRow fila = dgvRoles.Rows[e.RowIndex];

            //    TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");
            //    TextBox txtTelefonoEdit = (TextBox)fila.FindControl("txtTelefonoEdit");
            //    TextBox txtEmailEdit = (TextBox)fila.FindControl("txtEmailEdit");

            //    if (string.IsNullOrWhiteSpace(txtNombreEdit.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el nombre del proveedor.");
            //        return;
            //    }

            //    if (string.IsNullOrWhiteSpace(txtTelefonoEdit.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el teléfono del proveedor.");
            //        return;
            //    }

            //    if (string.IsNullOrWhiteSpace(txtEmailEdit.Text))
            //    {
            //        mostrarMensaje("Debe ingresar el email del proveedor.");
            //        return;
            //    }

            //    Proveedor proveedor = new Proveedor();
            //    proveedor.IdProveedor = idProveedor;
            //    proveedor.Nombre = txtNombreEdit.Text.Trim();
            //    proveedor.Telefono = txtTelefonoEdit.Text.Trim();
            //    proveedor.Email = txtEmailEdit.Text.Trim();
            //    proveedor.Activo = true;

            //    RolNegocio negocio = new RolNegocio();
            //    negocio.modificar(proveedor);

            //    dgvRoles.EditIndex = -1;
            //    cargarRoles();
            //}
            //catch (Exception ex)
            //{
            //    mostrarMensaje("Error al modificar el proveedor: " + ex.Message);
            //}
        }
        protected void dgvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RolNegocio negocio = new RolNegocio();
            string id = (e.CommandArgument).ToString();

            if (e.CommandName == "EditarModal")
            {
                
                Rol rol = (negocio.listar(id))[0];

                hfIdRol.Value = rol.IdRol.ToString();
                txtNombreModal.Text = rol.Nombre;
                txtDescripcionModal.Text = rol.Descripcion;


                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "abrirModal",
                    "$('#modalEditar').modal('show');",
                    true);
            }
            else if (e.CommandName == "EliminarRol") {
                int idRol = int.Parse(id);
                negocio.eliminar(idRol);

                //dgvRoles.EditIndex = -1;
                cargarRoles();

            }


            //try
            //{
            //    lblMensaje.Text = "";

            //    if (e.CommandName == "EliminarProveedor")
            //    {
            //        
            //    }
            //}
            //catch (Exception ex)
            //{
            //    mostrarMensaje("Error al eliminar el proveedor: " + ex.Message);
            //}
        }

        protected void btnGuardarModal_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensajeModal.Text = "";
                if (string.IsNullOrWhiteSpace(txtNombreModal.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcionModal.Text))
                {
                    mostrarMensaje(lblMensajeModal, "Nombre y descripción son obligatorios");

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "abrirModal",
                        "$('#modalEditar').modal('show');",
                        true);
                    return;
                }

                Rol rol = new Rol();
                rol.IdRol = int.Parse(hfIdRol.Value);
                rol.Nombre = txtNombreModal.Text.Trim();
                rol.Descripcion = txtDescripcionModal.Text;
                rol.Activo = true;

                RolNegocio negocio = new RolNegocio();
                negocio.modificar(rol);

                txtNombreModal.Text = "";
                txtDescripcionModal.Text = "";
                txtFiltroNombre.Text = "";

                cargarRoles();
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensajeModal,"Error al agregar el proveedor: " + ex.Message);
            }

        }
    }
}