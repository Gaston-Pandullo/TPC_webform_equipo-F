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
                CargarMeseros();
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

        private void CargarMeseros()
        {
            MeserosService negocio = new MeserosService();
            List<Mesero> meseros = negocio.getAll();


            ddlMeseros.DataSource = meseros;
            ddlMeseros.DataTextField = "name";
            ddlMeseros.DataValueField = "id_mesero";

            ddlMeseros.DataBind();

            ddlMeseros.Items.Insert(0, new ListItem("-- Seleccione un mesero --", "0"));
        }

        protected void TableButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;

            string tableId = button.CommandArgument;

            string fechaPedido = DateTime.Now.ToString("dd/MM/yyyy");
            lblFechaPedido.Text = fechaPedido;
            lblNumeroMesa.Text = tableId.ToString();
            lblPlatos.Text = "";

            OrderDetailsPanel.Visible = true;
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

        protected void btnAgregarMesero_Click(object sender, EventArgs e)
        {
            string meseroSeleccionado = ddlMeseros.SelectedItem.Text;
            lblMesero.Text = meseroSeleccionado;
        }

        protected void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            OrderDetailsPanel.Visible = false;
            // Reseteamos los campos
            lblFechaPedido.Text = "";
            lblNumeroMesa.Text = "";
            lblMesero.Text = "";
            lblPlatos.Text = "";
            lblPrecioTotal.Text = "";
        }
    }
}