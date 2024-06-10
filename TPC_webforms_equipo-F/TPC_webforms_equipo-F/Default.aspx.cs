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
            OrderDetailsPanel.Visible = true;
        }

        //falta ver que el btn continuar funciona igual que si tocas la mesa
        //hay que ver que cuando se vaya agregando la comida se haga un post en la BD y luego
        //cada vez que ponga continuar me traiga esos datos guardados.
        //entonces cuando le de a cerrar mesa me tiene que hacer el post final en el de pedidos
        //y volver a poner la mesa en libre
    }
}