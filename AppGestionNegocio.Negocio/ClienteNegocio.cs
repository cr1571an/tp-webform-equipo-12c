using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class ClienteNegocio
    {
        private const string CONSULTA = "SELECT c.IdCliente, c.IdCondicionIva, c.Cuit, c.Nombre, c.Apellido, c.Telefono, c.Email, c.Cp, c.Domicilio, c.Activo, ci.Condicion FROM Clientes c JOIN CondicionIva ci ON ci.IdCondicionIva = c.IdCondicionIva ";

        public List<Cliente> listar(bool verInactivos = false)
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                string consulta = CONSULTA + "WHERE c.Activo = @Activo ORDER BY c.Nombre, c.Apellido";

                accesoDatos.setearConsulta(consulta);
                accesoDatos.setearParametro("@Activo", verInactivos ? 0 : 1);
                accesoDatos.ejecutarLectura();

                SqlDataReader lector = accesoDatos.Lector;

                while (lector.Read())
                {
                    Cliente aux = cargarCliente(lector);
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
                accesoDatos.cerrarConexion();
            }
        }

        public Cliente obtenerPorId(int idCliente)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                string consulta = CONSULTA + "WHERE c.Activo = 1 AND c.IdCliente = @IdCliente";

                accesoDatos.setearConsulta(consulta);
                accesoDatos.setearParametro("@IdCliente", idCliente);
                accesoDatos.ejecutarLectura();

                SqlDataReader lector = accesoDatos.Lector;

                if (lector.Read())
                {
                    return cargarCliente(lector);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public void agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Clientes (IdCondicionIva, Cuit, Nombre, Apellido, Telefono, Email, Cp, Domicilio, Activo) VALUES (@IdCondicionIva, @Cuit, @Nombre, @Apellido, @Telefono, @Email, @Cp, @Domicilio, 1)");
                datos.setearParametro("@IdCondicionIva", nuevo.CondicionIva.IdCondicionIva);
                datos.setearParametro("@Cuit", string.IsNullOrWhiteSpace(nuevo.Cuit) ? (object)DBNull.Value : nuevo.Cuit);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Cp", nuevo.Cp);
                datos.setearParametro("@Domicilio", nuevo.Domicilio);

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

        public void modificar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Clientes SET IdCondicionIva = @IdCondicionIva, Cuit = @Cuit, Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, Email = @Email, Cp = @Cp, Domicilio = @Domicilio WHERE IdCliente = @IdCliente");
                datos.setearParametro("@IdCondicionIva", cliente.CondicionIva.IdCondicionIva);
                datos.setearParametro("@Cuit", string.IsNullOrWhiteSpace(cliente.Cuit) ? (object)DBNull.Value : cliente.Cuit);
                datos.setearParametro("@Nombre", cliente.Nombre);
                datos.setearParametro("@Apellido", cliente.Apellido);
                datos.setearParametro("@Telefono", cliente.Telefono);
                datos.setearParametro("@Email", cliente.Email);
                datos.setearParametro("@Cp", cliente.Cp);
                datos.setearParametro("@Domicilio", cliente.Domicilio);
                datos.setearParametro("@IdCliente", cliente.IdCliente);

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

        public void eliminar(int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Clientes SET Activo = 0 WHERE IdCliente = @IdCliente");
                datos.setearParametro("@IdCliente", idCliente);
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

        public void restaurar(int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Clientes SET Activo = 1 WHERE IdCliente = @IdCliente");
                datos.setearParametro("@IdCliente", idCliente);
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

        public List<Cliente> filtrar(string filtro, bool verInactivos = false)
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                string consulta = CONSULTA + "WHERE c.Activo = @Activo ";

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    consulta += "AND (c.Nombre LIKE @filtro OR c.Apellido LIKE @filtro OR c.Cuit LIKE @filtro OR (c.Nombre + ' ' + c.Apellido) LIKE @filtro) ";
                }

                consulta += "ORDER BY c.Nombre, c.Apellido";

                accesoDatos.setearConsulta(consulta);
                accesoDatos.setearParametro("@Activo", verInactivos ? 0 : 1);

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    accesoDatos.setearParametro("@filtro", "%" + filtro.Trim() + "%");
                }

                accesoDatos.ejecutarLectura();

                SqlDataReader lector = accesoDatos.Lector;

                while (lector.Read())
                {
                    Cliente aux = cargarCliente(lector);
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
                accesoDatos.cerrarConexion();
            }
        }

        public bool existeEmail(string email, int? idClienteActual = null)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdCliente FROM Clientes WHERE UPPER(Email) = UPPER(@Email) ";

                if (idClienteActual.HasValue)
                {
                    consulta += "AND IdCliente <> @IdCliente ";
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@Email", email.Trim());

                if (idClienteActual.HasValue)
                {
                    datos.setearParametro("@IdCliente", idClienteActual.Value);
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

        private Cliente cargarCliente(SqlDataReader lector)
        {
            Cliente aux = new Cliente();

            aux.IdCliente = (int)lector["IdCliente"];
            aux.Cuit = lector["Cuit"] != DBNull.Value ? lector["Cuit"].ToString() : "";
            aux.Nombre = lector["Nombre"] != DBNull.Value ? lector["Nombre"].ToString() : "";
            aux.Apellido = lector["Apellido"] != DBNull.Value ? lector["Apellido"].ToString() : "";
            aux.Telefono = lector["Telefono"] != DBNull.Value ? lector["Telefono"].ToString() : "";
            aux.Email = lector["Email"] != DBNull.Value ? lector["Email"].ToString() : "";
            aux.Cp = lector["Cp"] != DBNull.Value ? lector["Cp"].ToString() : "";
            aux.Domicilio = lector["Domicilio"] != DBNull.Value ? lector["Domicilio"].ToString() : "";
            aux.Activo = (bool)lector["Activo"];

            aux.CondicionIva = new CondicionIva();
            aux.CondicionIva.IdCondicionIva = (int)lector["IdCondicionIva"];
            aux.CondicionIva.Condicion = lector["Condicion"] != DBNull.Value ? lector["Condicion"].ToString() : "";

            return aux;
        }
    }
}