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
        PlatosService platoNegocio = new PlatosService();
        BebidasService bebidaNegocio = new BebidasService();

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
            ddlTipoProducto.Items.Insert(1, new ListItem("Plato", "1"));
            ddlTipoProducto.Items.Insert(2, new ListItem("Bebida", "2"));
        }

        protected void ddlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tipo de producto se maneja por numero 1: Plato y 2: Bebida.
            int tipoProducto = int.Parse(ddlTipoProducto.SelectedValue);

            if (tipoProducto == 1)
            {
                CargarPlatos();
            }
            else if (tipoProducto == 2)
            {
                CargarBebidas();
            }
            else
            {
                ddlProducto.Items.Clear();
                LimpiarCampos();
            }
        }
        private void CargarPlatos()
        {
            List<Plato> platos = platoNegocio.getAll();
            ddlProducto.DataSource = platos;
            ddlProducto.DataTextField = "nombre";
            ddlProducto.DataValueField = "id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione un plato", "0"));
        }

        private void CargarBebidas()
        {
            List<Bebidas> bebidas = bebidaNegocio.getAll();
            ddlProducto.DataSource = bebidas;
            ddlProducto.DataTextField = "nombre";
            ddlProducto.DataValueField = "id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione una bebida", "0"));
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
                int tipoProductoId = int.Parse(ddlTipoProducto.SelectedValue);

                // Si se elige "Plato" (1) se cargan los platos y si es Bebida (2), las bebidas.
                if (tipoProductoId == 1)
                {
                    Plato plato = platoNegocio.getAll().Find(p => p.id == productoId);
                    if (plato != null)
                    {
                        CargarDetallesProducto(plato);
                    }
                }
                else if (tipoProductoId == 2)
                {
                    Bebidas bebida = bebidaNegocio.getAll().Find(b => b.id == productoId);
                    if (bebida != null)
                    {
                        CargarDetallesProducto(bebida);
                    }
                }
            }
            else
            {
                LimpiarCampos();
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int productoId = int.Parse(ddlProducto.SelectedValue);

            if (productoId > 0)
            {
                // Se cargan variables con los listados modificados (o no modificados)
                int tipoProductoId = int.Parse(ddlTipoProducto.SelectedValue);
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                float precio;
                int stock;

                try
                {
                    // Validaciones
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

                        // Segun la opcion elegida al principio, se llenan las variables de los objetos Plato o Bebida
                        if (tipoProductoId == 1)
                        {
                            Plato plato = new Plato
                            {
                                id = productoId,
                                nombre = nombre,
                                descripcion = descripcion,
                                precio = precio,
                                stock = stock
                            };
                            platoNegocio.modificarPlato(plato);
                        }
                        else if (tipoProductoId == 2)
                        {
                            /*
                            DESCOMENTAR CUANDO CREE LA FUNCION MODIFICAR BEBIDA
                            
                            Bebidas bebida = new Bebidas
                            {
                                id = productoId,
                                nombre = nombre,
                                descripcion = descripcion,
                                precio = precio,
                                stock = stock
                            };

                            bebidasService.modificarBebida(bebida);
                            */
                        }

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