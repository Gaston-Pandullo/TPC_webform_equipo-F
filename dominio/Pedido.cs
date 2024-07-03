using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public Pedido()
        {
            comandas = new List<Comanda>();
            fechaPedido = DateTime.Now;
        }
        public int idPedido { get; set; }
        public int idMesa { get; set; }
        public DateTime fechaPedido { get; set; }
        public decimal total { get; set; }
        public List<Comanda> comandas { get; set; }
    }
}
