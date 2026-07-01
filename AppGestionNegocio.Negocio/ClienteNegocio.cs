using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class ClienteNegocio
    {
        private const string CONSULTA = "SELECT c.IdCliente, c.IdCondicionIva, c.Cuit, c.Nombre, c.Apellido, " + "c.Telefono, c.Email, c.Cp, c.Domicilio, c.Activo, ci.Condicion " + "FROM Clientes c " + "JOIN CondicionIva ci ON ci.IdCondicionIva = c.IdCondicionIva " + "WHERE c.Activo = 1 ";

        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                string consulta = CONSULTA + "ORDER BY c.Nombre, c.Apellido";

                accesoDatos.setearConsulta(consulta);
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
                string consulta = CONSULTA + "AND c.IdCliente = @IdCliente";

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
                datos.setearConsulta("INSERT INTO Clientes " + "(IdCondicionIva, Cuit, Nombre, Apellido, Telefono, Email, Cp, Domicilio, Activo) " + "VALUES " + "(@IdCondicionIva, @Cuit, @Nombre, @Apellido, @Telefono, @Email, @Cp, @Domicilio, 1)");
                datos.setearParametro("@IdCondicionIva", nuevo.CondicionIva.IdCondicionIva);
                datos.setearParametro("@Cuit", string.IsNullOrWhiteSpace(nuevo.Cuit) ? (object)DBNull.Value : nuevo.Cuit);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Telefono", string.IsNullOrWhiteSpace(nuevo.Telefono) ? (object)DBNull.Value : nuevo.Telefono);
                datos.setearParametro("@Email", string.IsNullOrWhiteSpace(nuevo.Email) ? (object)DBNull.Value : nuevo.Email);
                datos.setearParametro("@Cp", string.IsNullOrWhiteSpace(nuevo.Cp) ? (object)DBNull.Value : nuevo.Cp);
                datos.setearParametro("@Domicilio", string.IsNullOrWhiteSpace(nuevo.Domicilio) ? (object)DBNull.Value : nuevo.Domicilio);

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
                datos.setearConsulta("UPDATE Clientes SET " + "IdCondicionIva = @IdCondicionIva, " + "Cuit = @Cuit, " + "Nombre = @Nombre, " + "Apellido = @Apellido, " + "Telefono = @Telefono, " + "Email = @Email, " + "Cp = @Cp, " + "Domicilio = @Domicilio " + "WHERE IdCliente = @IdCliente");
                datos.setearParametro("@IdCondicionIva", cliente.CondicionIva.IdCondicionIva);
                datos.setearParametro("@Cuit", string.IsNullOrWhiteSpace(cliente.Cuit) ? (object)DBNull.Value : cliente.Cuit);
                datos.setearParametro("@Nombre", cliente.Nombre);
                datos.setearParametro("@Apellido", cliente.Apellido);
                datos.setearParametro("@Telefono", string.IsNullOrWhiteSpace(cliente.Telefono) ? (object)DBNull.Value : cliente.Telefono);
                datos.setearParametro("@Email", string.IsNullOrWhiteSpace(cliente.Email) ? (object)DBNull.Value : cliente.Email);
                datos.setearParametro("@Cp", string.IsNullOrWhiteSpace(cliente.Cp) ? (object)DBNull.Value : cliente.Cp);
                datos.setearParametro("@Domicilio", string.IsNullOrWhiteSpace(cliente.Domicilio) ? (object)DBNull.Value : cliente.Domicilio);
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

        public List<Cliente> filtrar(string filtro)
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                string consulta = CONSULTA;

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    consulta += "AND (c.Nombre LIKE @filtro OR c.Apellido LIKE @filtro OR c.Cuit LIKE @filtro OR (c.Nombre + ' ' + c.Apellido) LIKE @filtro) ";
                }

                consulta += "ORDER BY c.Nombre, c.Apellido";

                accesoDatos.setearConsulta(consulta);

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