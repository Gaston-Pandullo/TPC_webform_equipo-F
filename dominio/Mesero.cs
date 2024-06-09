using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Mesero : Usuario
    {
        public Mesero() {}
        public int id_mesero {  get; set; }
        public int id_usuario { get; set; }
        private int cantMesasActuales { get; set; }
        private int total_mesas_atendidas { get; set; }
        //public List<Mesa> mesas_asignadas { get; set; }
        //public List<Comanda> lista_comandas { get; set; }
    }
}
