using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Marca
    {
        public int IdMarca { get; set; }
        public string Nombre { get; set; }
        public List<Articulo> Articulos { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
