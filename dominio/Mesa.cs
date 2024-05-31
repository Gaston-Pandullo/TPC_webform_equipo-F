using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Mesa
    {
        public Mesa() 
        {
            ocupada = true;
        }
        public Mesa( Mesero mesero, Comanda comanda) 
        {
            mesero_asignado = mesero;
            this.comanda = comanda;
            ocupada = true;
        }

        public int id_mesa { get; set; }
        public Mesero mesero_asignado { get; set; }
        public Comanda comanda { get; set; }
        public bool ocupada { get; set; }
    }
}
