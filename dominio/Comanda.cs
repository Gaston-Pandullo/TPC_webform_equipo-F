using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Comanda
    {
        public Comanda()
        {
            items = new List<ItemMenu>();
            Fecha = DateTime.Now;
        }
        public int id { get; set; }
        public int idPedido { get; set; }
        public List<ItemMenu> items { get; set; }
        public decimal precioTotal { get; set; }
        public DateTime Fecha { get; set; }
    }
}
