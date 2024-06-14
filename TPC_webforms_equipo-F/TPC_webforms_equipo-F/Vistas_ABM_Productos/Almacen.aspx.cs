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
            if (e.CommandName == "Incrementar" || e.CommandName == "Decrementar")
            {
                var args = e.CommandArgument.ToString().Split(',');
                if (args.Length == 3)
                {
                    int id = Convert.ToInt32(args[0]);
                    string categoria = args[1];
                    int stock = Convert.ToInt32(args[2]);

                    bool incrementar = e.CommandName == "Incrementar";
                    ActualizarStock(id, categoria, stock, incrementar);
                }
            }
        }

        private void ActualizarStock(int id, string categoria, int stock, bool incrementar)
        {
            try
            {
                if (stock <= 0 && !incrementar)
                {
                    return;
                }

                if (categoria == "B")
                {
                    BebidasService bebidasService = new BebidasService();
                    bebidasService.updateStock(id, stock, incrementar);
                }
                else if (categoria == "C")
                {
                    PlatosService platosService = new PlatosService();
                    platosService.updateStock(id, stock, incrementar);
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
    }
}