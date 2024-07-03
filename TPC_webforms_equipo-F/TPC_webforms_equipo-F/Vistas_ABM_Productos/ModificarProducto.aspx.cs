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
        ItemMenuService negocio = new ItemMenuService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTiposProducto();
            }
        }
        private void CargarTiposProducto()
        {
            ddlTipoProducto.Items.Insert(0, new ListItem("Seleccione un tipo de producto", "0"));
            ddlTipoProducto.Items.Insert(1, new ListItem("Plato", "C"));
            ddlTipoProducto.Items.Insert(2, new ListItem("Bebida", "B"));
            ddlTipoProducto.Items.Insert(3, new ListItem("Postre", "P"));
        }

        protected void ddlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tipo de producto se maneja por C: Plato, B: Bebida y P: Postres.
            char tipoProducto = char.Parse(ddlTipoProducto.SelectedValue);

            if (tipoProducto == 'C')
            {
                CargarPlatos();
            }
            else if (tipoProducto == 'B')
            {
                CargarBebidas();
            }
            else if (tipoProducto == 'P')
            {
                CargarPostres();
            }
            else
            {
                ddlProducto.Items.Clear();
                LimpiarCampos();
            }
        }
        private void CargarPlatos()
        {
            List<ItemMenu> listaPlatos = negocio.getPlatos();
            ddlProducto.DataSource = listaPlatos;
            ddlProducto.DataTextField = "nombre";
            ddlProducto.DataValueField = "id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione un plato", "0"));
        }

        private void CargarBebidas()
        {
            List<ItemMenu> listaBebidas = negocio.getBebidas();
            ddlProducto.DataSource = listaBebidas;
            ddlProducto.DataTextField = "nombre";
            ddlProducto.DataValueField = "id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione un plato", "0"));
        }
        private void CargarPostres()
        {
            List<ItemMenu> listaPostres = negocio.getPostres();
            ddlProducto.DataSource = listaPostres;
            ddlProducto.DataTextField = "nombre";
            ddlProducto.DataValueField = "id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione un plato", "0"));
        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
        }
        private void CargarDetallesProducto(ItemMenu producto)
        {
            txtNombre.Text = producto.nombre;
            txtDescripcion.Text = producto.descripcion;
            txtPrecio.Text = producto.precio.ToString();
            txtStock.Text = producto.stock.ToString();
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productoId = int.Parse(ddlProducto.SelectedValue);

            if (productoId > 0)
            {
                ItemMenu producto = negocio.getItemByID(productoId);
                if (producto != null)
                {
                    CargarDetallesProducto(producto);
                }
            }
            else
            {
                LimpiarCampos();
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ItemMenu itemModificado = new ItemMenu();

            string nombre = txtNombre.Text;
            char categoriaProducto = Convert.ToChar(ddlTipoProducto.SelectedValue);

            try
            {
                // Validacion: El plato debe tener nombre.
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    lblErrorNombre.Text = "Maestro...acordate del nombre....";
                    lblErrorNombre.Visible = true;
                    return;
                }
                else { lblErrorNombre.Visible = false; }

                // Validacion: El plato debe tener un precio.
                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    lblErrorPrecio.Text = "¿Y el precio?";
                    lblErrorPrecio.Visible = true;
                    return;
                }
                else { lblErrorPrecio.Visible = false; }

                // Validacion: Campo stock.
                if (string.IsNullOrWhiteSpace(txtStock.Text))
                {
                    lblErrorStock.Text = "¿Y el stock?";
                    lblErrorStock.Visible = true;
                    return;
                }
                else { lblErrorStock.Visible = false; }

                if (decimal.TryParse(txtPrecio.Text, out decimal precio) && int.TryParse(txtStock.Text, out int stock))
                {

                    // Validacion: El precio debe ser mayor a cero.
                    if (precio == 0)
                    {
                        lblErrorPrecio.Text = "¿Queres regalarlo? Ponele precio!";
                        lblErrorPrecio.Visible = true;
                        return;
                    }
                    else if (precio < 0)
                    {
                        lblErrorPrecio.Text = "¿Le tenemos que pagar al cliente? El precio mayor a cero!!";
                        lblErrorPrecio.Visible = true;
                        return;
                    }
                    else { lblErrorPrecio.Visible = false; }


                    // Validacion: El stock no puede ser cero.
                    if (stock < 0)
                    {
                        lblErrorStock.Text = "No contamos lo que no hay... El stock debe ser positivo.";
                        lblErrorStock.Visible = true;
                        return;
                    }
                    else { lblErrorStock.Visible = false; }

                    itemModificado.nombre = nombre;
                    itemModificado.descripcion = txtDescripcion.Text;
                    itemModificado.precio = precio;
                    itemModificado.stock = stock;
                    itemModificado.categoria = categoriaProducto;
                    itemModificado.id = int.Parse(ddlProducto.SelectedValue);

                    negocio.modificarItem(itemModificado);

                }
                else
                {
                    // Validacion: Valores inválidos en precios y stock.
                    lblError.Text = "Nada de cosas raras, solo numeros para el precio y el stock.";
                    lblError.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                // Mensaje de error no previsto.
                lblError.Text = "Error al guardar el plato. Por favor, intente nuevamente." + ex.Message;
                lblError.Visible = true;

            }

            Response.Redirect("Almacen.aspx");
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Almacen.aspx");
        }
    }
}