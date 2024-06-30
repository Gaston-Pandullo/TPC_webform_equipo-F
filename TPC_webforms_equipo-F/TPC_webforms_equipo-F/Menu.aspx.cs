using System;
using System.Collections.Generic;
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
                PlatosService platosService = new PlatosService();
                BebidasService bebidasService = new BebidasService();

                if (Request.QueryString["idMesa"] != null)
                {
                    mesaId = Convert.ToInt32(Request.QueryString["idMesa"]);
                }

                listaPlatos = platosService.getAll();
                listaBebidas = bebidasService.getAll();

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
            // Se crea una lista donde se llenara con platos y bebidas
            List<Pedido> listaPedido = new List<Pedido>();

            // Se recorre el repeater de los platos
            foreach (RepeaterItem itemMenu in rptPlatos.Items)
            {
                TextBox txtCantidad = (TextBox)itemMenu.FindControl("txtCantidad");
                Label lblNombre = (Label)itemMenu.FindControl("lblNombre");

                if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    PlatosService platosService = new PlatosService();
                    decimal precio = platosService.ObtenerPrecioPorNombre(lblNombre.Text);

                    Pedido pedido = new Pedido
                    {
                        Cantidad = cantidad,
                        Nombre = lblNombre.Text,
                        precio_unitario = precio
                    };

                    listaPedido.Add(pedido);
                }
            }

            // Se recorre el repeater de las bebidas
            foreach (RepeaterItem itemMenu in rptBebidas.Items)
            {
                TextBox txtCantidad = (TextBox)itemMenu.FindControl("txtCantidad");
                Label lblNombre = (Label)itemMenu.FindControl("lblNombre");

                if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    BebidasService bebidasService = new BebidasService();
                    decimal precio = bebidasService.ObtenerPrecioPorNombre(lblNombre.Text);

                    Pedido pedido = new Pedido
                    {
                        Cantidad = cantidad,
                        Nombre = lblNombre.Text,
                        precio_unitario = precio
                    };

                    listaPedido.Add(pedido);
                }
            }

            // Guardar listaPedido en la sesión
            Session["Pedido"] = listaPedido;

            // Redirigir a Default.aspx
            Response.Redirect("Default.aspx");
        }

        protected void rptPlatos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RestarCantidad")
            {
                ActualizarCantidad(e, rptPlatos, -1);
            }
            else if (e.CommandName == "SumarCantidad")
            {
                ActualizarCantidad(e, rptPlatos, 1);
            }
        }

        protected void rptBebidas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RestarCantidad")
            {
                ActualizarCantidad(e, rptBebidas, -1);
            }
            else if (e.CommandName == "SumarCantidad")
            {
                ActualizarCantidad(e, rptBebidas, 1);
            }
        }

        private void ActualizarCantidad(RepeaterCommandEventArgs e, Repeater repeater, int delta)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            TextBox txtCantidad = (TextBox)repeater.Items[index].FindControl("txtCantidad");

            if (txtCantidad != null)
            {
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                cantidad += delta;
                if (cantidad < 0) cantidad = 0;
                txtCantidad.Text = cantidad.ToString();
            }
        }
    }
}
