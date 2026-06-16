using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public List<Articulo> Articulos { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
