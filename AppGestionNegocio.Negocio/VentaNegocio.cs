using AppGestionNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Negocio
{
    public class VentaNegocio
    {
        private const string CONSULTA_VENTAS = "SELECT V.IdVenta, V.IdCliente, C.Nombre AS NombreCliente, C.Apellido AS ApellidoCliente, V.IdUsuario, V.IdMedioPago, MP.Nombre AS MedioPago, V.Fecha, V.NumeroFactura, V.Total, V.Activo FROM Ventas V INNER JOIN Clientes C ON V.IdCliente = C.IdCliente INNER JOIN MediosPago MP ON V.IdMedioPago = MP.IdMedioPago WHERE V.Activo = 1";
        private const string CONSULTA_DETALLES = "SELECT DV.IdDetalleVenta, DV.IdVenta, DV.IdArticulo, A.Nombre AS NombreArticulo, DV.Cantidad, DV.PrecioUnitario, DV.SubTotal FROM DetallesVenta DV INNER JOIN Articulos A ON DV.IdArticulo = A.IdArticulo";

        public List<Venta> listar(string id = "")
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                string consulta = CONSULTA_VENTAS;

                if (!string.IsNullOrEmpty(id))
                {
                    consulta += " AND V.IdVenta = @IdVenta";
                }

                accesoDatos.setearConsulta(consulta);

                if (!string.IsNullOrEmpty(id))
                {
                    accesoDatos.setearParametro("@IdVenta", int.Parse(id));
                }

                accesoDatos.ejecutarLectura();
                SqlDataReader lector = accesoDatos.Lector;

                while (lector.Read())
                {
                    lista.Add(MapearVenta(lector));
                }

                if (!string.IsNullOrEmpty(id) && lista.Count > 0)
                {
                    lista[0].DetallesVenta = listarDetallesPorVenta(lista[0].IdVenta);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        private List<DetalleVenta> listarDetallesPorVenta(int idVenta)
        {
            List<DetalleVenta> listaDetalles = new List<DetalleVenta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(CONSULTA_DETALLES + " WHERE DV.IdVenta = @IdVenta");
                datos.setearParametro("@IdVenta", idVenta);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;
                while (lector.Read())
                {
                    DetalleVenta det = new DetalleVenta();
                    det.IdDetalleVenta = lector["IdDetalleVenta"] != DBNull.Value ? (int)lector["IdDetalleVenta"] : 0;
                    det.Cantidad = lector["Cantidad"] != DBNull.Value ? Convert.ToInt32(lector["Cantidad"]) : 0;
                    det.PrecioUnitario = lector["PrecioUnitario"] != DBNull.Value ? (decimal)lector["PrecioUnitario"] : 0;
                    det.Subtotal = lector["SubTotal"] != DBNull.Value ? (decimal)lector["SubTotal"] : 0;
                    det.Articulo = new Articulo();
                    if (lector["IdArticulo"] != DBNull.Value)
                    {
                        det.Articulo.IdArticulo = (int)lector["IdArticulo"];
                        det.Articulo.Nombre = lector["NombreArticulo"] != DBNull.Value ? lector["NombreArticulo"].ToString() : "";
                    }
                    listaDetalles.Add(det);
                }
                return listaDetalles;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private Venta MapearVenta(SqlDataReader lector)
        {
            Venta aux = new Venta();

            aux.IdVenta = lector["IdVenta"] != DBNull.Value ? (int)lector["IdVenta"] : 0;
            aux.Fecha = lector["Fecha"] != DBNull.Value ? (DateTime)lector["Fecha"] : DateTime.MinValue;
            aux.NumeroFactura = lector["NumeroFactura"] != DBNull.Value ? lector["NumeroFactura"].ToString() : "";
            aux.Total = lector["Total"] != DBNull.Value ? (decimal)lector["Total"] : 0;
            aux.Cliente = new Cliente();
            if (lector["IdCliente"] != DBNull.Value)
            {
                aux.Cliente.IdCliente = (int)lector["IdCliente"];
                string nombre = lector["NombreCliente"] != DBNull.Value ? lector["NombreCliente"].ToString() : "";
                string apellido = lector["ApellidoCliente"] != DBNull.Value ? lector["ApellidoCliente"].ToString() : "";
                aux.Cliente.Nombre = $"{nombre} {apellido}".Trim();
            }
            aux.MedioPago = new MedioPago();
            if (lector["IdMedioPago"] != DBNull.Value)
            {
                aux.MedioPago.IdMedioPago = (int)lector["IdMedioPago"];
                aux.MedioPago.Nombre = lector["MedioPago"] != DBNull.Value ? lector["MedioPago"].ToString() : "";
            }
            aux.Usuario = new Usuario();
            if (lector["IdUsuario"] != DBNull.Value)
            {
                aux.Usuario.IdUsuario = (int)lector["IdUsuario"];
            }
            aux.DetallesVenta = new List<DetalleVenta>();
            return aux;
        }

        public List<Venta> listarOrdenadoPorFecha(string criterio)
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = CONSULTA_VENTAS;

                switch (criterio)
                {
                    case "MasRecientes":
                        consulta += " ORDER BY V.Fecha DESC";
                        break;
                    case "MasAntiguas":
                        consulta += " ORDER BY V.Fecha ASC";
                        break;
                    default:
                        consulta += " ORDER BY V.Fecha DESC";
                        break;
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                SqlDataReader lector = datos.Lector;
                while (lector.Read())
                {
                    lista.Add(MapearVenta(lector));
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.iniciarTransaccion();

                datos.setearProcedimiento("sp_CrearCabeceraVenta");
                datos.setearParametro("@IdCliente", venta.Cliente.IdCliente);
                datos.setearParametro("@IdUsuario", venta.Usuario.IdUsuario);
                datos.setearParametro("@IdMedioPago", venta.MedioPago.IdMedioPago);
                datos.setearParametro("@NumeroFactura", venta.NumeroFactura);
                datos.setearParametro("@Total", venta.Total); 
                int idVenta = datos.ejecutarAccionScalarTransaccion();

                foreach (DetalleVenta detalle in venta.DetallesVenta)
                {
                    datos.limpiarParametros();
                    datos.setearProcedimiento("sp_AgregarDetalleVenta");

                    datos.setearParametro("@IdVenta", idVenta);
                    datos.setearParametro("@IdArticulo", detalle.Articulo.IdArticulo);
                    datos.setearParametro("@Cantidad", detalle.Cantidad);

                    datos.ejecutarAccionTransaccion();
                }
                datos.confirmarTransaccion();
            }
            catch (Exception ex)
            {
                datos.cancelarTransaccion();
                throw new Exception("Error al procesar la venta: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int idVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_EliminarVenta");
                datos.setearParametro("@IdVenta", idVenta);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la venta: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}