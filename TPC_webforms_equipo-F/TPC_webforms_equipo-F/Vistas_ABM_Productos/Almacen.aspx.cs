using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using System.Diagnostics;

namespace TPC_webforms_equipo_F
{
    public partial class Almacen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        protected void gvProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Guardar")
            {
                string[] args = e.CommandArgument.ToString().Split(',');
                int id = Convert.ToInt32(args[0]);
                string categoria = args[1];
                TextBox txtStock = (TextBox)e.Item.FindControl("txtStock");
                int stock = Convert.ToInt32(txtStock.Text);

                ActualizarStock(id, categoria, stock);

                ScriptManager.RegisterStartupScript(this, GetType(), "showNotificationModal", "showNotificationModal();", true);
            }
        }

        private void ActualizarStock(int id, string categoria, int stock)
        {
            ItemMenuService itemMenuService = new ItemMenuService();
            try
            {
                if (stock <= 0)
                {
                    return;
                }

                if(categoria == "C" || categoria == "P" || categoria == "B")
                {
                    itemMenuService.updateStock(id, stock);
                }

                CargarProductos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarProductos()
        {
            try
            {
                ItemMenuService itemMenuService = new ItemMenuService();
                var itemMenus = itemMenuService.getAll();

                gvProductos.DataSource = itemMenus;
                gvProductos.DataBind();

                Debug.WriteLine("Productos cargados.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            string textoFiltro = txtFiltroNombre.Text;
            if(textoFiltro != "")
            {
                try
                {
                    ItemMenuService itemMenuService = new ItemMenuService();
                    List<ItemMenu> itemMenus = itemMenuService.getItems_by_filtro(textoFiltro);

                    ddlFiltroCategoria.SelectedIndex=0;
                    gvProductos.DataSource = itemMenus;
                    gvProductos.DataBind();

                    Debug.WriteLine("Productos cargados.");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ddlFiltroCategoria.SelectedIndex = 0;
                CargarProductos();
            }
            
        }

        protected void ddlFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            char categoriaFiltro = Convert.ToChar(ddlFiltroCategoria.SelectedValue);
            if (ddlFiltroCategoria.SelectedIndex!=0)
            {
                try
                {
                    ItemMenuService itemMenuService = new ItemMenuService();
                    List<ItemMenu> itemMenus = itemMenuService.getItems_by_filtroCategoria(categoriaFiltro);

                    txtFiltroNombre.Text = "";
                    gvProductos.DataSource = itemMenus;
                    gvProductos.DataBind();

                    Debug.WriteLine("Productos cargados.");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if(ddlFiltroCategoria.SelectedIndex == 0)
            {
                txtFiltroNombre.Text = "";
                CargarProductos();
            }
        }
    }
}