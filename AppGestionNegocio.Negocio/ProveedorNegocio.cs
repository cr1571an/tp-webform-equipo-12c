using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> listar(bool verInactivos = false)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdProveedor, Nombre, Cuit, Telefono, Email, Domicilio, Cp, PersonaContacto, Observaciones, Activo FROM Proveedores WHERE Activo = @Activo ORDER BY Nombre");
                datos.setearParametro("@Activo", verInactivos ? 0 : 1);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Proveedor aux = cargarProveedor(lector);
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

        public Proveedor obtenerPorId(int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdProveedor, Nombre, Cuit, Telefono, Email, Domicilio, Cp, PersonaContacto, Observaciones, Activo FROM Proveedores WHERE IdProveedor = @IdProveedor AND Activo = 1");
                datos.setearParametro("@IdProveedor", idProveedor);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                if (lector.Read())
                {
                    return cargarProveedor(lector);
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

        public void agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Proveedores (Nombre, Cuit, Telefono, Email, Domicilio, Cp, PersonaContacto, Observaciones, Activo) VALUES (@Nombre, @Cuit, @Telefono, @Email, @Domicilio, @Cp, @PersonaContacto, @Observaciones, 1)");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Cuit", nuevo.Cuit);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Domicilio", nuevo.Domicilio);
                datos.setearParametro("@Cp", string.IsNullOrWhiteSpace(nuevo.Cp) ? (object)DBNull.Value : nuevo.Cp);
                datos.setearParametro("@PersonaContacto", string.IsNullOrWhiteSpace(nuevo.PersonaContacto) ? (object)DBNull.Value : nuevo.PersonaContacto);
                datos.setearParametro("@Observaciones", string.IsNullOrWhiteSpace(nuevo.Observaciones) ? (object)DBNull.Value : nuevo.Observaciones);

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

        public void modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Nombre = @Nombre, Cuit = @Cuit, Telefono = @Telefono, Email = @Email, Domicilio = @Domicilio, Cp = @Cp, PersonaContacto = @PersonaContacto, Observaciones = @Observaciones, Activo = @Activo WHERE IdProveedor = @IdProveedor");
                datos.setearParametro("@Nombre", proveedor.Nombre);
                datos.setearParametro("@Cuit", proveedor.Cuit);
                datos.setearParametro("@Telefono", proveedor.Telefono);
                datos.setearParametro("@Email", proveedor.Email);
                datos.setearParametro("@Domicilio", proveedor.Domicilio);
                datos.setearParametro("@Cp", string.IsNullOrWhiteSpace(proveedor.Cp) ? (object)DBNull.Value : proveedor.Cp);
                datos.setearParametro("@PersonaContacto", string.IsNullOrWhiteSpace(proveedor.PersonaContacto) ? (object)DBNull.Value : proveedor.PersonaContacto);
                datos.setearParametro("@Observaciones", string.IsNullOrWhiteSpace(proveedor.Observaciones) ? (object)DBNull.Value : proveedor.Observaciones);
                datos.setearParametro("@Activo", proveedor.Activo);
                datos.setearParametro("@IdProveedor", proveedor.IdProveedor);

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

        public void eliminar(int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Activo = 0 WHERE IdProveedor = @IdProveedor");
                datos.setearParametro("@IdProveedor", idProveedor);
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

        public void restaurar(int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Activo = 1 WHERE IdProveedor = @IdProveedor");
                datos.setearParametro("@IdProveedor", idProveedor);
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

        public List<Proveedor> filtrar(string filtro, string cp, bool verInactivos = false)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdProveedor, Nombre, Cuit, Telefono, Email, Domicilio, Cp, PersonaContacto, Observaciones, Activo FROM Proveedores WHERE Activo = @Activo ";

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    consulta += "AND (Nombre LIKE @filtro OR Cuit LIKE @filtro) ";
                }

                if (!string.IsNullOrWhiteSpace(cp))
                {
                    consulta += "AND Cp LIKE @cp ";
                }

                consulta += "ORDER BY Nombre";

                datos.setearConsulta(consulta);
                datos.setearParametro("@Activo", verInactivos ? 0 : 1);

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    datos.setearParametro("@filtro", "%" + filtro.Trim() + "%");
                }

                if (!string.IsNullOrWhiteSpace(cp))
                {
                    datos.setearParametro("@cp", "%" + cp.Trim() + "%");
                }

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Proveedor aux = cargarProveedor(lector);
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

        public bool existeNombre(string nombre, int? idProveedorActual = null)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdProveedor FROM Proveedores WHERE UPPER(Nombre) = UPPER(@Nombre) ";

                if (idProveedorActual.HasValue)
                {
                    consulta += "AND IdProveedor <> @IdProveedor ";
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombre", nombre.Trim());

                if (idProveedorActual.HasValue)
                {
                    datos.setearParametro("@IdProveedor", idProveedorActual.Value);
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

        public bool existeCuit(string cuit, int? idProveedorActual = null)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdProveedor FROM Proveedores WHERE Cuit = @Cuit ";

                if (idProveedorActual.HasValue)
                {
                    consulta += "AND IdProveedor <> @IdProveedor ";
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@Cuit", cuit.Trim());

                if (idProveedorActual.HasValue)
                {
                    datos.setearParametro("@IdProveedor", idProveedorActual.Value);
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

        public bool existeEmail(string email, int? idProveedorActual = null)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdProveedor FROM Proveedores WHERE UPPER(Email) = UPPER(@Email) ";

                if (idProveedorActual.HasValue)
                {
                    consulta += "AND IdProveedor <> @IdProveedor ";
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@Email", email.Trim());

                if (idProveedorActual.HasValue)
                {
                    datos.setearParametro("@IdProveedor", idProveedorActual.Value);
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

        private Proveedor cargarProveedor(SqlDataReader lector)
        {
            Proveedor aux = new Proveedor();

            aux.IdProveedor = (int)lector["IdProveedor"];
            aux.Nombre = lector["Nombre"].ToString();
            aux.Cuit = lector["Cuit"].ToString();
            aux.Telefono = lector["Telefono"].ToString();
            aux.Email = lector["Email"].ToString();
            aux.Domicilio = lector["Domicilio"].ToString();
            aux.Cp = lector["Cp"] != DBNull.Value ? lector["Cp"].ToString() : "";
            aux.PersonaContacto = lector["PersonaContacto"] != DBNull.Value ? lector["PersonaContacto"].ToString() : "";
            aux.Observaciones = lector["Observaciones"] != DBNull.Value ? lector["Observaciones"].ToString() : "";
            aux.Activo = bool.Parse(lector["Activo"].ToString());

            return aux;
        }
    }
}