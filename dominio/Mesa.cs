using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Mesa
    {
        public Mesa() 
        {
            ocupada = true;
        }
        public Mesa( Mesero mesero, Comanda comanda) 
        {
            mesero = mesero;
            this.comanda = comanda;
            ocupada = true;
        }

        public bool activo {  get; set; }
        public int id_mesa { get; set; }
        public int id_mesero { get; set; }
        public Mesero mesero { get; set; }
        public Comanda comanda { get; set; }
        public bool ocupada { get; set; }
    }
}
