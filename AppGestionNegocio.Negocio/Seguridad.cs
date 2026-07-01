using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppGestionNegocio.Dominio;

namespace AppGestionNegocio.Negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            if (usuario != null && usuario.IdUsuario != 0)
                return true;
            else
                return false;
        }

        public static bool esAdmin(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            if (usuario != null && usuario.Rol != null)
                return usuario.Rol.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
            else
                return false;
        }
    }
}
