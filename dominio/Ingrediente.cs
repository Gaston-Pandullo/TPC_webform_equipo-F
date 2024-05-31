using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    sealed class Ingrediente
    {
        public Ingrediente() 
        {
            nombre = "";
            precio = 0;
            stock = 0;
            estado = true;
        }
        public int id { get; set; }
        public string nombre { get; set; }
        public int precio { get; set; }
        public int stock { get; set; }
        public bool estado { get; set; }
    }
}
