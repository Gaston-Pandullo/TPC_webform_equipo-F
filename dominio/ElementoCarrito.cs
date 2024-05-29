using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class ElementoCarrito
    {
        public Articulo IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public Venta IdVenta { get; set; }
    }
}
