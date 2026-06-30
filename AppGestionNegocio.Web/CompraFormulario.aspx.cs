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
            }
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProveedor = int.Parse(ddlProveedor.SelectedValue);

            ArticuloNegocio negocio = new ArticuloNegocio();
            List<ArticuloProveedorDto> articulos = negocio.listarPorProveedor(idProveedor);
            Session["ArticulosProveedor"] = articulos;

            ddlArticulo.DataSource = articulos;
            ddlArticulo.DataTextField = "Nombre";
            ddlArticulo.DataValueField = "IdArticulo";
            ddlArticulo.DataBind();
            ddlArticulo.Items.Insert(0, new ListItem("Seleccione un artículo", "0"));

            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";
            txtSubtotal.Text = "";

            Session.Remove("ArticuloSeleccionado");
            Session.Remove("DetallesCompra");

            gvDetalle.DataSource = null;
            gvDetalle.DataBind();

            lblTotal.Text = "$ 0,00";
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
            if (ddlArticulo.SelectedValue == "0")
            {

                lblMensaje.CssClass = "alert alert-warning";
                lblMensaje.Text = "Debe seleccionar un artículo.";
                lblMensaje.Visible = true;
                return;
            }


            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                lblMensaje.CssClass = "alert alert-warning";
                lblMensaje.Text = "Debe ingresar un cantidad valida.";
                lblMensaje.Visible = true;
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
                existente.Subtotal = existente.Cantidad * existente.PrecioUnitario;
            }
            else
            {
                detalles.Add(new DetalleCompraDto
                {
                    IdArticulo = articulo.IdArticulo,
                    NombreArticulo = articulo.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = articulo.PrecioUnitario,
                    Subtotal = cantidad * articulo.PrecioUnitario
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
            List<DetalleCompraDto> detalles = (List<DetalleCompraDto>)Session["DetallesCompra"];

            int idArticulo = Convert.ToInt32(gvDetalle.DataKeys[e.RowIndex].Value);

            GridViewRow fila = gvDetalle.Rows[e.RowIndex];

            TextBox txtCantidadEdit = (TextBox)fila.FindControl("txtCantidadEdit");

            int cantidad = Convert.ToInt32(txtCantidadEdit.Text);

            DetalleCompraDto detalle = detalles.FirstOrDefault(x => x.IdArticulo == idArticulo);

            if (detalle != null)
            {
                detalle.Cantidad = cantidad;
                detalle.Subtotal =
                cantidad * detalle.PrecioUnitario;
            }

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
    }
}