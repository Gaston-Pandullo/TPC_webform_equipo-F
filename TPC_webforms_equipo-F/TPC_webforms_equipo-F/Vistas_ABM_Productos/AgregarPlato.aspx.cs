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
    public partial class AgregarPlato : System.Web.UI.Page
    {
        ItemMenuService negocio = new ItemMenuService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ItemMenu itemNuevo = new ItemMenu();

            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            char categoriaProducto = Convert.ToChar(ddlTipo.SelectedValue);

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

                    itemNuevo.nombre = nombre;
                    itemNuevo.descripcion = descripcion;
                    itemNuevo.precio = precio;
                    itemNuevo.stock = stock;
                    itemNuevo.categoria = categoriaProducto;

                    negocio.agregarItem(itemNuevo);

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