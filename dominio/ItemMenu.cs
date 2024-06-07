using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ItemMenu
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public float precio { get; set; }
        public int stock { get; set; }
        public bool estado { get; set; }
        public char categoria {  get; set; }

        public ItemMenu()
        {
            nombre = "";
            descripcion = "";
            precio = 0;
            estado = true;
            cantidad = 0;
            stock = 50;
        }
    }
}
