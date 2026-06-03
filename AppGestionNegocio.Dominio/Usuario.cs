using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestionNegocio.Dominio
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public Empleado Empleado { get; set; }
        public Rol Rol { get; set; }
        public string Nombre { get; set; }
        public string PasswordHash { get; set; }
        public bool Activo { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
