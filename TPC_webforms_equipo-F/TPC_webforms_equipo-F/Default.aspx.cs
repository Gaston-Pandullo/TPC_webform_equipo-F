using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_webforms_equipo_F
{
    public partial class _Default : System.Web.UI.Page
    {
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
                InicializarMesas();
                total = 0;
                idPedidoActual = 0;

                idPlatosSeleccionados = new List<int>();
            }
        }

        private void CargarPlatos()
        {
            PlatosService negocio = new PlatosService();
            List<Plato> plato = negocio.getAll();

            ddlPlatos.DataSource = plato;
            ddlPlatos.DataTextField = "nombre";
            ddlPlatos.DataValueField = "id";
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
                OrderDetailsPanel.Visible = true;

                // Cada vez que se abra una mesa ocupada, cargar los detalles de la comanda
                CargarDetallesComanda(Convert.ToInt32(tableId));
            }
            else
            {
                btnAbrirMesa.Visible = true;
                OrderDetailsPanel.Visible = false;
            }
        }

        private void CargarDetallesComanda(int mesaId)
        {
            MesasService negocio = new MesasService();
            int idPedidoActual = negocio.buscarUltimoIdpedidoxMesa(mesaId);

            if (idPedidoActual > 0)
            {
                // Obtener los nombres de los platos asociados al idPedidoActual desde la base de datos
                List<Plato> platosPedidos = negocio.ObtenerNombresPlatosPorPedido(idPedidoActual);

                // Mostrar los nombres de los platos en lblPlatos
                gvPlatosPedidos.DataSource = platosPedidos;
                gvPlatosPedidos.DataBind();
            }

            // Cargar la fecha del sistema
            DateTime fechaActual = DateTime.Now;
            string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");
            lblFechaPedido.Text = fechaFormateada;
        }


        protected void btnAgregarPlato_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPlatos.SelectedValue != "0")
                {
                    int idPlatoSeleccionado = Convert.ToInt32(ddlPlatos.SelectedValue);

                    // Obtener la lista actual de ViewState
                    List<int> listaPlatosSeleccionados = idPlatosSeleccionados;

                    // Agregar el nuevo plato a la lista
                    listaPlatosSeleccionados.Add(idPlatoSeleccionado);

                    // Guardar la lista actualizada en ViewState
                    idPlatosSeleccionados = listaPlatosSeleccionados;

                    // Guardar la comanda cada vez que se agrega un plato
                    MesasService negocio = new MesasService();
                    int idMesa = Convert.ToInt32(lblNumeroMesa.Text);
                    int idPedidoActual = negocio.buscarUltimoIdpedidoxMesa(idMesa);

                    if (idPedidoActual > 0)
                    {
                        negocio.GuardarDetalleComanda(idMesa, idPlatoSeleccionado, idPedidoActual);

                        // Recargar los detalles de la comanda
                        CargarDetallesComanda(idMesa);

                        // Calcular y mostrar el precio total
                        total = CalcularPrecioTotal();
                        lblPrecioTotal.Text = total.ToString("0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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


            Button button = TableOrder.FindControl("btnTable" + mesaId) as Button;
            if (button != null)
            {
                button.CssClass = "table-button red";
            }

            // Obtener la fecha actual del sistema
            DateTime fechaActual = DateTime.Now;
            // Formatear la fecha como "dd/MM/yyyy"
            string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");

            lblFechaPedido.Text = fechaFormateada;

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
            mesaService.ActualizarPrecioEnPedido(total,idPedidoActual);
            mesaService.MarcarMesaComoNoOcupada(mesaId);

            Button button = TableOrder.FindControl("btnTable" + mesaId) as Button;
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