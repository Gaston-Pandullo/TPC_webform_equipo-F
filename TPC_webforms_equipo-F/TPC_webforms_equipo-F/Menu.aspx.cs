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
        public List<Bebidas> listaBebidas = new List<Bebidas>();
        protected void Page_Load(object sender, EventArgs e)
        {
            PlatosService platos = new PlatosService();
            BebidasService bebidas = new BebidasService();

            listaPlatos = platos.getAll();
            listaBebidas = bebidas.getAll();

            MostrarItems(listaPlatos,listaBebidas);
        }

        private void MostrarItems(List<Plato> listaPlatos, List<Bebidas> listaBebidas)
        {
            rptPlatos.DataSource = listaPlatos;
            rptBebidas.DataSource = listaBebidas;
            rptBebidas.DataBind();
            rptPlatos.DataBind();
        }
    }
}