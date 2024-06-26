using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_webforms_equipo_F
{
    public partial class Menu : System.Web.UI.Page
    {
        public List<Plato> listaPlatos = new List<Plato>();
        public List<Bebidas> listaBebidas = new List<Bebidas>();
        protected int mesaId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PlatosService platos = new PlatosService();
                BebidasService bebidas = new BebidasService();

                if (Request.QueryString["idMesa"] != null)
                {
                    mesaId = Convert.ToInt32(Request.QueryString["idMesa"]);
                }

                listaPlatos = platos.getAll();
                listaBebidas = bebidas.getAll();

                MostrarItems(listaPlatos, listaBebidas);
            }
        }

        private void MostrarItems(List<Plato> listaPlatos, List<Bebidas> listaBebidas)
        {
            rptPlatos.DataSource = listaPlatos;
            rptBebidas.DataSource = listaBebidas;
            rptBebidas.DataBind();
            rptPlatos.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            var pedidos = new List<Pedido>();

            foreach (RepeaterItem item in rptPlatos.Items)
            {
                var cantidadInput = (HtmlInputControl)item.FindControl("txtCantidad");
                int cantidad = int.Parse(cantidadInput.Value);
                if (cantidad > 0)
                {
                    var nombreLabel = (Label)item.FindControl("nombreLabel");
                    string nombre = nombreLabel.Text;

                    pedidos.Add(new Pedido { Nombre = nombre, Cantidad = cantidad });
                }
            }

            foreach (RepeaterItem item in rptBebidas.Items)
            {
                var cantidadInput = (HtmlInputControl)item.FindControl("txtCantidad");
                int cantidad = int.Parse(cantidadInput.Value);
                if (cantidad > 0)
                {
                    var nombreLabel = (Label)item.FindControl("nombreLabel");
                    string nombre = nombreLabel.Text;

                    pedidos.Add(new Pedido { Nombre = nombre, Cantidad = cantidad });
                }
            }

            //PedidoService pedidoService = new PedidoService();
            //pedidoService.GuardarPedidos(pedidos);

            //Response.Redirect("Success.aspx");
        }

        //private void CargarDetallesComanda(int mesaId)
        //{
        //    MesasService negocio = new MesasService();
        //    int idPedidoActual = negocio.buscarUltimoIdpedidoxMesa(mesaId);
        //
        //    if (idPedidoActual > 0)
        //    {
        //        List<Plato> platosPedidos = negocio.ObtenerNombresPlatosPorPedido(idPedidoActual);
        //        gvPlatosPedidos.DataSource = platosPedidos;
        //        gvPlatosPedidos.DataBind();
        //    }
        //
        //    DateTime fechaActual = DateTime.Now;
        //    lblFechaPedido.Text = fechaActual.ToString("dd/MM/yyyy");
        //}
    }

    public class Pedido
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
    }
}
