using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class RolNegocio
    {
        public List<Rol> listar(int? id = null, bool verInactivos = false)
        {
            List<Rol> lista = new List<Rol>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdRol, NombreRol, Descripcion, Activo FROM Roles WHERE Activo = @Activo ";

                if (id.HasValue)
                {
                    consulta += "AND IdRol = @IdRol ";
                }

                consulta += "ORDER BY NombreRol";

                datos.setearConsulta(consulta);
                datos.setearParametro("@Activo", verInactivos ? 0 : 1);

                if (id.HasValue)
                {
                    datos.setearParametro("@IdRol", id.Value);
                }

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Rol aux = cargarRol(lector);
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

        public void agregar(Rol nuevoRol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Roles (NombreRol, Descripcion, Activo) VALUES (@Nombre, @Descripcion, 1)");
                datos.setearParametro("@Nombre", nuevoRol.Nombre);
                datos.setearParametro("@Descripcion", nuevoRol.Descripcion);
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

        public void modificar(Rol rol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Roles SET NombreRol = @Nombre, Descripcion = @Descripcion, Activo = @Activo WHERE IdRol = @IdRol");
                datos.setearParametro("@Nombre", rol.Nombre);
                datos.setearParametro("@Descripcion", rol.Descripcion);
                datos.setearParametro("@Activo", rol.Activo);
                datos.setearParametro("@IdRol", rol.IdRol);
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

        public void eliminar(int idRol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Roles SET Activo = 0 WHERE IdRol = @IdRol");
                datos.setearParametro("@IdRol", idRol);
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

        public void restaurar(int idRol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Roles SET Activo = 1 WHERE IdRol = @IdRol");
                datos.setearParametro("@IdRol", idRol);
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

        private Rol cargarRol(SqlDataReader lector)
        {
            Rol aux = new Rol();

            aux.IdRol = (int)lector["IdRol"];
            aux.Nombre = lector["NombreRol"].ToString();
            aux.Descripcion = lector["Descripcion"].ToString();
            aux.Activo = bool.Parse(lector["Activo"].ToString());

            return aux;
        }
    }
}