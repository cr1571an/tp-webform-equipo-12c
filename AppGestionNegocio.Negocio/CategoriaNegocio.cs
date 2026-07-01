using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar(bool verInactivos = false)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdCategoria, Nombre, Activo FROM Categorias WHERE Activo = @Activo ORDER BY Nombre");
                datos.setearParametro("@Activo", verInactivos ? 0 : 1);
                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.IdCategoria = (int)lector["IdCategoria"];
                    aux.Nombre = (string)lector["Nombre"];
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

        public void agregar(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Categorias (Nombre, Activo) VALUES (@Nombre, 1)");
                datos.setearParametro("@Nombre", nueva.Nombre);
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

        public void modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Categorias SET Nombre = @Nombre, Activo = @Activo WHERE IdCategoria = @IdCategoria");
                datos.setearParametro("@Nombre", categoria.Nombre);
                datos.setearParametro("@Activo", categoria.Activo);
                datos.setearParametro("@IdCategoria", categoria.IdCategoria);
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

        public void eliminar(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Categorias SET Activo = 0 WHERE IdCategoria = @IdCategoria");
                datos.setearParametro("@IdCategoria", idCategoria);
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

        public void restaurar(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Categorias SET Activo = 1 WHERE IdCategoria = @IdCategoria");
                datos.setearParametro("@IdCategoria", idCategoria);
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

        public List<Categoria> filtrar(string filtro, bool verInactivos = false)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT IdCategoria, Nombre, Activo FROM Categorias WHERE Activo = @Activo";

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    consulta += " AND Nombre LIKE @filtro";
                }

                consulta += " ORDER BY Nombre";

                datos.setearConsulta(consulta);
                datos.setearParametro("@Activo", verInactivos ? 0 : 1);

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    datos.setearParametro("@filtro", "%" + filtro.Trim() + "%");
                }

                datos.ejecutarLectura();

                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.IdCategoria = (int)lector["IdCategoria"];
                    aux.Nombre = (string)lector["Nombre"];
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