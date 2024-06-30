using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal precio_unitario { get; set; }
    }
}
