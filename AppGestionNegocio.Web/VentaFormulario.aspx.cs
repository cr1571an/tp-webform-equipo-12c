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
    public partial class VentaFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDesplegables();

                ddlArticulo.Items.Insert(0, new ListItem("Seleccione un artículo", "0"));

                txtFechaVenta.Text = DateTime.Today.ToString("yyyy-MM-dd");

                Session.Remove("DetallesVenta");
                Session.Remove("ArticulosFiltrados");
                Session.Remove("ArticuloSeleccionado");
            }
        }

        private void CargarDesplegables()
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            ddlCliente.DataSource = clienteNegocio.listar();
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("-- Seleccione un cliente --", "0"));

            MedioPagoNegocio medioPagoNegocio = new MedioPagoNegocio();
            ddlMedio.DataSource = medioPagoNegocio.listar();
            ddlMedio.DataValueField = "IdMedioPago";
            ddlMedio.DataTextField = "Nombre";
            ddlMedio.DataBind();
            ddlMedio.Items.Insert(0, new ListItem("-- Medio de pago --", "0"));

            CategoriaNegocio catNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = catNegocio.listar();
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione una Categoría --", "0"));
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddlCategoria.SelectedValue);
            ddlArticulo.Items.Clear();
            txtPrecioUnitario.Text = "";
            txtCantidad.Text = "";
            txtStockDisponible.Text = "";

            if (idCategoria > 0)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.listarPorCategoria(idCategoria);
                Session["ArticulosFiltrados"] = lista;

                ddlArticulo.DataSource = lista;
                ddlArticulo.DataValueField = "IdArticulo";
                ddlArticulo.DataTextField = "Nombre";
                ddlArticulo.DataBind();
            }
            ddlArticulo.Items.Insert(0, new ListItem("Seleccione un artículo", "0"));
        }

        protected void ddlArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lista = (List<Articulo>)Session["ArticulosFiltrados"];
            int idArticulo = int.Parse(ddlArticulo.SelectedValue);

            if (lista != null)
            {
                var art = lista.FirstOrDefault(x => x.IdArticulo == idArticulo);
                if (art != null)
                {
                    Session["ArticuloSeleccionado"] = art;

                    txtPrecioUnitario.Text = art.Precio.ToString("F2", CultureInfo.InvariantCulture);
                    txtStockDisponible.Text = art.Stock.ToString();
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lblMensajeDetalle.Text = "";

            if (!validarArticulo()) return;

            var listaDetalle = (List<DetalleVenta>)Session["DetallesVenta"] ?? new List<DetalleVenta>();
            var art = (Articulo)Session["ArticuloSeleccionado"];
            int cant = int.Parse(txtCantidad.Text);

            decimal precio = art.Precio;

            listaDetalle.Add(new DetalleVenta
            {
                Articulo = art,
                Cantidad = cant,
                PrecioUnitario = precio,
                Subtotal = cant * precio
            });

            Session["DetallesVenta"] = listaDetalle;
            ActualizarGrilla();

            txtCantidad.Text = ""; txtPrecioUnitario.Text = ""; txtStockDisponible.Text = "";
            ddlCategoria.SelectedIndex = 0; ddlArticulo.Items.Clear();
            ddlArticulo.Items.Insert(0, new ListItem("Seleccione un artículo", "0"));
        }

        private bool validarArticulo()
        {
            if (ddlCategoria.SelectedValue == "0")
            {
                MostrarMensaje(lblMensajeDetalle, "Debe seleccionar una categoría.");
                return false;
            }

            if (ddlArticulo.SelectedValue == "0")
            {
                MostrarMensaje(lblMensajeDetalle, "Debe seleccionar un artículo.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out int cant) || cant <= 0)
            {
                MostrarMensaje(lblMensajeDetalle, "Debe ingresar una cantidad válida mayor a 0.");
                return false;
            }

            var artSeleccionado = (Articulo)Session["ArticuloSeleccionado"];
            if (artSeleccionado != null)
            {
                if (cant > artSeleccionado.Stock)
                {
                    MostrarMensaje(lblMensajeDetalle, "No hay suficiente stock disponible. Máximo: " + artSeleccionado.Stock);
                    return false;
                }
            }

            var listaDetalle = (List<DetalleVenta>)Session["DetallesVenta"];
            if (listaDetalle != null)
            {
                int idArticuloSeleccionado = int.Parse(ddlArticulo.SelectedValue);
                bool yaExiste = listaDetalle.Any(x => x.Articulo != null && x.Articulo.IdArticulo == idArticuloSeleccionado);

                if (yaExiste)
                {
                    MostrarMensaje(lblMensajeDetalle, "Este artículo ya está en la lista, si quiere modifique la cantidad.");
                    return false;
                }
            }
            return true;
        }

        protected void gvDetalle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblMensajeDetalle.Text = "";
            gvDetalle.EditIndex = e.NewEditIndex;
            ActualizarGrilla();
        }

        protected void gvDetalle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblMensajeDetalle.Text = "";
            gvDetalle.EditIndex = -1;
            ActualizarGrilla();
        }

        protected void gvDetalle_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMensajeDetalle.Text = "";
            GridViewRow fila = gvDetalle.Rows[e.RowIndex];
            TextBox txtCantEdit = (TextBox)fila.FindControl("txtCantidadEdit");
            Button btnGuardar = (Button)fila.FindControl("btnGuardar");

            if (txtCantEdit != null && btnGuardar != null && int.TryParse(txtCantEdit.Text, out int nuevaCantidad) && nuevaCantidad > 0)
            {
                int idArticulo = Convert.ToInt32(btnGuardar.CommandArgument);
                var listaDetalle = (List<DetalleVenta>)Session["DetallesVenta"];

                if (listaDetalle != null)
                {
                    var detalle = listaDetalle.FirstOrDefault(x => x.Articulo != null && x.Articulo.IdArticulo == idArticulo);
                    if (detalle != null)
                    {
                        if (nuevaCantidad > detalle.Articulo.Stock)
                        {
                            MostrarMensaje(lblMensajeDetalle, "No hay suficiente stock. Máximo disponible: " + detalle.Articulo.Stock);
                            return;
                        }
                        detalle.Cantidad = nuevaCantidad;
                        detalle.Subtotal = nuevaCantidad * detalle.PrecioUnitario;
                    }
                }

                Session["DetallesVenta"] = listaDetalle;
                gvDetalle.EditIndex = -1;
                ActualizarGrilla();
            }
            else
            {
                MostrarMensaje(lblMensajeDetalle, "Ingrese una cantidad válida mayor a 0.");
            }
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AbrirModalEliminar")
            {
                hfIdArticuloEliminar.Value = e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopModal", "$('#modalEliminarArticulo').modal('show');", true);
            }
        }

        protected void btnConfirmarEliminarArticulo_Click(object sender, EventArgs e)
        {
            int idArticuloAEliminar = int.Parse(hfIdArticuloEliminar.Value);
            var listaDetalle = (List<DetalleVenta>)Session["DetallesVenta"];

            if (listaDetalle != null)
            {
                var item = listaDetalle.FirstOrDefault(x => x.Articulo != null && x.Articulo.IdArticulo == idArticuloAEliminar);
                if (item != null)
                {
                    listaDetalle.Remove(item);
                }

                Session["DetallesVenta"] = listaDetalle;
                ActualizarGrilla();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideModal", "$('#modalEliminarArticulo').modal('hide');", true);
        }

        protected void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            lblMensajeDetalle.Text = "";
            if (!validarVenta()) return;

            List<DetalleVenta> detalles = (List<DetalleVenta>)Session["DetallesVenta"];

            Venta ventaDto = new Venta
            {
                Usuario = new Usuario { IdUsuario = 1 },
                Cliente = new Cliente { IdCliente = int.Parse(ddlCliente.SelectedValue) },
                MedioPago = new MedioPago { IdMedioPago = int.Parse(ddlMedio.SelectedValue) },
                NumeroFactura = txtNumeroComprobante.Text.Trim(),
                Fecha = DateTime.Today,
                DetallesVenta = detalles,
                Total = detalles.Sum(x => x.Subtotal)
            };

            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                ventaNegocio.agregar(ventaDto);

                Session.Remove("DetallesVenta");
                Session.Remove("ArticulosFiltrados");
                Session.Remove("ArticuloSeleccionado");

                Response.Redirect("Ventas.aspx?mensaje=Venta registrada correctamente");
            }
            catch (Exception ex)
            {
                MostrarMensaje(lblMensaje, ex.Message);
            }
        }

        protected void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            Session.Remove("DetallesVenta");
            Response.Redirect("Ventas.aspx");
        }

        private bool validarVenta()
        {
            if (ddlCliente.SelectedValue == "0")
            {
                MostrarMensaje(lblMensaje, "Debe seleccionar un cliente.");
                return false;
            }

            if (ddlMedio.SelectedValue == "0")
            {
                MostrarMensaje(lblMensaje, "Debe seleccionar un medio de pago.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroComprobante.Text))
            {
                MostrarMensaje(lblMensaje, "Debe ingresar el número de factura/comprobante.");
                return false;
            }

            var lista = (List<DetalleVenta>)Session["DetallesVenta"];
            if (lista == null || !lista.Any())
            {
                MostrarMensaje(lblMensajeDetalle, "Debe agregar al menos un artículo a la venta.");
                return false;
            }

            return true;
        }

        private void ActualizarGrilla()
        {
            var lista = (List<DetalleVenta>)Session["DetallesVenta"] ?? new List<DetalleVenta>();
            gvDetalle.DataSource = lista;
            gvDetalle.DataBind();
            lblTotal.Text = "$ " + lista.Sum(x => x.Subtotal).ToString("N2");
        }

        private void MostrarMensaje(Label lbl, string mensaje)
        {
            lbl.Text = mensaje;
            lbl.CssClass = "text-danger font-weight-bold mb-2";
            lbl.Visible = true;

            string script = "setTimeout(function() { " +
                            "var mensajeElemento = document.getElementById('" + lbl.ClientID + "'); " +
                            "if (mensajeElemento) { mensajeElemento.innerHTML = ''; } " +
                            "}, 4000);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}