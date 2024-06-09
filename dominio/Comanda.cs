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
            precio = 0;
        }
        public int id { get; set; }
        public Mesa mesa_asignada { get; set; }
        public Mesero mesero_asignado { get; set; }
        public List<Plato> pedido { get; set; }
        public int precio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
