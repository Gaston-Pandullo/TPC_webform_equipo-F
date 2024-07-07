using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_webforms_equipo_F
{
    public partial class Menu : System.Web.UI.Page
    {
        public List<ItemMenu> itemsMenu = new List<ItemMenu> ();
        protected int mesaId;
        protected int pedidoId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ItemMenuService itemMenuService = new ItemMenuService();
                if (Request.QueryString["idMesa"] != null)
                {
                    mesaId = Convert.ToInt32(Request.QueryString["idMesa"]);
                }

                if (Request.QueryString["idPedido"] != null)
                {
                    pedidoId = Convert.ToInt32(Request.QueryString["idPedido"]);
                }

                itemsMenu = itemMenuService.getAll();

                MostrarItems(itemsMenu);
            }
        }

        private void MostrarItems(List<ItemMenu> items)
        {
            var platos = items.Where(i => i.categoria == 'C').ToList();
            var bebidas = items.Where(i => i.categoria == 'B').ToList();
            var postres = items.Where(i => i.categoria == 'P').ToList();

            rptPlatos.DataSource = platos;
            rptPlatos.DataBind();

            rptBebidas.DataSource = bebidas;
            rptBebidas.DataBind();

            rptPostres.DataSource = postres;
            rptPostres.DataBind();
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

        protected void rptPostres_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RestarCantidad")
            {
                ActualizarCantidad(e, rptPostres, -1);
            }
            else if (e.CommandName == "SumarCantidad")
            {
                ActualizarCantidad(e, rptPostres, 1);
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ItemMenuService itemMenuService = new ItemMenuService();
            PedidoService pedidoService = new PedidoService();

            Comanda comanda = new Comanda();

            // Recuperar pedidoId de la sesión
            if (Session["idPedido"] != null)
            {
                pedidoId = (int)Session["idPedido"];
            }

            Pedido pedido = pedidoService.GetPedidoById(pedidoId);//esto devuelve idpedido 0

            //if (pedido == null)
            //{
            //    return;
            //}

            // Se recorre el repeater de los platos
            foreach (RepeaterItem itemMenu in rptPlatos.Items)
            {
                TextBox txtCantidad = (TextBox)itemMenu.FindControl("txtCantidad");
                Label lblNombre = (Label)itemMenu.FindControl("lblNombre");
            
                if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    decimal precio = itemMenuService.getPrice(lblNombre.Text);

                    ItemMenu item = new ItemMenu
                    {
                        cantidad = cantidad,
                        nombre = lblNombre.Text,
                        precio = precio,
                    };
                    item.id = itemMenuService.getIdbyName(lblNombre.Text);
                    comanda.items.Add(item);
                }
            }

            //Se recorre el repeater de las bebidas
            foreach (RepeaterItem itemMenu in rptBebidas.Items)
            {
                TextBox txtCantidad = (TextBox)itemMenu.FindControl("txtCantidad");
                Label lblNombre = (Label)itemMenu.FindControl("lblNombre");

                if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    decimal precio = itemMenuService.getPrice(lblNombre.Text);

                    ItemMenu item = new ItemMenu
                    {
                        cantidad = cantidad,
                        nombre = lblNombre.Text,
                        precio = precio,
                    };
                    item.id = itemMenuService.getIdbyName(lblNombre.Text);
                    comanda.items.Add(item);
                }
            }

            //Se recorre el repeater de las bebidas
            foreach (RepeaterItem itemMenu in rptPostres.Items)
            {
                TextBox txtCantidad = (TextBox)itemMenu.FindControl("txtCantidad");
                Label lblNombre = (Label)itemMenu.FindControl("lblNombre");
                
                if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    decimal precio = itemMenuService.getPrice(lblNombre.Text);

                    ItemMenu item = new ItemMenu
                    {
                        cantidad = cantidad,
                        nombre = lblNombre.Text,
                        precio = precio,
                    };

                    item.id = itemMenuService.getIdbyName(lblNombre.Text);
                    comanda.items.Add(item);
                }
            }


            //descontar stock de la tabla itemMenu
            foreach (var item in comanda.items)
            {
                itemMenuService.DescontarStock(item.id, item.cantidad);
            }


            comanda.precioTotal = comanda.items.Sum(item => item.precio * item.cantidad);

            pedido.comandas.Add(comanda);

            pedidoService.ActualizarPedido(pedido);

            Response.Redirect("Default.aspx");
        }
    }
}
