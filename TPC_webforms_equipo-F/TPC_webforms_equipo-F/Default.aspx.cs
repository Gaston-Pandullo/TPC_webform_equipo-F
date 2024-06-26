using dominio;
using negocio;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System;

namespace TPC_webforms_equipo_F
{
    public partial class _Default : System.Web.UI.Page
    {
        public List<Mesa> _MesaList;
        private List<int> idPlatosSeleccionados
        {
            get
            {
                if (ViewState["idPlatosSeleccionados"] == null)
                {
                    ViewState["idPlatosSeleccionados"] = new List<int>();
                }
                return (List<int>)ViewState["idPlatosSeleccionados"];
            }
            set
            {
                ViewState["idPlatosSeleccionados"] = value;
            }
        }

        decimal total;
        int idPedidoActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPlatos();
                total = 0;
                idPedidoActual = 0;
                idPlatosSeleccionados = new List<int>();
            }
            InicializarMesas(); // Mueve InicializarMesas fuera de !IsPostBack
        }

        private void CargarPlatos()
        {
            PlatosService negocio = new PlatosService();
            List<Plato> plato = negocio.getAll();
        }

        private void InicializarMesas()
        {
            MesasService mesaService = new MesasService();
            _MesaList = mesaService.getAll();

            mainTables.Controls.Clear(); // Limpia los controles para evitar duplicados

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
                //CargarDetallesComanda(Convert.ToInt32(tableId));
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
            Response.Redirect($"Menu.aspx?idMesa={mesaId}", false);
        }

        private decimal CalcularPrecioTotal()
        {
            decimal precioTotal = 0;
            MesasService negocio = new MesasService();
            foreach (int idPlato in idPlatosSeleccionados)
            {
                Plato plato = negocio.ObtenerPlatoPorID(idPlato);
                precioTotal += plato.precio;
            }
            return precioTotal;
        }

        protected void btnAbrirMesa_Click(object sender, EventArgs e)
        {
            int mesaId = Convert.ToInt32(lblNumeroMesa.Text);
            MesasService mesaService = new MesasService();
            mesaService.MarcarMesaComoOcupada(mesaId);
            idPedidoActual = CrearIDPedido();
            mesaService.PedidoCompleto(mesaId);

            Button button = mainTables.FindControl("btnTable" + mesaId) as Button;
            if (button != null)
            {
                button.CssClass = "table-button red";
            }

            DateTime fechaActual = DateTime.Now;
            lblFechaPedido.Text = fechaActual.ToString("dd/MM/yyyy");

            OrderDetailsPanel.Visible = true;
            btnAbrirMesa.Visible = false;
        }

        protected int CrearIDPedido()
        {
            int idPedido;
            MesasService negocio = new MesasService();
            idPedido = negocio.buscarUltimoIdpedido();
            return idPedido + 1;
        }

        protected void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            int mesaId = Convert.ToInt32(lblNumeroMesa.Text);
            MesasService mesaService = new MesasService();
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
