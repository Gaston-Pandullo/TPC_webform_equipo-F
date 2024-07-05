using dominio;
using negocio;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System;
using System.Web.UI;

namespace TPC_webforms_equipo_F
{
    public partial class _Default : System.Web.UI.Page
    {
        PedidoService pedidoService = new PedidoService();
        MesasService mesaService = new MesasService();

        public List<Mesa> _MesaList;

        decimal total;
        int idPedidoActual;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblFechaPedido.Text = DateTime.Now.ToString("dd/MM/yyyy"); ;
            if (!IsPostBack)
            {
                total = 0;
                idPedidoActual = 0;
            }
            InicializarMesas();
        }

        private void InicializarMesas()
        {
            _MesaList = mesaService.getAll();
            mainTables.Controls.Clear();

            int index = 1;
            foreach (var mesa in _MesaList)
            {
                Button button = new Button
                {
                    
                    ID = "btnTable" + mesa.id_mesa,
                    Text = index.ToString(),
                    CommandArgument = mesa.id_mesa.ToString(),
                    CssClass = "table-button " + (mesa.ocupada ? "red" : "green")
                };
                button.Click += new EventHandler(TableButton_Click);
                mainTables.Controls.Add(button);
                index++;
            }
        }

        protected void btnRemoveMesa_Click(object sender, EventArgs e)
        {
            try
            {
                bool mesasOcupadas = mesaService.CheckMesasLibres();
                if (mesasOcupadas)
                {
                    alertaMesa.Text = "Primero hay que desocupar las mesas.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideAlert", "setTimeout(function(){ document.getElementById('alertaMesa').innerText = ''; }, 3000);", true);
                    return;
                }
                else
                {
                    mesaService.updateMesaQty(false);
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                alertaMesa.Text = "Se produjo un error al intentar eliminar una mesa. Intente de nuevo.";
                Console.WriteLine("Error en btnRemoveMesa_Click: " + ex.Message);
            }
        }

        protected void btnAddMesa_Click(object sender, EventArgs e)
        {
            try
            {
                bool mesasOcupadas = mesaService.CheckMesasLibres();
                if (mesasOcupadas)
                {
                    alertaMesa.Text = "Primero hay que desocupar las mesas.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideAlert", "setTimeout(function(){ document.getElementById('alertaMesa').innerText = ''; }, 3000);", true);
                    return;
                }
                else
                {
                    mesaService.updateMesaQty(true);
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                alertaMesa.Text = "Se produjo un error al intentar agregar una mesa. Intente de nuevo.";
                Console.WriteLine("Error en btnAddMesa_Click: " + ex.Message);
            }
        }



        protected void TableButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string tableId = button.CommandArgument;
            lblNumeroMesa.Text = tableId;

            bool mesaOcupada = mesaService.MesaEstaOcupada(Convert.ToInt32(tableId));

            if (mesaOcupada)
            {
                btnAbrirMesa.Visible = false;
                OrderDetailsPanel.Visible = true;

                int idMesa = Convert.ToInt32(tableId);
                idPedidoActual = mesaService.buscarUltimoIdpedidoxMesa(idMesa);

                // Añade esta línea para depurar
                Console.WriteLine($"Mesa ocupada. IdMesa: {idMesa}, IdPedidoActual: {idPedidoActual}");

                var comandas = pedidoService.GetComandasByPedidoId(idPedidoActual);

                // Añade esta línea para depurar
                Console.WriteLine($"Número de comandas: {comandas.Count}");

                MostrarComandas(comandas);
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
            int idPedido = mesaService.buscarUltimoIdpedidoxMesa(mesaId);

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
            lblPrecioTotal.Text = precioTotal.ToString("C"); // Formatear como moneda
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

            // Reinicio de datos al abrir las mesas
            OrderDetailsPanel.Visible = true;
            btnAbrirMesa.Visible = false;
            lblPrecioTotal.Text = "";
            //Lista vacia para que siempre aparezca el encabezado de los pedidos.
            List<Comanda> comandas = new List<Comanda>();  
            rptComandas.DataSource = comandas; 
            rptComandas.DataBind(); 
        }
        private void MostrarComandas(List<Comanda> comandas)
        {
            var itemsMenu = new List<ItemMenu>();

            foreach (var comanda in comandas)
            {
                itemsMenu.AddRange(comanda.items);
            }

            rptComandas.DataSource = itemsMenu;
            rptComandas.DataBind();

            CalcularPrecioTotal(itemsMenu);
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
