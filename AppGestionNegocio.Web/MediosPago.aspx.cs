using System;
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
                contenedorInactivos.Visible = Seguridad.esAdmin(Session["usuario"]);
                cargarMediosPago();
            }
        }

        private void cargarMediosPago()
        {
            MedioPagoNegocio negocio = new MedioPagoNegocio();

            bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

            cardNuevoMedioPago.Visible = !verInactivos;

            if (verInactivos)
            {
                lblTituloListado.Text = "Medios de pago inactivos";
                dgvMediosPago.EmptyDataText = "No hay medios de pago inactivos registrados.";
            }
            else
            {
                lblTituloListado.Text = "Medios de pago registrados";
                dgvMediosPago.EmptyDataText = "No hay medios de pago activos registrados.";
            }

            dgvMediosPago.DataSource = negocio.listar(verInactivos);
            dgvMediosPago.DataBind();
        }

        private void mostrarMensaje(Label lbl, string mensaje)
        {
            lbl.Text = mensaje;

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lbl.ClientID + "'); if (mensaje) { mensaje.innerHTML = ''; } }, 4000);";

            ClientScript.RegisterStartupScript(this.GetType(), "ocultarMensaje" + lbl.ClientID, script, true);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    mostrarMensaje(lblMensaje, "Debe ingresar el nombre del medio de pago.");
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

                cargarMediosPago();
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensaje, "Error al agregar el medio de pago: " + ex.Message);
            }
        }

        protected void chkVerInactivos_CheckedChanged(object sender, EventArgs e)
        {
            dgvMediosPago.EditIndex = -1;
            lblMensaje.Text = "";
            lblMensajeListado.Text = "";
            cargarMediosPago();
        }

        protected void dgvMediosPago_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (chkVerInactivos.Checked)
            {
                e.Cancel = true;
                return;
            }

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
                    mostrarMensaje(lblMensaje, "Debe ingresar el nombre del medio de pago.");
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
                mostrarMensaje(lblMensaje, "Error al modificar el medio de pago: " + ex.Message);
            }
        }

        protected void dgvMediosPago_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";
                lblMensajeListado.Text = "";

                if (e.CommandName == "AbrirModalEliminar")
                {
                    int idMedioPago = int.Parse(e.CommandArgument.ToString());

                    hfIdMedioPagoEliminar.Value = idMedioPago.ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalEliminarMedioPago", "$('#modalEliminarMedioPago').modal('show');", true);
                }

                if (e.CommandName == "Restaurar")
                {
                    int idMedioPago = int.Parse(e.CommandArgument.ToString());

                    MedioPagoNegocio negocio = new MedioPagoNegocio();
                    negocio.restaurar(idMedioPago);

                    cargarMediosPago();

                    mostrarMensaje(lblMensajeListado, "Medio de pago restaurado correctamente.");
                }
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensajeListado, "Error al seleccionar el medio de pago: " + ex.Message);
            }
        }

        protected void dgvMediosPago_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool verInactivos = Seguridad.esAdmin(Session["usuario"]) && chkVerInactivos.Checked;

                Button btnEditar = (Button)e.Row.FindControl("btnEditar");
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");
                Button btnRestaurar = (Button)e.Row.FindControl("btnRestaurar");

                if (btnEditar != null)
                    btnEditar.Visible = !verInactivos;

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
                lblMensajeListado.Text = "";

                int idMedioPago;

                if (!int.TryParse(hfIdMedioPagoEliminar.Value, out idMedioPago))
                {
                    mostrarMensaje(lblMensaje, "No se pudo identificar el medio de pago a eliminar.");
                    return;
                }

                MedioPagoNegocio negocio = new MedioPagoNegocio();
                negocio.eliminar(idMedioPago);

                hfIdMedioPagoEliminar.Value = "";

                dgvMediosPago.EditIndex = -1;
                cargarMediosPago();

                mostrarMensaje(lblMensaje, "Medio de pago eliminado correctamente.");
            }
            catch (Exception ex)
            {
                mostrarMensaje(lblMensaje, "Error al eliminar el medio de pago: " + ex.Message);
            }
        }
    }
}