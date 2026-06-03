using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public MedioPago MedioPago { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroFactura { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVenta> DetallesVenta { get; set; }
    }
}
