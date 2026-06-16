using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdProveedor, Nombre, Telefono, Email, Activo FROM Proveedores WHERE Activo = 1 ORDER BY Nombre");
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Proveedor aux = new Proveedor();

                    aux.IdProveedor = (int)lector["IdProveedor"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Telefono = (string)lector["Telefono"];
                    aux.Email = (string)lector["Email"];
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
        public void agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Proveedores (Nombre, Telefono, Email, Activo) VALUES (@Nombre, @Telefono, @Email, 1)");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Email", nuevo.Email);
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
        public void modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email, Activo = @Activo WHERE IdProveedor = @IdProveedor");
                datos.setearParametro("@Nombre", proveedor.Nombre);
                datos.setearParametro("@Telefono", proveedor.Telefono);
                datos.setearParametro("@Email", proveedor.Email);
                datos.setearParametro("@Activo", proveedor.Activo);
                datos.setearParametro("@IdProveedor", proveedor.IdProveedor);
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
        public void eliminar(int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Activo = 0 WHERE IdProveedor = @IdProveedor");
                datos.setearParametro("@IdProveedor", idProveedor);
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
        public List<Proveedor> filtrar(string filtro)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdProveedor, Nombre, Telefono, Email, Activo FROM Proveedores WHERE Activo = 1";

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
                    Proveedor aux = new Proveedor();

                    aux.IdProveedor = (int)lector["IdProveedor"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Telefono = (string)lector["Telefono"];
                    aux.Email = (string)lector["Email"];
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