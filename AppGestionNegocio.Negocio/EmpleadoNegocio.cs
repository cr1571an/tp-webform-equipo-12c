using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string consulta = "SELECT IdEmpleado, Nombre, Apellido, Telefono, Email, Dni, FechaIngreso FROM Empleados WHERE Activo = 1";

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
                    Empleado aux = new Empleado();

                    aux.IdEmpleado = (int)lector["IdEmpleado"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Apellido = (string)lector["Apellido"];
                    aux.Telefono = lector["Telefono"] as string;
                    aux.Email = lector["Email"] as string;
                    aux.Dni = lector["Dni"] as string;
                    aux.FechaIngreso = (DateTime)lector["FechaIngreso"];

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
    }
}