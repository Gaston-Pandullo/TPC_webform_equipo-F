using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Usuario
    {
        Usuario(string nombre_usuario, string nombreYapellido, string contrasenia, bool esGerente)
        {
            this.nombre_usuario = nombre_usuario;
            this.nombreYapellido = nombreYapellido;
            this.contrasenia = contrasenia;
            this.esGerente = esGerente;
        }

        Usuario(string nombre_usuario, string contrasenia)
        {
            this.nombre_usuario = nombre_usuario;
            nombreYapellido="";
            this.contrasenia = contrasenia;
            esGerente = false;
        }

        public Usuario(){ }

        public int id { get; set; }
        public string nombre_usuario { get; set; }
        public string nombreYapellido { get; set; }
        public string contrasenia { get; set; }
        public bool esGerente { get; set; }
    
    }
}
