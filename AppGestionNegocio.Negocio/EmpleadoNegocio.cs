using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class EmpleadoNegocio
    {
        public List<Empleado> listar(int? id = null)
        {
            List<Empleado> lista = new List<Empleado>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdEmpleado, Nombre, Apellido, Telefono, Email, Dni, FechaIngreso, Activo FROM Empleados WHERE Activo = 1";

                if (id.HasValue)
                {
                    consulta += " AND IdEmpleado = @IdEmpleado";
                    datos.setearParametro("@IdEmpleado", id.Value);
                }

                consulta += " ORDER BY Nombre";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Empleado aux = cargarEmpleado(lector);
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

        public int agregar(Empleado nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Empleados (Nombre, Apellido, Telefono, Email, Dni, FechaIngreso, Activo) VALUES (@Nombre, @Apellido, @Telefono, @Email, @Dni, @FechaIngreso, 1); SELECT CAST(SCOPE_IDENTITY() AS INT) AS IdEmpleado");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Dni", nuevo.Dni);
                datos.setearParametro("@FechaIngreso", nuevo.FechaIngreso);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IdEmpleado"];
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

        public void modificar(Empleado empleado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Empleados SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Email = @Email, Dni = @Dni, FechaIngreso = @FechaIngreso WHERE IdEmpleado = @IdEmpleado");
                datos.setearParametro("@Nombre", empleado.Nombre);
                datos.setearParametro("@Apellido", empleado.Apellido);
                datos.setearParametro("@Telefono", empleado.Telefono);
                datos.setearParametro("@Email", empleado.Email);
                datos.setearParametro("@Dni", empleado.Dni);
                datos.setearParametro("@FechaIngreso", empleado.FechaIngreso);
                datos.setearParametro("@IdEmpleado", empleado.IdEmpleado);

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

        public bool existeDni(string dni, int? idEmpleadoActual = null)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdEmpleado FROM Empleados WHERE Dni = @Dni ";

                if (idEmpleadoActual.HasValue)
                {
                    consulta += "AND IdEmpleado <> @IdEmpleado ";
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@Dni", dni.Trim());

                if (idEmpleadoActual.HasValue)
                {
                    datos.setearParametro("@IdEmpleado", idEmpleadoActual.Value);
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

        public bool existeEmail(string email, int? idEmpleadoActual = null)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdEmpleado FROM Empleados WHERE UPPER(Email) = UPPER(@Email) ";

                if (idEmpleadoActual.HasValue)
                {
                    consulta += "AND IdEmpleado <> @IdEmpleado ";
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@Email", email.Trim());

                if (idEmpleadoActual.HasValue)
                {
                    datos.setearParametro("@IdEmpleado", idEmpleadoActual.Value);
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

        private Empleado cargarEmpleado(SqlDataReader lector)
        {
            Empleado aux = new Empleado();

            aux.IdEmpleado = (int)lector["IdEmpleado"];
            aux.Nombre = lector["Nombre"].ToString();
            aux.Apellido = lector["Apellido"].ToString();
            aux.Telefono = lector["Telefono"].ToString();
            aux.Email = lector["Email"].ToString();
            aux.Dni = lector["Dni"].ToString();
            aux.FechaIngreso = (DateTime)lector["FechaIngreso"];
            aux.Activo = (bool)lector["Activo"];

            return aux;
        }
    }
}