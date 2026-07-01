using AppGestionNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Negocio
{

    public class CompraNegocio
    {
        public List<Compra> listar()
        {
            List<Compra> lista = new List<Compra>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    @"SELECT
                    C.IdCompra,
                    C.FechaCompra,
                    C.NumeroFactura,
                    C.Total,
                    P.IdProveedor,
                    P.Nombre AS Proveedor,
                    M.IdMedioPago,
                    M.Descripcion AS MedioPago
                  FROM Compras C
                  INNER JOIN Proveedores P
                    ON P.IdProveedor = C.IdProveedor
                  INNER JOIN MediosPago M
                    ON M.IdMedioPago = C.IdMedioPago
                  WHERE C.Activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();

                    aux.IdCompra =
                        (int)datos.Lector["IdCompra"];

                    aux.FechaCompra =
                        (DateTime)datos.Lector["FechaCompra"];

                    aux.NumeroComprobante =
                        (string)datos.Lector["NumeroFactura"];

                    aux.Total =
                        (decimal)datos.Lector["Total"];

                    aux.Proveedor = new Proveedor();

                    aux.Proveedor.IdProveedor =
                        (int)datos.Lector["IdProveedor"];

                    aux.Proveedor.Nombre =
                        (string)datos.Lector["Proveedor"];

                    aux.MedioPago = new MedioPago();

                    aux.MedioPago.IdMedioPago =
                        (int)datos.Lector["IdMedioPago"];

                    aux.MedioPago.Descripcion =
                        (string)datos.Lector["MedioPago"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Compra obtener(int idCompra)
        {
            return new Compra();
        }

        public void agregar(CompraDto compra) { }

        public void modificar(CompraDto compra) { }

        public void eliminar(int idCompra) { }
    }
}
