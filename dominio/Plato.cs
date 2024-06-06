using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Plato
    {
        public Plato() 
        {
            nombre = "";
            descripcion = "";
            precio = 0;
            estado = true;
            cantidad = 0;
        }
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public List<Ingrediente> lista_ingredientes { get; set; }
        public float precio { get; set; }
        public bool estado { get; set; }
        public int cantidad { get; set; }
        public int stock {  get; set; }
        public Imagen imagen_plato { get; set; }
    }
}
