using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum UserType
    {
        NORMAL = 0,
        ADMIN = 1,
    }

    public class Usuario
    {
        public int id { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Pass { get; set; }
        public UserType TipoUsuario { get; set; }

        Usuario(string nombre_usuario, string nombre, string apellido, string pass, UserType tipo)
        {
            this.User = nombre_usuario;
            this.Name = nombre;
            this.Lastname = apellido;
            this.Pass = pass;
            this.TipoUsuario = tipo;
        }

        public Usuario(string nombre_usuario, string pass, bool admin)
        {
            this.User = nombre_usuario;
            this.Name = "";
            this.Lastname = "";
            this.Pass = pass;
            this.TipoUsuario = admin ? UserType.ADMIN : UserType.NORMAL;
        }

        public Usuario(){ }

    
    }
}
