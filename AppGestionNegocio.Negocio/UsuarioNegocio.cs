using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class UsuarioNegocio
    {
        private const string CONSULTA = "SELECT u.IdUsuario, u.IdEmpleado, u.IdRol, u.NombreUsuario, u.Activo AS UsuarioActivo, u.PasswordHash, e.IdEmpleado AS EmpIdEmpleado, e.Nombre, e.Apellido, e.Telefono, e.Email, e.Dni, e.FechaIngreso, e.Activo AS EmpleadoActivo, r.IdRol AS RolIdRol, r.NombreRol, r.Descripcion, r.Activo AS RolActivo FROM Usuarios u INNER JOIN Empleados e ON u.IdEmpleado = e.IdEmpleado INNER JOIN Roles r ON u.IdRol = r.IdRol ";

        public List<Usuario> listar(int? id = null, bool verInactivos = false)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = CONSULTA + "WHERE u.Activo = @Activo ";

                if (id.HasValue)
                {
                    consulta += "AND u.IdUsuario = @IdUsuario ";
                }

                consulta += "ORDER BY u.NombreUsuario";

                datos.setearConsulta(consulta);
                datos.setearParametro("@Activo", verInactivos ? 0 : 1);

                if (id.HasValue)
                {
                    datos.setearParametro("@IdUsuario", id.Value);
                }

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Usuario aux = cargarUsuario(lector);
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
            catch (Exception)
            {
                throw;
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

        public void restaurar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Activo = 1 WHERE IdUsuario = @IdUsuario");
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

        public bool existeNombreUsuario(string nombreUsuario, int? idUsuarioActual = null)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdUsuario FROM Usuarios WHERE UPPER(NombreUsuario) = UPPER(@NombreUsuario) ";

                if (idUsuarioActual.HasValue)
                {
                    consulta += "AND IdUsuario <> @IdUsuario ";
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@NombreUsuario", nombreUsuario.Trim());

                if (idUsuarioActual.HasValue)
                {
                    datos.setearParametro("@IdUsuario", idUsuarioActual.Value);
                }

                datos.ejecutarLectura();

                return datos.Lector.Read();
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

        public bool Login(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT u.IdUsuario, u.IdEmpleado, u.IdRol, u.NombreUsuario, u.PasswordHash, u.Activo AS UsuarioActivo, e.IdEmpleado AS EmpIdEmpleado, e.Nombre, e.Apellido, e.Email, r.IdRol AS RolIdRol, r.NombreRol, r.Descripcion, r.Activo AS RolActivo FROM Usuarios u INNER JOIN Empleados e ON u.IdEmpleado = e.IdEmpleado INNER JOIN Roles r ON u.IdRol = r.IdRol WHERE (u.NombreUsuario = @login OR e.Email = @login) AND u.PasswordHash = @password AND u.Activo = 1");
                datos.setearParametro("@login", usuario.Nombre);
                datos.setearParametro("@password", usuario.PasswordHash);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
                    usuario.Nombre = (string)datos.Lector["NombreUsuario"];
                    usuario.PasswordHash = (string)datos.Lector["PasswordHash"];
                    usuario.Activo = (bool)datos.Lector["UsuarioActivo"];

                    Empleado empleado = new Empleado();
                    empleado.IdEmpleado = (int)datos.Lector["EmpIdEmpleado"];
                    empleado.Nombre = (string)datos.Lector["Nombre"];
                    empleado.Apellido = (string)datos.Lector["Apellido"];
                    empleado.Email = (string)datos.Lector["Email"];
                    usuario.Empleado = empleado;

                    Rol rol = new Rol();
                    rol.IdRol = (int)datos.Lector["RolIdRol"];
                    rol.Nombre = (string)datos.Lector["NombreRol"];
                    rol.Descripcion = (string)datos.Lector["Descripcion"];
                    rol.Activo = (bool)datos.Lector["RolActivo"];
                    usuario.Rol = rol;

                    return true;
                }

                return false;
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

        public List<Usuario> filtrar(int idRol, bool verInactivos = false)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = CONSULTA + "WHERE u.Activo = @Activo ";

                if (idRol > 0)
                {
                    consulta += "AND u.IdRol = @IdRol ";
                }

                consulta += "ORDER BY u.NombreUsuario";

                datos.setearConsulta(consulta);
                datos.setearParametro("@Activo", verInactivos ? 0 : 1);

                if (idRol > 0)
                {
                    datos.setearParametro("@IdRol", idRol);
                }

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Usuario aux = cargarUsuario(lector);
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

        private Usuario cargarUsuario(SqlDataReader lector)
        {
            Usuario aux = new Usuario();

            aux.IdUsuario = (int)lector["IdUsuario"];
            aux.Nombre = lector["NombreUsuario"].ToString();
            aux.Activo = (bool)lector["UsuarioActivo"];
            aux.PasswordHash = lector["PasswordHash"].ToString();

            Empleado empleado = new Empleado();
            empleado.IdEmpleado = (int)lector["EmpIdEmpleado"];
            empleado.Nombre = lector["Nombre"] != DBNull.Value ? lector["Nombre"].ToString() : "";
            empleado.Apellido = lector["Apellido"] != DBNull.Value ? lector["Apellido"].ToString() : "";
            empleado.Telefono = lector["Telefono"] != DBNull.Value ? lector["Telefono"].ToString() : "";
            empleado.Email = lector["Email"] != DBNull.Value ? lector["Email"].ToString() : "";
            empleado.Dni = lector["Dni"] != DBNull.Value ? lector["Dni"].ToString() : "";

            if (lector["FechaIngreso"] != DBNull.Value)
            {
                empleado.FechaIngreso = (DateTime)lector["FechaIngreso"];
            }

            empleado.Activo = (bool)lector["EmpleadoActivo"];

            aux.Empleado = empleado;

            Rol rol = new Rol();
            rol.IdRol = (int)lector["RolIdRol"];
            rol.Nombre = lector["NombreRol"] != DBNull.Value ? lector["NombreRol"].ToString() : "";
            rol.Descripcion = lector["Descripcion"] != DBNull.Value ? lector["Descripcion"].ToString() : "";
            rol.Activo = (bool)lector["RolActivo"];

            aux.Rol = rol;

            return aux;
        }
    }
}