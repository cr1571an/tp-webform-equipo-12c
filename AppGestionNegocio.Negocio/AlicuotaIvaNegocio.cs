using AppGestionNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Negocio
{
    public class AlicuotaIvaNegocio
    {
        public List<AlicuotaIva> listar()
        {
            List<AlicuotaIva> lista = new List<AlicuotaIva>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdAlicuotaIva, AlicuotaIva, Activo FROM AlicuotaIva WHERE Activo = 1 ORDER BY AlicuotaIva");
                datos.ejecutarLectura();
                SqlDataReader lector = datos.Lector;

                while (lector.Read())
                {
                    AlicuotaIva aux = new AlicuotaIva();

                    aux.IdAlicuotaIva = (int)lector["IdAlicuotaIva"];
                    aux.Alicuota = (decimal)lector["AlicuotaIva"];
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
