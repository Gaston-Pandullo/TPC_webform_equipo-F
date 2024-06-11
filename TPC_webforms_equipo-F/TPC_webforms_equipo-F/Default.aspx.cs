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
    public partial class _Default : System.Web.UI.Page
    {
        List<int> idPlatosSeleccionados = new List<int>(); //esta lista la vamos a usar para guardar los idPlato
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarPlatos();
                InicializarMesas();
            }
        }

        private void CargarPlatos()
        {
            PlatosService negocio = new PlatosService();
            List<Plato> plato = negocio.getAll();

            ddlPlatos.DataSource = plato;
            ddlPlatos.DataTextField = "nombre";
            ddlPlatos.DataValueField = "nombre";
            ddlPlatos.DataBind();

            ddlPlatos.Items.Insert(0, new ListItem("-- Seleccione un Plato --", "0"));
        }

        private void InicializarMesas()
        {
            MesasService mesaService = new MesasService();
            foreach (Control control in TableOrder.Controls)
            {
                if (control is Button button && button.CommandArgument != null)
                {
                    int mesaId = Convert.ToInt32(button.CommandArgument);
                    if (mesaService.MesaEstaOcupada(mesaId))
                    {
                        button.CssClass = "table-button red";
                    }
                    else
                    {
                        button.CssClass = "table-button green";
                    }
                }
            }
        }

        protected void TableButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string tableId = button.CommandArgument;
            lblNumeroMesa.Text = tableId;

            MesasService mesaService = new MesasService();
            if (mesaService.MesaEstaOcupada(Convert.ToInt32(tableId)))
            {
                btnAbrirMesa.Visible = false;
                btnContinuar.Visible = true;
                OrderDetailsPanel.Visible = true;
            }
            else
            {
                btnAbrirMesa.Visible = true;
                btnContinuar.Visible = false;
                OrderDetailsPanel.Visible = false;
            }
        }

        protected void btnAgregarPlato_Click(object sender, EventArgs e)
        {
            string platoSeleccionado = ddlPlatos.SelectedItem.Text;
            PlatosService negocio = new PlatosService();

            int idPlatoSeleccionado = Convert.ToInt32(ddlPlatos.SelectedValue);
            idPlatosSeleccionados.Add(idPlatoSeleccionado);

            if (string.IsNullOrEmpty(lblPlatos.Text))
            {
                lblPlatos.Text = platoSeleccionado;
            }
            else
            {
                lblPlatos.Text += ", " + platoSeleccionado;
            }
        }

        protected void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            int mesaId = Convert.ToInt32(lblNumeroMesa.Text);
            MesasService mesaService = new MesasService();
            mesaService.MarcarMesaComoNoOcupada(mesaId);

            Button button = TableOrder.FindControl("btnTable" + mesaId) as Button;
            if (button != null)
            {
                button.CssClass = "table-button green";
            }

            lblFechaPedido.Text = "";
            lblNumeroMesa.Text = "";
            lblPlatos.Text = "";

            OrderDetailsPanel.Visible = false;
            btnAbrirMesa.Visible = false;
            btnContinuar.Visible = false;

            MesasService negocio = new MesasService();
            int idComanda = negocio.ObtenerUltimoIDComanda(mesaId);

            negocio.GuardarDetallesComanda(idComanda, Convert.ToInt32(lblNumeroMesa.Text), idPlatosSeleccionados);
            //falta desarrollar el metodo para hacer el post en la Tabla de Pedidos cuando se cierra la mesa
        }

        protected void btnAbrirMesa_Click(object sender, EventArgs e)
        {
            int mesaId = Convert.ToInt32(lblNumeroMesa.Text);
            MesasService mesaService = new MesasService();
            mesaService.MarcarMesaComoOcupada(mesaId);

            Button button = TableOrder.FindControl("btnTable" + mesaId) as Button;
            if (button != null)
            {
                button.CssClass = "table-button red";
            }

            string fechaPedido = DateTime.Now.ToString("dd/MM/yyyy");
            lblFechaPedido.Text = fechaPedido;
            lblPlatos.Text = "";

            OrderDetailsPanel.Visible = true;
            btnAbrirMesa.Visible = false;
            btnContinuar.Visible = true;
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            MesasService negocio = new MesasService();
            // Generar un ID único para la comanda
            int mesaId = Convert.ToInt32(lblNumeroMesa.Text);
            int idComanda = GenerarIDComandaUnico(mesaId);

            negocio.GuardarDetallesComanda(idComanda, Convert.ToInt32(lblNumeroMesa.Text), idPlatosSeleccionados);

            // Limpiar la lista de idPlatosSeleccionados para el próximo pedido
            idPlatosSeleccionados.Clear();

            // Mostrar los detalles del pedido
            OrderDetailsPanel.Visible = true;
        }

        private int GenerarIDComandaUnico(int mesaId)
        {
            MesasService negocio = new MesasService();
            int idComanda = negocio.ObtenerUltimoIDComanda(mesaId) + 1;
            return idComanda;
        }

        //ver que cuando ponga continuar me traiga los datos guardados de esa mesa
        //Revisar que da error cuando agrego un plato

    }
}