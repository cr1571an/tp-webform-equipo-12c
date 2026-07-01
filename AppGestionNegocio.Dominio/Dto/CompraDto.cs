using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class CompraDto
    {

        public int IdCompra { get; set; }
        public int IdProveedor { get; set; }
        public int IdMedioPago { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaCompra { get; set; }
        public string NumeroComprobante { get; set; }
        public string Observaciones { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompraDto> Detalles { get; set; }
    }
}
