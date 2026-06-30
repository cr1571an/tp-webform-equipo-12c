using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppGestionNegocio.Web
{
    public partial class CompraFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                MedioPagoNegocio medioPagoNegocio = new MedioPagoNegocio();

                ddlProveedor.DataSource = proveedorNegocio.listar();
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "IdProveedor";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("-- Seleccione un proveedor --", "0"));

                ddlMedio.DataSource = medioPagoNegocio.listar();
                ddlMedio.DataTextField = "Nombre";
                ddlMedio.DataValueField = "IdMedioPago";
                ddlMedio.DataBind();
                ddlMedio.Items.Insert(0, new ListItem("-- Seleccione un medio de pago --", "0"));

                ddlArticulo.Items.Insert(0, new ListItem("Seleccione un artículo", "0"));

                txtFechaCompra.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtCantidad.Text = "";
                txtPrecioUnitario.Text = "";
                txtSubtotal.Text = "";

                Session.Remove("ArticuloSeleccionado");
                Session.Remove("DetallesCompra");

                gvDetalle.DataSource = null;
                gvDetalle.DataBind();
                lblTotal.Text = "$ 0,00";
            }
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";
            txtSubtotal.Text = "";

            Session.Remove("ArticuloSeleccionado");
            Session.Remove("DetallesCompra");

            gvDetalle.DataSource = null;
            gvDetalle.DataBind();
            lblTotal.Text = "$ 0,00";

            int idProveedor = int.Parse(ddlProveedor.SelectedValue);

            ArticuloNegocio negocio = new ArticuloNegocio();
            List<ArticuloProveedorDto> articulos = negocio.listarPorProveedor(idProveedor);
            Session["ArticulosProveedor"] = articulos;

            ddlArticulo.DataSource = articulos;
            ddlArticulo.DataTextField = "Nombre";
            ddlArticulo.DataValueField = "IdArticulo";
            ddlArticulo.DataBind();
            ddlArticulo.Items.Insert(0, new ListItem("Seleccione un artículo", "0"));
        }

        protected void ddlArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<ArticuloProveedorDto> articulos = (List<ArticuloProveedorDto>)Session["ArticulosProveedor"];

            int idArticulo = int.Parse(ddlArticulo.SelectedValue);

            ArticuloProveedorDto articulo = articulos.FirstOrDefault(a => a.IdArticulo == idArticulo);

            if (articulo != null)
            {
                Session["ArticuloSeleccionado"] = articulo;
                txtPrecioUnitario.Text = articulo.PrecioUnitario.ToString("F2", CultureInfo.InvariantCulture);
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            lblMensajeDetalle.Visible = false;
            lblMensajeDetalle.Text = string.Empty;

            lblMensajeDatos.Visible = false;
            lblMensajeDatos.Text = string.Empty;

            if (ddlProveedor.SelectedValue == "0")
            {
                MostrarMensaje(lblMensajeDatos, "Debe seleccionar un proveedor.");
                return;
            }

            if (ddlArticulo.SelectedValue == "0")
            {
                MostrarMensaje(lblMensajeDetalle, "Debe seleccionar un artículo.");
                return;
            }


            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MostrarMensaje(lblMensajeDetalle, "Debe ingresar un cantidad valida.");
                return;
            }

            if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precioCompra) || precioCompra <= 0)
            {
                MostrarMensaje(lblMensajeDetalle, "Debe ingresar un precio valido valida.");
                return;
            }

            List<DetalleCompraDto> detalles;

            if (Session["DetallesCompra"] == null)
                detalles = new List<DetalleCompraDto>();
            else
                detalles = (List<DetalleCompraDto>)Session["DetallesCompra"];

            ArticuloProveedorDto articulo = (ArticuloProveedorDto)Session["ArticuloSeleccionado"];

            var existente = detalles.FirstOrDefault(x => x.IdArticulo == articulo.IdArticulo);

            if (existente != null)
            {

                existente.Cantidad += cantidad;
                existente.PrecioUnitario = precioCompra;
                existente.Subtotal = existente.Cantidad * existente.PrecioUnitario;

            }
            else
            {
                detalles.Add(new DetalleCompraDto
                {
                    IdArticulo = articulo.IdArticulo,
                    NombreArticulo = articulo.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = precioCompra,
                    Subtotal = cantidad * precioCompra
                });
            }

            Session["DetallesCompra"] = detalles;

            ActualizarGrilla();

            ddlArticulo.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            txtPrecioUnitario.Text = string.Empty;
            txtSubtotal.Text = string.Empty;

            Session.Remove("ArticuloSeleccionado");
        }

        private void ActualizarGrilla()
        {
            var detalles = (List<DetalleCompraDto>)Session["DetallesCompra"];

            gvDetalle.DataSource = detalles;
            gvDetalle.DataBind();

            decimal total = detalles.Sum(x => x.Subtotal);

            lblTotal.Text = "$ " + total.ToString("N2");
        }

        protected void gvDetalle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetalle.EditIndex = e.NewEditIndex;
            gvDetalle.DataSource = (List<DetalleCompraDto>)Session["DetallesCompra"];
            gvDetalle.DataBind();
        }

        protected void gvDetalle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetalle.EditIndex = -1;
            gvDetalle.DataSource = (List<DetalleCompraDto>)Session["DetallesCompra"];
            gvDetalle.DataBind();
        }

        protected void gvDetalle_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMensajeDetalle.Visible = false;
            lblMensajeDetalle.Text = string.Empty;

            List<DetalleCompraDto> detalles = (List<DetalleCompraDto>)Session["DetallesCompra"];

            int idArticulo = Convert.ToInt32(gvDetalle.DataKeys[e.RowIndex].Value);

            GridViewRow fila = gvDetalle.Rows[e.RowIndex];

            TextBox txtCantidadEdit = (TextBox)fila.FindControl("txtCantidadEdit");

            int cantidad = Convert.ToInt32(txtCantidadEdit.Text);

            if (cantidad <= 0)
            {
                MostrarMensaje(lblMensajeDetalle, "Debe ingresar un cantidad valida.");

                gvDetalle.DataSource = detalles;
                gvDetalle.DataBind();
                return;
            }

            DetalleCompraDto detalle = detalles.FirstOrDefault(x => x.IdArticulo == idArticulo);


            if (detalle == null)
            {
                MostrarMensaje(
                    lblMensajeDetalle,
                    "No se encontró el artículo seleccionado.");

                gvDetalle.EditIndex = -1;

                gvDetalle.DataSource = detalles;
                gvDetalle.DataBind();

                return;
            }

            detalle.Cantidad = cantidad;
            detalle.Subtotal =
            cantidad * detalle.PrecioUnitario;


            Session["DetallesCompra"] = detalles;

            gvDetalle.EditIndex = -1;

            gvDetalle.DataSource = detalles;
            gvDetalle.DataBind();
            decimal total = detalles.Sum(x => x.Subtotal);

            lblTotal.Text = "$ " + total.ToString("N2");

        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AbrirModalEliminar")
            {
                hfIdArticuloEliminar.Value =
                    e.CommandArgument.ToString();

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "abrirModalEliminar",
                    "$('#modalEliminarArticulo').modal('show');",
                    true);
            }
        }

        protected void btnConfirmarEliminarArticulo_Click(object sender, EventArgs e)
        {
            int idArticulo = Convert.ToInt32(hfIdArticuloEliminar.Value);

            List<DetalleCompraDto> detalles = (List<DetalleCompraDto>)Session["DetallesCompra"];

            if (detalles == null)
                return;

            var detalle = detalles.FirstOrDefault(x => x.IdArticulo == idArticulo);

            if (detalle != null)
            {
                detalles.Remove(detalle);
            }

            Session["DetallesCompra"] = detalles;

            ActualizarGrilla();
        }

        protected void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            Session.Remove("DetallesCompra");
            Session.Remove("ArticuloSeleccionado");
            Session.Remove("ArticulosProveedor");
            Response.Redirect("Compras.aspx");
        }

        protected void btnGuardarCompra_Click(object sender, EventArgs e)
        {
            lblMensajeDetalle.Visible = false;
            lblMensajeDetalle.Text = string.Empty;

            lblMensajeDatos.Visible = false;
            lblMensajeDatos.Text = string.Empty;

            if (ddlProveedor.SelectedValue == "0")
            {
                MostrarMensaje(lblMensajeDatos, "Debe seleccionar un proveedor.");
                return;
            }

            if (ddlMedio.SelectedValue == "0")
            {
                MostrarMensaje(lblMensajeDatos, "Debe seleccionar un medio de pago.");
                return;
            }

            if (!DateTime.TryParse(txtFechaCompra.Text, out DateTime fechaCompra))
            {
                MostrarMensaje(lblMensajeDatos, "Debe ingresar una fecha válida.");
                return;
            }

            if (fechaCompra.Date > DateTime.Today)
            {
                MostrarMensaje(lblMensajeDatos, "La fecha no puede ser posterior a hoy.");

                return;
            }

            if (fechaCompra.Date < DateTime.Today.AddMonths(-2))
            {
                MostrarMensaje(lblMensajeDatos, "La fecha no puede superar los dos meses de antigüedad.");
                return;
            }

            string numeroComprobante = txtNumeroComprobante.Text.Trim();

            if (string.IsNullOrWhiteSpace(numeroComprobante))
            {
                MostrarMensaje(lblMensajeDatos, "Debe ingresar un número de comprobante.");
                return;
            }

            List<DetalleCompraDto> detalles = (List<DetalleCompraDto>)Session["DetallesCompra"];

            if (detalles == null || !detalles.Any())
            {
                MostrarMensaje(lblMensajeDetalle, "Debe agregar al menos un artículo.");
                return;
            }

            foreach (var detalle in detalles)
            {
                if (detalle.Cantidad <= 0)
                {
                    MostrarMensaje(lblMensajeDetalle,"Existe un artículo con cantidad inválida.");
                    return;
                }

                if (detalle.PrecioUnitario <= 0)
                {
                    MostrarMensaje(lblMensajeDetalle,"Existe un artículo con precio inválido.");
                    return;
                }
            }

            int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

            int idMedioPago = Convert.ToInt32(ddlMedio.SelectedValue);

            string observaciones = txtObservaciones.Text.Trim();

            CompraDto compra = new CompraDto
            {
                IdUsuario = 1, //TO DO: quitar valor hardcodeado!!
                IdProveedor = int.Parse(ddlProveedor.SelectedValue),
                IdMedioPago = int.Parse(ddlMedio.SelectedValue),
                FechaCompra = fechaCompra,
                NumeroComprobante = txtNumeroComprobante.Text.Trim(),
                Observaciones = txtObservaciones.Text.Trim(),
                Total = detalles.Sum(x => x.Subtotal),
                Detalles = detalles
            };

            try
            {
                CompraNegocio compraNegocio = new CompraNegocio();

                compraNegocio.Guardar(compra);

                Session.Remove("DetallesCompra");
                Session.Remove("ArticuloSeleccionado");
                Session.Remove("ArticulosProveedor");

                Response.Redirect(
                    "Compras.aspx?mensaje=Compra registrada correctamente");
            }
            catch (Exception ex)
            {
                MostrarMensaje(lblMensajeDatos,"Ocurrió un error al registrar la compra.");
            }

        }

        private void MostrarMensaje(Label lbl, string mensaje, string cssClass = "alert alert-warning")
        {
            lbl.Text = mensaje;
            lbl.CssClass = cssClass;
            lbl.Visible = true;

            string script =
                "setTimeout(function() {" +
                "var mensaje = document.getElementById('" + lbl.ClientID + "');" +
                "if (mensaje) {" +
                "mensaje.style.display = 'none';" +
                "}" +
                "}, 4000);";

            ClientScript.RegisterStartupScript(
                GetType(),
                Guid.NewGuid().ToString(),
                script,
                true);
        }
    }
}