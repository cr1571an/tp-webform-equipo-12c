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
                datos.setearConsulta(@"SELECT C.IdCompra,C.FechaCompra,C.NumeroFactura,C.Total,P.IdProveedor,P.Nombre AS Proveedor,M.IdMedioPago,
                                       M.Descripcion AS MedioPago
                                       FROM Compras C
                                       INNER JOIN Proveedores P ON P.IdProveedor = C.IdProveedor
                                       INNER JOIN MediosPago M ON M.IdMedioPago = C.IdMedioPago
                                       WHERE C.Activo = 1");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();

                    aux.IdCompra = (int)datos.Lector["IdCompra"];
                    aux.FechaCompra = (DateTime)datos.Lector["FechaCompra"];
                    aux.NumeroComprobante = (string)datos.Lector["NumeroFactura"];
                    aux.Total = (decimal)datos.Lector["Total"];

                    aux.Proveedor = new Proveedor();

                    aux.Proveedor.IdProveedor = (int)datos.Lector["IdProveedor"];
                    aux.Proveedor.Nombre = (string)datos.Lector["Proveedor"];

                    aux.MedioPago = new MedioPago();

                    aux.MedioPago.IdMedioPago = (int)datos.Lector["IdMedioPago"];
                    aux.MedioPago.Descripcion = (string)datos.Lector["MedioPago"];

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
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT IdCompra,IdUsuario,IdProveedor,IdMedioPago,FechaCompra,NumeroFactura,Observaciones,Total                                       
                                       FROM Compras
                                       WHERE IdCompra = @IdCompra
                                       AND Activo = 1");

                datos.setearParametro("@IdCompra", idCompra);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                if (!lector.Read())
                    return null;

                Compra compra = new Compra();

                compra.IdCompra = (int)lector["IdCompra"];

                compra.FechaCompra = (DateTime)lector["FechaCompra"];
                compra.NumeroComprobante = (string)lector["NumeroFactura"];
                compra.Total = (decimal)lector["Total"];
                compra.Observaciones = lector["Observaciones"] == DBNull.Value
                        ? string.Empty
                        : (string)lector["Observaciones"];
                int idProveedor = (int)lector["IdProveedor"];
                int idMedioPago = (int)lector["IdMedioPago"];
                int idUsuario = (int)lector["IdUsuario"];

                datos.cerrarConexion();

                compra.Proveedor = new ProveedorNegocio().obtenerPorId(idProveedor);
                compra.MedioPago = new MedioPagoNegocio().obtenerPorId(idMedioPago);
                compra.Usuario = new Usuario
                {
                    IdUsuario = idUsuario
                };

                List<DetalleCompra> detalleCompra = obtenerDetalles(idCompra);

                compra.DetallesCompra = detalleCompra;

                return compra;
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

        private List<DetalleCompra> obtenerDetalles(int idCompra)
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT DC.IdDetalleCompra,DC.IdArticulo,DC.Cantidad,DC.PrecioUnitario,DC.SubTotal,A.Nombre
                                      FROM DetallesCompra DC
                                      INNER JOIN Articulos A ON A.IdArticulo = DC.IdArticulo
                                      WHERE DC.IdCompra = @IdCompra");

                datos.setearParametro("@IdCompra", idCompra);

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    DetalleCompra detalle = new DetalleCompra();

                    detalle.IdDetalleCompra = (int)lector["IdDetalleCompra"];
                    detalle.Cantidad = (int)lector["Cantidad"];
                    detalle.PrecioUnitario = (decimal)lector["PrecioUnitario"];
                    detalle.Subtotal = (decimal)lector["SubTotal"];

                    Articulo articulo = new Articulo();
                    articulo.IdArticulo = (int)lector["IdArticulo"];
                    articulo.Nombre = (string)lector["Nombre"];
                    detalle.Articulo = articulo;
                    lista.Add(detalle);
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

        public void agregar(CompraDto compra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.iniciarTransaccion();

                datos.setearConsulta(@"INSERT INTO Compras
                                    (
                                        IdUsuario,
                                        IdProveedor,
                                        IdMedioPago,
                                        FechaCompra,
                                        NumeroFactura,
                                        Observaciones,
                                        Total,
                                        Activo
                                    )
                                    VALUES
                                    (
                                        @IdUsuario,
                                        @IdProveedor,
                                        @IdMedioPago,
                                        @FechaCompra,
                                        @NumeroFactura,
                                        @Observaciones,
                                        @Total,
                                        1
                                    );

                                    SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@IdUsuario", compra.IdUsuario);
                datos.setearParametro("@IdProveedor", compra.IdProveedor);
                datos.setearParametro("@IdMedioPago", compra.IdMedioPago);
                datos.setearParametro("@FechaCompra", compra.FechaCompra);
                datos.setearParametro("@NumeroFactura", compra.NumeroComprobante);
                datos.setearParametro("@Observaciones",
                    string.IsNullOrWhiteSpace(compra.Observaciones)
                        ? (object)DBNull.Value
                        : compra.Observaciones);
                datos.setearParametro("@Total", compra.Total);

                int idCompra = datos.ejecutarAccionScalarTransaccion();

                foreach (DetalleCompraDto detalle in compra.Detalles)
                {
                    guardarDetalle(datos, idCompra, detalle);
                    modificarStock(datos, detalle.IdArticulo, detalle.Cantidad, OperacionStock.Sumar);
                }

                datos.confirmarTransaccion();
            }
            catch
            {
                datos.cancelarTransaccion();
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void guardarDetalle(AccesoDatos datos, int idCompra, DetalleCompraDto detalle)
        {
            datos.limpiarParametros();

            datos.setearConsulta(@"INSERT INTO DetallesCompra
                                (
                                    IdCompra,
                                    IdArticulo,
                                    Cantidad,
                                    PrecioUnitario,
                                    SubTotal
                                )
                                VALUES
                                (
                                    @IdCompra,
                                    @IdArticulo,
                                    @Cantidad,
                                    @PrecioUnitario,
                                    @SubTotal
                                )");

            datos.setearParametro("@IdCompra", idCompra);
            datos.setearParametro("@IdArticulo", detalle.IdArticulo);
            datos.setearParametro("@Cantidad", detalle.Cantidad);
            datos.setearParametro("@PrecioUnitario", detalle.PrecioUnitario);
            datos.setearParametro("@SubTotal", detalle.Subtotal);

            datos.ejecutarAccionTransaccion();
        }
        public void modificar(CompraDto compra) { }

        public void eliminar(int idCompra)
        {
            AccesoDatos datos = new AccesoDatos();

            datos.iniciarTransaccion();

            try
            {
                List<DetalleCompra> detalles =
                    obtenerDetalles(idCompra);

                foreach (DetalleCompra detalle in detalles)
                {
                    modificarStock(datos, detalle.Articulo.IdArticulo, detalle.Cantidad, OperacionStock.Restar);
                }

                eliminarDetalles(datos, idCompra);

                desactivarCompra(datos, idCompra);

                datos.confirmarTransaccion();
            }
            catch
            {
                datos.cancelarTransaccion();
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void modificarStock(AccesoDatos datos, int idArticulo, int cantidad, OperacionStock operacion)
        {
            string operador = operacion == OperacionStock.Sumar
                    ? "+"
                    : "-";

            datos.limpiarParametros();

            datos.setearConsulta($@"UPDATE Articulos
                                    SET Stock = Stock {operador} @Cantidad
                                    WHERE IdArticulo = @IdArticulo");

            datos.setearParametro("@Cantidad", cantidad);
            datos.setearParametro("@IdArticulo", idArticulo);

            datos.ejecutarAccionTransaccion();
        }

        private void eliminarDetalles(AccesoDatos datos, int idCompra)
        {
            datos.limpiarParametros();

            datos.setearConsulta(@"DELETE FROM DetallesCompra
                                   WHERE IdCompra = @IdCompra");

            datos.setearParametro("@IdCompra", idCompra);

            datos.ejecutarAccionTransaccion();
        }

        private void desactivarCompra(AccesoDatos datos, int idCompra)
        {
            datos.limpiarParametros();

            datos.setearConsulta(@"UPDATE Compras
                                  SET Activo = 0
                                  WHERE IdCompra = @IdCompra");
            datos.setearParametro("@IdCompra", idCompra);

            datos.ejecutarAccionTransaccion();
        }
    }
}
