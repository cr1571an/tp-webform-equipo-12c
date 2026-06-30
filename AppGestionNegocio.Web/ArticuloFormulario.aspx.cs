using AppGestionNegocio.Dominio;
using AppGestionNegocio.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppGestionNegocio.Web
{
    public partial class ArticuloFormulario : System.Web.UI.Page
    {
        private List<int> IdsProveedoresAsociados
        {
            get
            {
                if (ViewState["IdsProveedoresAsociados"] == null)
                {
                    ViewState["IdsProveedoresAsociados"] = new List<int>();
                }

                return (List<int>)ViewState["IdsProveedoresAsociados"];
            }
            set
            {
                ViewState["IdsProveedoresAsociados"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    OcultarError();
                }

                if (!IsPostBack)
                {
                    CargarDesplegables();

                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                    if (id != "")
                    {
                        lblTitulo.Text = "Modificar artículo";
                        btnGuardar.Text = "Guardar cambios";

                        ArticuloNegocio negocio = new ArticuloNegocio();
                        Articulo seleccionado = (negocio.listar(id))[0];

                        Session.Add("articuloSeleccionado", seleccionado);

                        txtNombre.Text = seleccionado.Nombre;
                        txtDescripcion.Text = seleccionado.Descripcion;
                        txtPrecio.Text = seleccionado.Precio.ToString();
                        txtStock.Text = seleccionado.Stock.ToString();
                        txtImagenUrl.Text = seleccionado.UrlImagen;

                        ddlCategoria.SelectedValue = seleccionado.Categoria.IdCategoria.ToString();
                        ddlMarca.SelectedValue = seleccionado.Marca.IdMarca.ToString();
                        ddlIva.SelectedValue = seleccionado.AlicuotaIva.IdAlicuotaIva.ToString();

                        CargarProveedoresAsociados(seleccionado.IdArticulo);

                        txtImagenUrl_TextChanged(sender, e);
                    }
                    else
                    {
                        lblTitulo.Text = "Registrar artículo";
                        btnGuardar.Text = "Guardar artículo";

                        IdsProveedoresAsociados = new List<int>();
                        CargarGrillaProveedoresAsociados();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDesplegables()
        {
            CategoriaNegocio catNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = catNegocio.listar();
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione una Categoría --", "0"));

            MarcaNegocio marNegocio = new MarcaNegocio();
            ddlMarca.DataSource = marNegocio.listar();
            ddlMarca.DataValueField = "IdMarca";
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("-- Seleccione una Marca --", "0"));

            AlicuotaIvaNegocio ivaNegocio = new AlicuotaIvaNegocio();
            ddlIva.DataSource = ivaNegocio.listar();
            ddlIva.DataValueField = "IdAlicuotaIva";
            ddlIva.DataTextField = "Alicuota";
            ddlIva.DataBind();
            ddlIva.Items.Insert(0, new ListItem("-- Seleccione Alícuota IVA --", "0"));

            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            ddlProveedor.DataSource = proveedorNegocio.listar();
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("-- Seleccione un proveedor --", "0"));
        }

        private void CargarProveedoresAsociados(int idArticulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            IdsProveedoresAsociados = negocio.listarIdsProveedoresPorArticulo(idArticulo);

            CargarGrillaProveedoresAsociados();
        }

        private void CargarGrillaProveedoresAsociados()
        {
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            List<Proveedor> proveedores = proveedorNegocio.listar();
            List<Proveedor> proveedoresAsociados = new List<Proveedor>();

            foreach (int idProveedor in IdsProveedoresAsociados)
            {
                Proveedor proveedor = proveedores.FirstOrDefault(p => p.IdProveedor == idProveedor);

                if (proveedor != null)
                {
                    proveedoresAsociados.Add(proveedor);
                }
            }

            dgvProveedoresAsociados.DataSource = proveedoresAsociados;
            dgvProveedoresAsociados.DataBind();
        }

        private List<Proveedor> ObtenerProveedoresAsociados()
        {
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            List<Proveedor> proveedores = proveedorNegocio.listar();
            List<Proveedor> proveedoresAsociados = new List<Proveedor>();

            foreach (int idProveedor in IdsProveedoresAsociados)
            {
                Proveedor proveedor = proveedores.FirstOrDefault(p => p.IdProveedor == idProveedor);

                if (proveedor != null)
                {
                    proveedoresAsociados.Add(proveedor);
                }
            }

            return proveedoresAsociados;
        }

        private void OcultarError()
        {
            lblMensajeError.Text = "";
            lblMensajeError.Visible = false;
        }

        private void MostrarError(string mensaje)
        {
            lblMensajeError.Text = mensaje;
            lblMensajeError.Visible = true;

            string script = "setTimeout(function() { var mensaje = document.getElementById('" + lblMensajeError.ClientID + "'); if (mensaje) { mensaje.style.display = 'none'; mensaje.innerHTML = ''; } }, 4000);";

            ScriptManager.RegisterStartupScript(upFormulario, upFormulario.GetType(), "ocultarMensajeError", script, true);
        }

        private bool ValidarCampo()
        {
            OcultarError();

            if (ddlCategoria.SelectedValue == "0")
            {
                MostrarError("Error: Por favor, seleccione una Categoría.");
                return false;
            }

            if (ddlMarca.SelectedValue == "0")
            {
                MostrarError("Error: Por favor, seleccione una Marca.");
                return false;
            }

            if (ddlIva.SelectedValue == "0")
            {
                MostrarError("Error: Por favor, seleccione Alícuota de IVA.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarError("Error: Por favor, ingrese el nombre del artículo.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MostrarError("Error: Por favor, ingrese un precio válido y mayor a cero.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text) || !int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MostrarError("Error: Por favor, ingrese un stock válido. No puede ser negativo.");
                return false;
            }

            if (IdsProveedoresAsociados.Count == 0)
            {
                MostrarError("Error: Por favor, asocie al menos un proveedor.");
                return false;
            }

            return true;
        }

        protected void btnAsociarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProveedor.SelectedValue == "0")
                {
                    MostrarError("Error: Por favor, seleccione un proveedor para asociar.");
                    return;
                }

                int idProveedor = int.Parse(ddlProveedor.SelectedValue);
                List<int> ids = IdsProveedoresAsociados;

                if (ids.Contains(idProveedor))
                {
                    MostrarError("Error: Ese proveedor ya está asociado al artículo.");
                    return;
                }

                ids.Add(idProveedor);
                IdsProveedoresAsociados = ids;

                ddlProveedor.SelectedValue = "0";

                CargarGrillaProveedoresAsociados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void dgvProveedoresAsociados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "QuitarProveedor")
                {
                    int idProveedor = int.Parse(e.CommandArgument.ToString());

                    List<int> ids = IdsProveedoresAsociados;
                    ids.Remove(idProveedor);
                    IdsProveedoresAsociados = ids;

                    CargarGrillaProveedoresAsociados();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtImagenUrl.Text))
            {
                imgPreview.ImageUrl = txtImagenUrl.Text;
            }
            else
            {
                imgPreview.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulos.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampo())
                {
                    return;
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                bool esNuevo = (Request.QueryString["id"] == null);

                if (esNuevo)
                {
                    List<Articulo> listaArticulos = negocio.listar();

                    foreach (Articulo art in listaArticulos)
                    {
                        if (art.Nombre.Trim().ToUpper() == txtNombre.Text.Trim().ToUpper())
                        {
                            MostrarError("Error: Ya existe un artículo registrado con ese nombre.");
                            return;
                        }
                    }
                }

                Articulo nuevo = new Articulo();

                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Descripcion = txtDescripcion.Text.Trim();
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.UrlImagen = txtImagenUrl.Text.Trim();
                nuevo.Stock = int.Parse(txtStock.Text);

                nuevo.Categoria = new Categoria();
                nuevo.Categoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);

                nuevo.Marca = new Marca();
                nuevo.Marca.IdMarca = int.Parse(ddlMarca.SelectedValue);

                nuevo.AlicuotaIva = new AlicuotaIva();
                nuevo.AlicuotaIva.IdAlicuotaIva = int.Parse(ddlIva.SelectedValue);

                nuevo.Proveedores = ObtenerProveedoresAsociados();

                if (!esNuevo)
                {
                    string idUrl = Request.QueryString["id"].ToString();
                    nuevo.IdArticulo = int.Parse(idUrl);

                    negocio.modificar(nuevo);
                    negocio.guardarProveedoresArticulo(nuevo.IdArticulo, nuevo.Proveedores);
                }
                else
                {
                    int idArticulo = negocio.agregar(nuevo);
                    negocio.guardarProveedoresArticulo(idArticulo, nuevo.Proveedores);
                }

                Response.Redirect("Articulos.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}