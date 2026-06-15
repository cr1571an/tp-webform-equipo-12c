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
    public partial class ArticuloFormulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    CargarDesplegables();
                }
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    btnEliminar.Visible = true;
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
                    txtImagenUrl_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDesplegables()
        {
            /*
            CategoriaNegocio catNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = catNegocio.listar();
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataBind();
            */
            ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione una Categoría --", "0"));

            /*
            MarcaNegocio marNegocio = new MarcaNegocio();
            ddlMarca.DataSource = marNegocio.listar();
            ddlMarca.DataValueField = "IdMarca";
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataBind();
            */
            ddlMarca.Items.Insert(0, new ListItem("-- Seleccione una Marca --", "0"));

            /*
            AlicuotaIvaNegocio ivaNegocio = new AlicuotaIvaNegocio();
            ddlIva.DataSource = ivaNegocio.listar();
            ddlIva.DataValueField = "IdAlicuotaIva";
            ddlIva.DataTextField = "Alicuota";
            ddlIva.DataBind();
            */
            ddlIva.Items.Insert(0, new ListItem("-- Seleccione Alícuota IVA --", "0"));
        }

        private bool ValidarCampo()
        {
            lblMensajeError.Visible = false;

            if (ddlCategoria.SelectedValue == "0")
            {   
                lblMensajeError.Text = "Error: Por favor, seleccione una Categoría.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (ddlMarca.SelectedValue == "0")
            {
                lblMensajeError.Text = "Error: Por favor, seleccione una Marca.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (ddlIva.SelectedValue == "0")
            {
                lblMensajeError.Text = "Error: Por favor, seleccione Alícuota de IVA.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                lblMensajeError.Text = "Error: Por favor, ingrese el nombre del artículo.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                lblMensajeError.Text = "Error: Por favor, ingrese un precio válido y mayor a cero.";
                lblMensajeError.Visible = true;
                return false;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                lblMensajeError.Text = "Error: Por favor, ingrese un stock válido. No puede ser negativo.";
                lblMensajeError.Visible = true;
                return false;
            }

            return true;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulos.aspx");
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPreview.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo seleccionado = (Articulo)Session["articuloSeleccionado"];

                if (seleccionado != null)
                {
                    negocio.eliminarLogico(seleccionado.IdArticulo);
                    Response.Redirect("Articulos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                if (ValidarCampo() == false) return;

                ArticuloNegocio negocio = new ArticuloNegocio();
                bool esNuevo = (Request.QueryString["id"] == null);

                if (esNuevo)
                {
                    List<Articulo> listaArticulos = negocio.listar();
                    foreach (Articulo art in listaArticulos)
                    {
                        if (art.Nombre.Trim().ToUpper() == txtNombre.Text.Trim().ToUpper())
                        {
                            lblMensajeError.Text = "Error: No se puede agregar este artículo porque ya está en el sistema.";
                            lblMensajeError.Visible = true;
                            return; 
                        }
                    }
                }

                Articulo nuevo = new Articulo();

                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.UrlImagen = txtImagenUrl.Text;
                nuevo.Stock = int.Parse(txtStock.Text);

                nuevo.Categoria = new Categoria();
                nuevo.Categoria.IdCategoria = int.Parse(ddlCategoria.SelectedValue);

                nuevo.Marca = new Marca();
                nuevo.Marca.IdMarca = int.Parse(ddlMarca.SelectedValue);

                nuevo.AlicuotaIva = new AlicuotaIva();
                nuevo.AlicuotaIva.IdAlicuotaIva = int.Parse(ddlIva.SelectedValue);

                if (!esNuevo)
                {
                    string idUrl = Request.QueryString["id"].ToString();
                    nuevo.IdArticulo = int.Parse(idUrl);

                    negocio.modificar(nuevo);
                }
                else
                {
                    negocio.agregar(nuevo);
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