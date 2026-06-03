using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class AlicuotaIva
    {
        public int IdAlicuotaIva { get; set; }
        public decimal Alicuota { get; set; }
        public List<Articulo> Articulos { get; set; }
        public override string ToString()
        {
            return Alicuota.ToString() + "%";
        }
    }
}
