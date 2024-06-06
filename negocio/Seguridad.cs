using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            Usuario usuario = user as Usuario;
            if (usuario != null && usuario.id != 0)
                return true;
            else
                return false;
        }
    }
}
