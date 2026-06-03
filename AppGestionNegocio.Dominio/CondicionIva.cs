using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class CondicionIva
    {
        public int IdCondicionIva { get; set; }
        public string Condicion { get; set; }
        public override string ToString()
        {
            return Condicion;
        }
    }
}
