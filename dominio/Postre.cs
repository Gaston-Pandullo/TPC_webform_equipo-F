using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Postre : ItemMenu
    {
        public Postre(){}
        public List<Ingrediente> lista_ingredientes { get; set; }
        public Imagen imagen_postre { get; set; }
        public bool sinAzucar { get; set; }
    }
}
