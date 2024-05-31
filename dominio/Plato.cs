using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Plato
    {
        public Plato() 
        {
            nombre = "";
            descripcion = "";
            preparable = true;
            precio = 0;
            estado = true;
        }
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public List<Ingrediente> lista_ingredientes { get; set; }
        public bool preparable { get; set; }
        public int precio { get; set; }
        public bool estado { get; set; }
        public Imagen imagen_plato { get; set; }
    }
}
