using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{

    public class Usuario
    {
        public int id { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Pass { get; set; }
        public byte TipoUsuario { get; set; }

        Usuario(string nombre_usuario, string nombre, string apellido, string pass, byte tipo)
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
            this.TipoUsuario = (byte)(admin ? 1 : 0);
        }

        public Usuario(){ }

    
    }
}
