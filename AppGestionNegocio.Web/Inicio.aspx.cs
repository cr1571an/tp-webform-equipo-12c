using System;
using System.Data;
using System.Globalization;
using AppGestionNegocio.Negocio;

namespace AppGestionNegocio.Web
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarReportes();
                cargarActividadReciente();
            }
        }

        private void cargarReportes()
        {
            try
            {
                InicioNegocio negocio = new InicioNegocio();

                CultureInfo culturaArgentina = new CultureInfo("es-AR");

                decimal totalVentasDia = negocio.obtenerTotalVentasDia();
                int cantidadVentasDia = negocio.obtenerCantidadVentasDia();

                decimal totalComprasDia = negocio.obtenerTotalComprasDia();
                int cantidadComprasDia = negocio.obtenerCantidadComprasDia();

                int cantidadArticulos = negocio.obtenerCantidadArticulosActivos();
                int cantidadClientes = negocio.obtenerCantidadClientesActivos();

                lblTotalVentasDia.Text = totalVentasDia.ToString("C", culturaArgentina);
                lblCantidadVentasDia.Text = cantidadVentasDia == 1 ? "1 venta" : cantidadVentasDia + " ventas";

                lblTotalComprasDia.Text = totalComprasDia.ToString("C", culturaArgentina);
                lblCantidadComprasDia.Text = cantidadComprasDia == 1 ? "1 compra" : cantidadComprasDia + " compras";

                lblCantidadArticulos.Text = cantidadArticulos.ToString();
                lblCantidadClientes.Text = cantidadClientes.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarActividadReciente()
        {
            try
            {
                InicioNegocio negocio = new InicioNegocio();

                DataTable tablaActividad = negocio.listarActividadReciente();

                if (tablaActividad.Rows.Count > 0)
                {
                    rptActividadReciente.DataSource = tablaActividad;
                    rptActividadReciente.DataBind();

                    pnlActividadVacia.Visible = false;
                    pnlActividadLista.Visible = true;
                }
                else
                {
                    pnlActividadVacia.Visible = true;
                    pnlActividadLista.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}