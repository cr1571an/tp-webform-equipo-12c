using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AppGestionNegocio.Negocio
{
    public class InicioNegocio
    {
        public decimal obtenerTotalVentasDia()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ISNULL(SUM(Total), 0) FROM Ventas WHERE Activo = 1 AND Fecha >= CONVERT(date, GETDATE()) AND Fecha < DATEADD(day, 1, CONVERT(date, GETDATE()))");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToDecimal(datos.Lector[0]);
                }

                return 0;
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

        public int obtenerCantidadVentasDia()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Ventas WHERE Activo = 1 AND Fecha >= CONVERT(date, GETDATE()) AND Fecha < DATEADD(day, 1, CONVERT(date, GETDATE()))");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]);
                }

                return 0;
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

        public decimal obtenerTotalComprasDia()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ISNULL(SUM(Total), 0) FROM Compras WHERE Activo = 1 AND FechaCompra >= CONVERT(date, GETDATE()) AND FechaCompra < DATEADD(day, 1, CONVERT(date, GETDATE()))");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToDecimal(datos.Lector[0]);
                }

                return 0;
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

        public int obtenerCantidadComprasDia()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Compras WHERE Activo = 1 AND FechaCompra >= CONVERT(date, GETDATE()) AND FechaCompra < DATEADD(day, 1, CONVERT(date, GETDATE()))");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]);
                }

                return 0;
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

        public int obtenerCantidadArticulosActivos()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Articulos WHERE Activo = 1");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]);
                }

                return 0;
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

        public int obtenerCantidadClientesActivos()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Clientes WHERE Activo = 1");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]);
                }

                return 0;
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

        public DataTable listarActividadReciente()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT TOP 5 Tipo, Persona, NumeroFactura, Fecha, Total FROM (SELECT 'Venta' AS Tipo, c.Nombre + ' ' + c.Apellido AS Persona, v.NumeroFactura, v.Fecha, v.Total FROM Ventas v INNER JOIN Clientes c ON c.IdCliente = v.IdCliente WHERE v.Activo = 1 UNION ALL SELECT 'Compra' AS Tipo, p.Nombre AS Persona, co.NumeroFactura, co.FechaCompra AS Fecha, co.Total FROM Compras co INNER JOIN Proveedores p ON p.IdProveedor = co.IdProveedor WHERE co.Activo = 1) AS Actividades ORDER BY Fecha DESC");

                datos.ejecutarLectura();

                DataTable tabla = new DataTable();
                tabla.Load(datos.Lector);

                return tabla;
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
    }
}