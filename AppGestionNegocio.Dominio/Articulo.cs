using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
        public AlicuotaIva AlicuotaIva { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string UrlImagen { get; set; }
        public List<Proveedor> Proveedores { get; set; }
        public List<DetalleCompra> DetallesCompra { get; set; }
        public List<DetalleVenta> DetallesVenta { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
