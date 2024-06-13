using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_webforms_equipo_F.Vistas_ABM_Productos
{
    public partial class ModificarProducto : System.Web.UI.Page
    {
        PlatosService negocio = new PlatosService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            List<Plato> productos = negocio.getAll();

            ddlProducto.DataSource = productos;
            ddlProducto.DataTextField = "nombre";
            ddlProducto.DataValueField = "id";
            ddlProducto.DataBind();

            // Para que se pueda "limpiar" los textbox
            ddlProducto.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productoId = int.Parse(ddlProducto.SelectedValue);

            if (productoId > 0)
            {
                Plato producto = negocio.getAll().Find(p => p.id == productoId);
                if (producto != null)
                {
                    txtNombre.Text = producto.nombre;
                    txtDescripcion.Text = producto.descripcion;
                    txtPrecio.Text = producto.precio.ToString();
                    txtStock.Text = producto.stock.ToString();
                }
            }
            else
            {
                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int productoId = int.Parse(ddlProducto.SelectedValue);

            if (productoId > 0)
            {
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                float precio;
                int stock;

                try
                {
                    if (float.TryParse(txtPrecio.Text, out precio) && int.TryParse(txtStock.Text, out stock))
                    {
                        if (stock < 0)
                        {
                            lblError.Text = "El stock no puede ser negativo.";
                            lblError.Visible = true;
                            return;
                        }

                        if (precio <= 0)
                        {
                            lblError.Text = "El precio debe ser positivo.";
                            lblError.Visible = true;
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(nombre))
                        {
                            lblError.Text = "El nombre no puede estar vacío.";
                            lblError.Visible = true;
                            return;
                        }

                        Plato producto = new Plato
                        {
                            id = productoId,
                            nombre = nombre,
                            descripcion = descripcion,
                            precio = precio,
                            stock = stock
                        };

                        //negocio.modificarPlato(producto);
                        Response.Redirect("Almacen.aspx");
                    }
                    else
                    {
                        lblError.Text = "Por favor, ingrese valores válidos para el precio y el stock.";
                        lblError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "Error al guardar el producto. Por favor, intente nuevamente. " + ex.Message;
                    lblError.Visible = true;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Almacen.aspx");
        }
    }
}