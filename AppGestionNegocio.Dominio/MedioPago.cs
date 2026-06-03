using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class MedioPago
    {
        public int IdMedioPago { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Compra> Compras { get; set; }
        public List<Venta> Ventas { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
