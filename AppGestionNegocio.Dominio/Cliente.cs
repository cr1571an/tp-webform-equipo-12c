using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public CondicionIva CondicionIva { get; set; }
        public string Cuit { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Cp { get; set; }
        public string Domicilio { get; set; }
        public List<Venta> Ventas { get; set; }
        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }
    }
}
