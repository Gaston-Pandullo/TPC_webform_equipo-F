using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_webforms_equipo_F
{
    public partial class Menu : System.Web.UI.Page
    {
        public List<Plato> listaPlatos = new List<Plato>();
        protected void Page_Load(object sender, EventArgs e)
        {
            PlatosService platos = new PlatosService();
            listaPlatos = platos.getAll();
            MostrarPlatos(listaPlatos);
        }

        private void MostrarPlatos(List<Plato> listaPlatos)
        {
            rptPlatos.DataSource = listaPlatos;
            rptPlatos.DataBind();
        }
    }
}