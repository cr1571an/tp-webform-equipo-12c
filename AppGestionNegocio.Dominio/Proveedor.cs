using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public List<Articulo> Articulos { get; set; }
        public List<Compra> Compras { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
