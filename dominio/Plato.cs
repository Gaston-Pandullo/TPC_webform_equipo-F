using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Plato : ItemMenu
    {
        public Plato() {}
        public List<Ingrediente> lista_ingredientes { get; set; }
        public Imagen imagen_plato { get; set; }
    }
}
