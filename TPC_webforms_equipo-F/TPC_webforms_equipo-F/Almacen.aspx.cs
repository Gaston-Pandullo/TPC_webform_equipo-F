using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        private void CargarProductos()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT id_Bebida AS Id, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio, stock AS Stock, categoria FROM BEBIDAS " +
                    "UNION ALL " +
                    "SELECT id_Plato AS Id, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio, stock AS Stock, categoria FROM PLATOS");
                datos.ejecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);

                gvProductos.DataSource = dt;
                gvProductos.DataBind();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Incrementar" || e.CommandName == "Decrementar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvProductos.Rows[index];
                int id = Convert.ToInt32(row.Cells[0].Text);
                string nombre = row.Cells[1].Text;
                int stock = Convert.ToInt32(row.Cells[4].Text);
                string categoria = row.Cells[5].Text;
                bool incrementar = e.CommandName == "Incrementar";

                ActualizarStock(id, nombre, categoria, stock, incrementar);
            }
        }

        private void ActualizarStock(int id, string nombre, string categoria, int stock, bool incrementar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (stock <= 0 && !incrementar)
                {
                    return;
                }

                datos.limpiarParametros();
                if (categoria == "B")
                {
                    datos.setearConsulta("UPDATE BEBIDAS SET stock = @stock WHERE id_Bebida = @id");

                }
                else if (categoria == "C")
                {
                    datos.setearConsulta("UPDATE PLATOS SET stock = @stock WHERE id_Plato = @id");
                }
                else
                {
                    datos.setearConsulta("UPDATE BEBIDAS SET stock = @stock WHERE id_Bebida = @id");
                }

                datos.setearParametro("@id", id);
                datos.setearParametro("@stock", incrementar ? stock + 1 : stock - 1);
                datos.ejecutarAccion();

                CargarProductos();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}