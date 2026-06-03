using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }
        public DateTime FechaIngreso { get; set; }
        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }
    }
}
