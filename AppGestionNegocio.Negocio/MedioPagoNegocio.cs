using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class MedioPagoNegocio
    {
        public List<MedioPago> listar(bool verInactivos = false)
        {
            List<MedioPago> lista = new List<MedioPago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdMedioPago, Nombre, Descripcion, Activo FROM MediosPago WHERE Activo = @Activo ORDER BY Nombre");
                datos.setearParametro("@Activo", verInactivos ? 0 : 1);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    MedioPago aux = cargarMedioPago(lector);
                    lista.Add(aux);
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

        public void agregar(MedioPago nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO MediosPago (Nombre, Descripcion, Activo) VALUES (@Nombre, @Descripcion, 1)");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", string.IsNullOrWhiteSpace(nuevo.Descripcion) ? (object)DBNull.Value : nuevo.Descripcion);
                datos.ejecutarAccion();
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

        public void modificar(MedioPago medioPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MediosPago SET Nombre = @Nombre, Descripcion = @Descripcion, Activo = @Activo WHERE IdMedioPago = @IdMedioPago");
                datos.setearParametro("@Nombre", medioPago.Nombre);
                datos.setearParametro("@Descripcion", string.IsNullOrWhiteSpace(medioPago.Descripcion) ? (object)DBNull.Value : medioPago.Descripcion);
                datos.setearParametro("@Activo", medioPago.Activo);
                datos.setearParametro("@IdMedioPago", medioPago.IdMedioPago);
                datos.ejecutarAccion();
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

        public void eliminar(int idMedioPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MediosPago SET Activo = 0 WHERE IdMedioPago = @IdMedioPago");
                datos.setearParametro("@IdMedioPago", idMedioPago);
                datos.ejecutarAccion();
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

        public void restaurar(int idMedioPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MediosPago SET Activo = 1 WHERE IdMedioPago = @IdMedioPago");
                datos.setearParametro("@IdMedioPago", idMedioPago);
                datos.ejecutarAccion();
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

        public MedioPago obtenerPorId(int idMedioPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdMedioPago, Nombre, Descripcion, Activo FROM MediosPago WHERE IdMedioPago = @IdMedioPago");
                datos.setearParametro("@IdMedioPago", idMedioPago);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                if (lector.Read())
                {
                    return cargarMedioPago(lector);
                }

                return null;
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

        private MedioPago cargarMedioPago(SqlDataReader lector)
        {
            MedioPago aux = new MedioPago();

            aux.IdMedioPago = (int)lector["IdMedioPago"];
            aux.Nombre = lector["Nombre"].ToString();
            aux.Descripcion = lector["Descripcion"] != DBNull.Value ? lector["Descripcion"].ToString() : "";
            aux.Activo = bool.Parse(lector["Activo"].ToString());

            return aux;
        }
    }
}