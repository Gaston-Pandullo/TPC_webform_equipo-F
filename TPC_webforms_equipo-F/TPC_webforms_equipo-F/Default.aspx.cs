using dominio;
using negocio;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System;

namespace TPC_webforms_equipo_F
{
    public partial class _Default : System.Web.UI.Page
    {
        PedidoService pedidoService = new PedidoService();
        MesasService mesaService = new MesasService();

        public List<Mesa> _MesaList;

        decimal total = 0;
        int idPedidoActual = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                total = 0;
                idPedidoActual = 0;

                //int idMesa = 1;
                //Pedido pedido = pedidoService.GetPedidoByMesaId(idMesa);
                //
                //if (pedido != null)
                //{
                //    List<Comanda> comandas = pedidoService.GetComandasByPedidoId(pedido.idPedido);
                //    MostrarComandas(comandas);
                //}
            }
            InicializarMesas();
        }

        private void InicializarMesas()
        {
            MesasService mesaService = new MesasService();
            _MesaList = mesaService.getAll();

            mainTables.Controls.Clear();

            foreach (var mesa in _MesaList)
            {
                Button button = new Button
                {
                    ID = "btnTable" + mesa.id_mesa,
                    Text = mesa.id_mesa.ToString(),
                    CommandArgument = mesa.id_mesa.ToString(),
                    CssClass = "table-button " + (mesa.ocupada ? "red" : "green")
                };
                button.Click += new EventHandler(TableButton_Click);
                mainTables.Controls.Add(button);
            }
        }

        protected void TableButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string tableId = button.CommandArgument;
            lblNumeroMesa.Text = tableId;

            MesasService mesaService = new MesasService();
            bool mesaOcupada = mesaService.MesaEstaOcupada(Convert.ToInt32(tableId));
            
            if (mesaOcupada)
            {
                btnAbrirMesa.Visible = false;
                OrderDetailsPanel.Visible = true;

                MostrarComandas(pedidoService.GetComandasByPedidoId(idPedidoActual));
            }
            else
            {
                btnAbrirMesa.Visible = true;
                OrderDetailsPanel.Visible = false;
            }
        }

        protected void btnAgregarPlato_Click(object sender, EventArgs e)
        {
            int mesaId = Convert.ToInt32(lblNumeroMesa.Text);
            MesasService negocio = new MesasService();
            int idPedido = negocio.buscarUltimoIdpedidoxMesa(mesaId);

            //Guardo en sesión
            Session["mesaId"] = mesaId;
            Session["idPedido"] = idPedido;
            Response.Redirect($"Menu.aspx?idMesa={mesaId}&idPedido={idPedido}", false);
        }

        private void CalcularPrecioTotal(List<ItemMenu> items)
        {
            decimal precioTotal = 0;
            foreach (ItemMenu item in items)
            {
                decimal precio = item.precio * item.cantidad;
                precioTotal += precio;
            }
            lblPrecioTotal.Text = precioTotal.ToString();
        }

        protected void btnAbrirMesa_Click(object sender, EventArgs e)
        {
            int idMesa = Convert.ToInt32(lblNumeroMesa.Text);
            mesaService.OcuparMesa(idMesa);
            Pedido pedido = mesaService.CrearPedido(idMesa);
            idPedidoActual = pedido.idPedido;

            Button button = mainTables.FindControl("btnTable" + idMesa) as Button;
            if (button != null)
            {
                button.CssClass = "table-button red";
            }

            DateTime fechaActual = DateTime.Now;
            lblFechaPedido.Text = fechaActual.ToString("dd/MM/yyyy");

            OrderDetailsPanel.Visible = true;
            btnAbrirMesa.Visible = false;
        }

        private void MostrarComandas(List<Comanda> comandas)
        {
            rptComandas.DataSource = comandas;
            rptComandas.DataBind();
        }

        protected void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            int mesaId = Convert.ToInt32(lblNumeroMesa.Text);
            MesasService mesaService = new MesasService();
            idPedidoActual = mesaService.buscarUltimoIdpedidoxMesa(mesaId);
            total = mesaService.calcularTotal(idPedidoActual);
            mesaService.ActualizarPrecioEnPedido(total, idPedidoActual);
            mesaService.MarcarMesaComoNoOcupada(mesaId);

            Button button = mainTables.FindControl("btnTable" + mesaId) as Button;
            if (button != null)
            {
                button.CssClass = "table-button green";
            }

            lblFechaPedido.Text = "";
            lblNumeroMesa.Text = "";
            idPedidoActual = 0;

            OrderDetailsPanel.Visible = false;
            btnAbrirMesa.Visible = false;
        }

    }
}
