using AppGestionNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppGestionNegocio.Negocio
{
    public class ArticuloNegocio
    {
        private const string CONSULTA = "SELECT A.IdArticulo, A.IdCategoria, A.IdMarca, A.IdAlicuotaIva, Ali.AlicuotaIva, A.Nombre AS NombreArticulo, C.Nombre AS Categoria, M.Nombre AS Marca, A.Descripcion, A.PrecioUnitario, A.Stock, A.UrlImagen, A.Activo FROM Articulos A JOIN Categorias C ON A.IdCategoria = C.IdCategoria JOIN AlicuotaIva Ali ON A.IdAlicuotaIva = Ali.IdAlicuotaIva JOIN Marcas M ON A.IdMarca = M.IdMarca WHERE A.Activo = 1";
        public List<Articulo> listar(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                string consulta = CONSULTA;
                if (id != "") consulta += " AND A.IdArticulo = " + id;

                accesoDatos.setearConsulta(consulta);
                accesoDatos.ejecutarLectura();
                SqlDataReader lector = accesoDatos.Lector;

                while (lector.Read())
                {
                    lista.Add(MapearArticulo(lector));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Articulos (IdCategoria, IdMarca, IdAlicuotaIva, Nombre, Descripcion, PrecioUnitario, Stock, UrlImagen, Activo) VALUES (@IdCategoria, @IdMarca, @IdAlicuotaIva, @Nombre, @Descripcion, @PrecioUnitario, @Stock, @UrlImagen, 1)");

                datos.setearParametro("@IdCategoria", nuevo.Categoria.IdCategoria);
                datos.setearParametro("@IdMarca", nuevo.Marca.IdMarca);
                datos.setearParametro("@IdAlicuotaIva", nuevo.AlicuotaIva.IdAlicuotaIva);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", (object)nuevo.Descripcion ?? DBNull.Value);
                datos.setearParametro("@PrecioUnitario", nuevo.Precio);
                datos.setearParametro("@Stock", nuevo.Stock);
                datos.setearParametro("@UrlImagen", (object)nuevo.UrlImagen ?? DBNull.Value);
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
        public void modificar(Articulo modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Articulos SET IdCategoria = @IdCategoria, IdMarca = @IdMarca, IdAlicuotaIva = @IdAlicuotaIva, Nombre = @Nombre, Descripcion = @Descripcion, PrecioUnitario = @PrecioUnitario, Stock = @Stock, UrlImagen = @UrlImagen WHERE IdArticulo = @IdArticulo");

                datos.setearParametro("@IdCategoria", modificado.Categoria.IdCategoria);
                datos.setearParametro("@IdMarca", modificado.Marca.IdMarca);
                datos.setearParametro("@IdAlicuotaIva", modificado.AlicuotaIva.IdAlicuotaIva);
                datos.setearParametro("@Nombre", modificado.Nombre);
                datos.setearParametro("@Descripcion", (object)modificado.Descripcion ?? DBNull.Value);
                datos.setearParametro("@PrecioUnitario", modificado.Precio);
                datos.setearParametro("@Stock", modificado.Stock);
                datos.setearParametro("@UrlImagen", (object)modificado.UrlImagen ?? DBNull.Value);
                datos.setearParametro("@IdArticulo", modificado.IdArticulo);
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
        public void eliminarLogico(int IdArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Articulos SET Activo = 0 WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", IdArticulo);

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
        private Articulo MapearArticulo(SqlDataReader lector)
        {
            Articulo aux = new Articulo();

            aux.IdArticulo = (int)lector["IdArticulo"];
            aux.Nombre = lector["NombreArticulo"].ToString();

            if (!(lector["Descripcion"] is DBNull)) aux.Descripcion = (string)lector["Descripcion"];

            aux.Precio = (decimal)lector["PrecioUnitario"];
            aux.Stock = Convert.ToInt32(lector["Stock"]);

            if (!(lector["UrlImagen"] is DBNull)) aux.UrlImagen = (string)lector["UrlImagen"];

            aux.Activo = (bool)lector["Activo"];

            aux.Categoria = new Categoria();
            aux.Categoria.IdCategoria = (int)lector["IdCategoria"];
            aux.Categoria.Nombre = lector["Categoria"].ToString();

            aux.Marca = new Marca();
            aux.Marca.IdMarca = (int)lector["IdMarca"];
            aux.Marca.Nombre = lector["Marca"].ToString();

            aux.AlicuotaIva = new AlicuotaIva();
            aux.AlicuotaIva.IdAlicuotaIva = (int)lector["IdAlicuotaIva"];
            aux.AlicuotaIva.Alicuota = (decimal)lector["AlicuotaIva"];
            return aux;
        }

        public List<Articulo> filtrar(string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = CONSULTA;

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    consulta += " AND A.Nombre LIKE @filtro";
                }

                consulta += " ORDER BY A.Nombre";

                datos.setearConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    datos.setearParametro("@filtro", "%" + filtro.Trim() + "%");
                }

                datos.ejecutarLectura();
                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    lista.Add(MapearArticulo(lector));
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

        public List<Articulo> listarOrdenado(string criterio)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                string consulta = CONSULTA;
                switch (criterio)
                {
                    case "StockMayorMenor":
                        consulta +=" ORDER BY A.Stock DESC";
                        break;
                    case "StockMenorMayor":
                        consulta += " ORDER BY A.Stock ASC";
                        break;
                    case "PrecioMayorMenor":
                        consulta += " ORDER BY A.PrecioUnitario DESC";
                        break;
                    case "PrecioMenorMayor":
                        consulta += " ORDER BY A.PrecioUnitario ASC";
                        break;
                }

                accesoDatos.setearConsulta(consulta);
                accesoDatos.ejecutarLectura();
                SqlDataReader lector = accesoDatos.Lector;

                while (lector.Read())
                {
                    lista.Add(MapearArticulo(lector));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

    }
}
