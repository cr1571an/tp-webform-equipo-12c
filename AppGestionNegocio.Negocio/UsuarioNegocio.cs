using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar(int? id = null)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT u.IdUsuario, u.IdEmpleado, u.IdRol, u.NombreUsuario, u.Activo AS UsuarioActivo, u.PasswordHash, e.IdEmpleado AS EmpIdEmpleado, e.Nombre, e.Apellido, e.Telefono, e.Email, e.Dni, e.FechaIngreso, e.Activo AS EmpleadoActivo, r.IdRol AS RolIdRol, r.NombreRol, r.Descripcion, r.Activo AS RolActivo FROM Usuarios u INNER JOIN Empleados e ON u.IdEmpleado = e.IdEmpleado INNER JOIN Roles r ON u.IdRol = r.IdRol WHERE u.Activo =1";
                
                if (id.HasValue)
                {
                    consulta += " AND u.IdUsuario = @id ";
                    datos.setearParametro("@Id", id.Value);
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.IdUsuario = (int)lector["IdUsuario"];
                    aux.Nombre = (string)lector["NombreUsuario"];
                    aux.Activo = (bool)lector["UsuarioActivo"];
                    aux.PasswordHash = (string)lector["PasswordHash"];

                    Empleado empleado = new Empleado();
                    empleado.IdEmpleado = (int)lector["EmpIdEmpleado"];
                    empleado.Nombre = (string)lector["Nombre"];
                    empleado.Apellido = (string)lector["Apellido"];
                    empleado.Telefono = (string)lector["Telefono"];
                    empleado.Email = (string)lector["Email"];
                    empleado.Dni = (string)lector["Dni"];
                    empleado.FechaIngreso = (DateTime)lector["FechaIngreso"];

                    aux.Empleado = empleado;

                    Rol rol = new Rol();
                    rol.IdRol = (int)lector["RolIdRol"];
                    rol.Nombre = (string)lector["NombreRol"];
                    rol.Descripcion = (string)lector["Descripcion"];
                    rol.Activo = (bool)lector["RolActivo"];

                    aux.Rol = rol;

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
        public void agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (IdEmpleado, IdRol, NombreUsuario, PasswordHash, Activo) VALUES (@IdEmpleado, @IdRol, @NombreUsuario, @PasswordHash, 1)");

                datos.setearParametro("@IdEmpleado", nuevo.Empleado.IdEmpleado);
                datos.setearParametro("@IdRol", nuevo.Rol.IdRol);
                datos.setearParametro("@NombreUsuario", nuevo.Nombre);
                datos.setearParametro("@PasswordHash", nuevo.PasswordHash);

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
        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET IdEmpleado = @IdEmpleado, IdRol = @IdRol, NombreUsuario = @NombreUsuario, PasswordHash = @PasswordHash, Activo = @Activo WHERE IdUsuario = @IdUsuario");

                datos.setearParametro("@IdUsuario", usuario.IdUsuario);
                datos.setearParametro("@IdEmpleado", usuario.Empleado.IdEmpleado);
                datos.setearParametro("@IdRol", usuario.Rol.IdRol);
                datos.setearParametro("@NombreUsuario", usuario.Nombre);
                datos.setearParametro("@PasswordHash", usuario.PasswordHash);
                datos.setearParametro("@Activo", usuario.Activo);

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
        public void eliminar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Activo = 0 WHERE IdUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", idUsuario);

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
        public List<Usuario> filtrar(string filtro)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT u.IdUsuario, u.NombreUsuario, u.Activo AS UsuarioActivo, u.PasswordHash,
                    e.IdEmpleado, e.Nombre, e.Apellido,
                    r.IdRol, r.NombreRol
                    FROM Usuarios u
                    INNER JOIN Empleados e ON u.IdEmpleado = e.IdEmpleado
                    INNER JOIN Roles r ON u.IdRol = r.IdRol
                    WHERE u.Activo = 1";

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    consulta += " AND (u.NombreUsuario LIKE @filtro OR e.Nombre LIKE @filtro OR e.Apellido LIKE @filtro)";
                }

                consulta += " ORDER BY u.NombreUsuario";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    datos.setearParametro("@filtro", "%" + filtro.Trim() + "%");
                }

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.IdUsuario = (int)lector["IdUsuario"];
                    aux.Nombre = (string)lector["NombreUsuario"];
                    aux.Activo = (bool)lector["UsuarioActivo"];
                    aux.PasswordHash = (string)lector["PasswordHash"];

                    aux.Empleado = new Empleado
                    {
                        IdEmpleado = (int)lector["IdEmpleado"],
                        Nombre = (string)lector["Nombre"],
                        Apellido = (string)lector["Apellido"]
                    };

                    aux.Rol = new Rol
                    {
                        IdRol = (int)lector["IdRol"],
                        Nombre = (string)lector["NombreRol"]
                    };

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
    }
}