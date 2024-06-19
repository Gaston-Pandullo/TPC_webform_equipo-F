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
        List<int> idPlatosSeleccionados = new List<int>(); //esta lista la vamos a usar para guardar los idPlato
        float total = 0;
        int idPedidoActual = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarPlatos();
                InicializarMesas();
                total = 0;
                idPedidoActual = 0;
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
            int idComanda = negocio.ObtenerUltimoIDComanda(mesaId);

            // Obtener el idPedidoActual para la mesa específica
            int idPedidoActual = negocio.buscarUltimoIdpedidoxMesa(mesaId);

            // Obtener los nombres de los platos asociados al idPedidoActual desde la base de datos
            List<string> nombresPlatos = negocio.ObtenerNombresPlatosPorPedido(idPedidoActual);

            // Mostrar los nombres de los platos en lblPlatos
            lblPlatos.Text = string.Join(", ", nombresPlatos);

            // Cargar la fecha del sistema
            DateTime fechaActual = DateTime.Now;
            string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");
            lblFechaPedido.Text = fechaFormateada;

            // Faltaría cargar el tema del precio.
        }


        protected void btnAgregarPlato_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPlatos.SelectedValue != "0")
                {
                    int idPlatoSeleccionado = Convert.ToInt32(ddlPlatos.SelectedValue);
                    idPlatosSeleccionados.Add(idPlatoSeleccionado);


                    // Aca se actualiza el Label para mostrar los platos 
                    if (string.IsNullOrEmpty(lblPlatos.Text))
                    {
                        lblPlatos.Text = ddlPlatos.SelectedItem.Text;
                    }
                    else
                    {
                        lblPlatos.Text += ", " + ddlPlatos.SelectedItem.Text;
                    }

                    // Guardamos la comanda cada vez que agregamos un plato
                    MesasService negocio = new MesasService();
                    //buscar el idpedido
                    idPedidoActual = negocio.buscarUltimoIdpedidoxMesa(Convert.ToInt32(lblNumeroMesa.Text));
                    negocio.GuardarDetalleComanda(Convert.ToInt32(lblNumeroMesa.Text), idPlatoSeleccionado, idPedidoActual);

                    // Calcular y mostrar el precio total
                    float acuSuma = CalcularPrecioTotal();
                    total += acuSuma;


                    // Actualizar el lblPrecioTotal
                    lblPrecioTotal.Text = total.ToString("0.00");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private float CalcularPrecioTotal()
        {
            float precioTotal = 0;

            foreach (int idPlato in idPlatosSeleccionados)
            {

                MesasService negocio = new MesasService();
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
            lblPlatos.Text = "";

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
            lblPlatos.Text = "";
            idPedidoActual = 0;

            OrderDetailsPanel.Visible = false;
            btnAbrirMesa.Visible = false;
        }

        

    }
}