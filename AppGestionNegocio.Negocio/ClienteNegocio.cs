using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos accesoDatos = new AccesoDatos();
            

            try
            {
                accesoDatos.setearConsulta("SELECT c.IdCliente, c.IdCondicionIva, c.Cuit, c.Nombre, c.Apellido, c.Telefono, c.Email, c.Cp, c.Domicilio, c.Activo, ci.Condicion FROM Clientes c JOIN CondicionIva ci ON ci.IdCondicionIva = c.IdCondicionIva WHERE c.Activo = 1;");
                accesoDatos.ejecutarLectura();
                SqlDataReader lector = accesoDatos.Lector;

                while (lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.IdCliente = (int)lector["IdCliente"];
                    aux.Cuit = lector["Cuit"] != DBNull.Value ? (string)lector["Cuit"] : "";
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Apellido = (string)lector["Apellido"];
                    aux.Telefono = (string)lector["Telefono"];
                    aux.Email = (string)lector["Email"];
                    aux.Cp = (string)lector["Cp"];
                    aux.Domicilio = (string)lector["Domicilio"];
                    aux.Activo = bool.Parse(lector["Activo"].ToString());
                                                                              
                    aux.CondicionIva = new CondicionIva();
                    aux.CondicionIva.IdCondicionIva = (int)lector["IdCondicionIva"];
                    aux.CondicionIva.Condicion = (string)lector["Condicion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
