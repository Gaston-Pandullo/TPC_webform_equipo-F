using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Venta
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        [DisplayName("Fecha de Compra")]
        public DateTime FechaCompra { get; set; }
        [DisplayName("Forma de Pago")]

        //Ver si agregamos estas dos clases extra para la informacion de los clientes y los pagos
        //public FormaDePago FOP { get; set; }
        //public Cliente IdCliente { get; set; }
        public bool Despachado { get; set; } //0: sin despachar 1:despachado
    }
}
