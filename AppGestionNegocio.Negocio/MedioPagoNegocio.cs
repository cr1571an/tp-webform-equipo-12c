using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class MedioPagoNegocio
    {
        public List<MedioPago> listar()
        {
            List<MedioPago> lista = new List<MedioPago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdMedioPago, Nombre, Descripcion, Activo FROM MediosPago WHERE Activo = 1 ORDER BY Nombre");
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    MedioPago aux = new MedioPago();

                    aux.IdMedioPago = (int)lector["IdMedioPago"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = lector["Descripcion"] != DBNull.Value ? (string)lector["Descripcion"] : "";
                    aux.Activo = bool.Parse(lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(MedioPago nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO MediosPago (Nombre, Descripcion, Activo) VALUES (@Nombre, @Descripcion, 1)");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(MedioPago medioPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MediosPago SET Nombre = @Nombre, Descripcion = @Descripcion, Activo = @Activo WHERE IdMedioPago = @IdMedioPago");
                datos.setearParametro("@Nombre", medioPago.Nombre);
                datos.setearParametro("@Descripcion", medioPago.Descripcion);
                datos.setearParametro("@Activo", medioPago.Activo);
                datos.setearParametro("@IdMedioPago", medioPago.IdMedioPago);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int idMedioPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MediosPago SET Activo = 0 WHERE IdMedioPago = @IdMedioPago");
                datos.setearParametro("@IdMedioPago", idMedioPago);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<MedioPago> filtrar(string filtro)
        {
            List<MedioPago> lista = new List<MedioPago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdMedioPago, Nombre, Descripcion, Activo FROM MediosPago WHERE Activo = 1";

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    consulta += " AND Nombre LIKE @filtro";
                }

                consulta += " ORDER BY Nombre";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    datos.setearParametro("@filtro", "%" + filtro.Trim() + "%");
                }

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    MedioPago aux = new MedioPago();

                    aux.IdMedioPago = (int)lector["IdMedioPago"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = lector["Descripcion"] != DBNull.Value ? (string)lector["Descripcion"] : "";
                    aux.Activo = bool.Parse(lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public MedioPago obtenerPorId(int idMedioPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta(@"SELECT IdMedioPago,Nombre,Descripcion,Activo
                                     FROM MediosPago
                                     WHERE IdMedioPago = @IdMedioPago");


                datos.setearParametro("@IdMedioPago", idMedioPago);

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                if (lector.Read())
                {
                    MedioPago medioPago = new MedioPago();

                    medioPago.IdMedioPago = (int)lector["IdMedioPago"];
                    medioPago.Nombre = (string)lector["Nombre"];
                    medioPago.Descripcion = lector["Descripcion"] == DBNull.Value
                            ? string.Empty
                            : (string)lector["Descripcion"];
                    medioPago.Activo = (bool)lector["Activo"];

                    return medioPago;
                }

                return null;
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
    }
}