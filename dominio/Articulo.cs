using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dominio
{
    public class Articulo
    {

        // Propiedades del articulo
        [DisplayName("ID")]
        public int ID { get; set; }
        
        [DisplayName("Codigo")]
        public string CODIGO { get; set; }
        
        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }
        
        [DisplayName("Descripción")]
        public string DESCRIPCION { get; set; }
        
        [DisplayName("Marca")]
        public Marca MARCA { get; set; }
        
        [DisplayName("Categoria")]
        public Categoria CATEGORIA { get; set; }
        
        [DisplayName("Imagen")]
        public List<Imagen> IMAGEN { get; set; }

        [DisplayName("Precio")]
        public decimal PRECIO { get; set; }

        private int CANTIDAD = 0;
        public int Cantidad
        {
            get { return CANTIDAD; }
            set { CANTIDAD = value; }
        } 

        public int CantidadImagenes { get; set; }
    }
}
