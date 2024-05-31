using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Bebidas
    {
        public Bebidas() { }
        public int id {  get; set; }
        public string nombre { get; set; }
        public int precio { get; set; }
        public int stock { get; set; }
        public bool estado { get; set; }
        public bool alcoholica { get; set; }
    }
}
