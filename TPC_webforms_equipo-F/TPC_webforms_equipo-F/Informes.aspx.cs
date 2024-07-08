using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_webforms_equipo_F
{
    public partial class Informes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblFacturacionDia.Text = 0.ToString("C2");
                CargarTablas();
            }
        }
        private void CargarTablas()
        {
            // Carga de tabla meseros
            MeserosService negocio = new MeserosService();
            List <Mesero> listaMeseros = new List <Mesero>();
            List<InformeMeseros> listaInformeMesero = new List<InformeMeseros>();

            listaMeseros = negocio.getAll();
            
            decimal totalFacturado = 0;

            foreach (Mesero mesero in listaMeseros)
            {
                InformeMeseros informeMesero = new InformeMeseros();
                informeMesero.Nombre = mesero.name;
                informeMesero.PedidosAtendidos = negocio.ObtenerPedidosAtendidos(mesero.id_mesero);
                informeMesero.TotalFacturado = negocio.ObtenerTotalFacturado(mesero.id_mesero);
                listaInformeMesero.Add(informeMesero);

                totalFacturado += informeMesero.TotalFacturado;
            }

            rptMeseros.DataSource = listaInformeMesero;
            rptMeseros.DataBind();

            // Carga de tabla de productos pedidos
            ItemMenuService negocioItem = new ItemMenuService();
            List<ItemMenu> listaProductos = negocioItem.getAll();
            List<InformeProductos> listaInformeProductos = new List<InformeProductos>();

            foreach (ItemMenu producto in listaProductos)
            {
                InformeProductos informeProducto = new InformeProductos();
                informeProducto.Nombre = producto.nombre;
                informeProducto.CantidadesVendidas = negocioItem.ObtenerCantidadesVendidas(producto.id);
                listaInformeProductos.Add(informeProducto);
            }

            rptProductos.DataSource = listaInformeProductos;
            rptProductos.DataBind();

            // Carga de la facturacion
            lblFacturacionDia.Text = totalFacturado.ToString("C2");
        }
    }
    public class InformeMeseros
    {
        public string Nombre { get; set; }
        public int PedidosAtendidos { get; set; }
        public decimal TotalFacturado { get; set; }
    }

    public class InformeProductos
    {
        public string Nombre { get; set; }
        public int CantidadesVendidas { get; set; }
        public decimal TotalFacturado { get; set; }
    }
}