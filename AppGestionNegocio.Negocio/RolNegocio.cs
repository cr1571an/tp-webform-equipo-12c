using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class RolNegocio
    {
        public List<Rol> listar(int? id = null)
        {
            List<Rol> lista = new List<Rol>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdRol, NombreRol, Descripcion, Activo FROM Roles WHERE Activo = 1 ";

                if (id.HasValue)
                {
                    consulta += " AND IdRol = @IdRol ";
                    datos.setearParametro("@IdRol", id.Value);
                }

                consulta += "ORDER BY NombreRol";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Rol aux = new Rol();

                    aux.IdRol = (int)lector["IdRol"];
                    aux.Nombre = (string)lector["NombreRol"];
                    aux.Descripcion = (string)lector["Descripcion"];
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
        public void agregar(Rol nuevoRol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Roles (NombreRol, Descripcion) VALUES (@Nombre, @Descripcion)");
                datos.setearParametro("@Nombre", nuevoRol.Nombre);
                datos.setearParametro("@Descripcion", nuevoRol.Descripcion);
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
        public void modificar(Rol rol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Roles SET NombreRol = @Nombre,Descripcion = @Descripcion, Activo = @Activo WHERE IdRol = @IdRol");
                datos.setearParametro("@Nombre", rol.Nombre);
                datos.setearParametro("@Descripcion", rol.Descripcion);                
                datos.setearParametro("@Activo", rol.Activo);
                datos.setearParametro("@IdRol", rol.IdRol);
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
        public void eliminar(int idRol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Roles SET Activo = 0 WHERE IdRol = @IdRol");
                datos.setearParametro("@IdRol", idRol);
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
        public List<Rol> filtrar(string filtro)
        {
            List<Rol> lista = new List<Rol>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdRol, NombreRol, Descripcion, Activo FROM Roles WHERE Activo = 1";

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    consulta += " AND NombreRol LIKE @filtro";
                }

                consulta += " ORDER BY NombreRol";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    datos.setearParametro("@filtro", "%" + filtro.Trim() + "%");
                }

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Rol aux = new Rol();

                    aux.IdRol = (int)lector["IdRol"];
                    aux.Nombre = (string)lector["NombreRol"];
                    aux.Descripcion = (string)lector["Descripcion"];                    
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
    }
}