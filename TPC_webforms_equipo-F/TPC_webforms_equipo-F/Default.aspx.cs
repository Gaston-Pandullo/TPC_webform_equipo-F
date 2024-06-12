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
        float precioTotal = 0;
        int idPedidoActual = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarPlatos();
                InicializarMesas();
                precioTotal = 0;
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

                // Cada vez que se abra una mesa Ocupada me va a traer los datos guardados
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
            List<int> platos = negocio.ObtenerPlatosPorComanda(idComanda); //Devuelve el id_Plato

            lblPlatos.Text = "";
            foreach (int idPlato in platos)
            {
                Plato plato = negocio.ObtenerPlatoPorID(idPlato); // Método para obtener un plato por su ID
                if (string.IsNullOrEmpty(lblPlatos.Text))
                {
                    lblPlatos.Text = plato.nombre;
                }
                else
                {
                    lblPlatos.Text += ", " + plato.nombre;
                }
            }
            //cargamos fecha del sistema
            DateTime fechaActual = DateTime.Now;
            string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");
            lblFechaPedido.Text = fechaFormateada;

            //falta ver tema precio
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
                    negocio.GuardarDetalleComanda(Convert.ToInt32(lblNumeroMesa.Text), idPlatoSeleccionado, idPedidoActual);

                    // Calcular y mostrar el precio total
                    float acuSuma = CalcularPrecioTotal();
                    precioTotal += acuSuma;
                    

                    // Actualizar el lblPrecioTotal
                    lblPrecioTotal.Text = precioTotal.ToString("0.00");
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
            mesaService.PedidoCompleto(idPedidoActual, precioTotal);
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