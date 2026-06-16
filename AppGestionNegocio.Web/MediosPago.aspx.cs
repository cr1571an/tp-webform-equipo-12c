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
    public partial class MediosPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarMediosPago();
            }
        }
        private void cargarMediosPago()
        {
            MedioPagoNegocio negocio = new MedioPagoNegocio();

            string nombre = txtFiltroNombre.Text.Trim();

            dgvMediosPago.DataSource = negocio.filtrar(nombre);
            dgvMediosPago.DataBind();
        }
        private void mostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;

            string script = "setTimeout(function() { " + "var mensaje = document.getElementById('" + lblMensaje.ClientID + "'); " + "if (mensaje) { mensaje.innerHTML = ''; } " + "}, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje", script, true);
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    mostrarMensaje("Debe ingresar el nombre del medio de pago.");
                    return;
                }

                MedioPago medioPago = new MedioPago();
                medioPago.Nombre = txtNombre.Text.Trim();
                medioPago.Descripcion = txtDescripcion.Text.Trim();
                medioPago.Activo = true;

                MedioPagoNegocio negocio = new MedioPagoNegocio();
                negocio.agregar(medioPago);

                txtNombre.Text = "";
                txtDescripcion.Text = "";
                txtFiltroNombre.Text = "";

                cargarMediosPago();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al agregar el medio de pago: " + ex.Message);
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarMediosPago();
        }
        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            cargarMediosPago();
        }
        protected void dgvMediosPago_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvMediosPago.EditIndex = e.NewEditIndex;
            cargarMediosPago();
        }
        protected void dgvMediosPago_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvMediosPago.EditIndex = -1;
            cargarMediosPago();
        }
        protected void dgvMediosPago_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                int idMedioPago = (int)dgvMediosPago.DataKeys[e.RowIndex].Value;

                GridViewRow fila = dgvMediosPago.Rows[e.RowIndex];

                TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");
                TextBox txtDescripcionEdit = (TextBox)fila.FindControl("txtDescripcionEdit");

                if (string.IsNullOrWhiteSpace(txtNombreEdit.Text))
                {
                    mostrarMensaje("Debe ingresar el nombre del medio de pago.");
                    return;
                }

                MedioPago medioPago = new MedioPago();
                medioPago.IdMedioPago = idMedioPago;
                medioPago.Nombre = txtNombreEdit.Text.Trim();
                medioPago.Descripcion = txtDescripcionEdit.Text.Trim();
                medioPago.Activo = true;

                MedioPagoNegocio negocio = new MedioPagoNegocio();
                negocio.modificar(medioPago);

                dgvMediosPago.EditIndex = -1;
                cargarMediosPago();
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al modificar el medio de pago: " + ex.Message);
            }
        }
        protected void dgvMediosPago_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (e.CommandName == "EliminarMedioPago")
                {
                    int idMedioPago = int.Parse(e.CommandArgument.ToString());

                    MedioPagoNegocio negocio = new MedioPagoNegocio();
                    negocio.eliminar(idMedioPago);

                    dgvMediosPago.EditIndex = -1;
                    cargarMediosPago();
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje("Error al eliminar el medio de pago: " + ex.Message);
            }
        }
    }
}