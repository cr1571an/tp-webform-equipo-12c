using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
        public MedioPago MedioPago { get; set; }
        public DateTime FechaCompra { get; set; }
        public string NumeroComprobante { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
        public List<DetalleCompra> DetallesCompra { get; set; }
    }
}
