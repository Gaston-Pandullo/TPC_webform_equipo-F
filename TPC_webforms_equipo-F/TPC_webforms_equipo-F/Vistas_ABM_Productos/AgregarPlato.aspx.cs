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
        PlatosService negocio = new PlatosService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            char TipoProducto = Convert.ToChar(ddlTipo.SelectedValue);

            switch (TipoProducto)
            {
                case 'C':
                    Plato platoNuevo = new Plato();

                    string nombre = txtNombre.Text;
                    string descripcion = txtDescripcion.Text;
                    float precio;
                    int stock;


                    try
                    {
                        if (float.TryParse(txtPrecio.Text, out precio) && int.TryParse(txtStock.Text, out stock))
                        {
                            // Validacion: El stock no puede ser cero.
                            if (stock < 0)
                            {
                                lblError.Text = "El stock no puede ser negativo.";
                                lblError.Visible = true;
                                return;
                            }

                            // Validacion: El precio debe ser mayor a cero.
                            if (precio <= 0)
                            {
                                lblError.Text = "El precio debe ser positivo.";
                                lblError.Visible = true;
                                return;
                            }

                            // Validacion: El plato debe tener nombre.
                            if (string.IsNullOrWhiteSpace(nombre))
                            {
                                lblError.Text = "No puede cargar un plato sin nombre.";
                                lblError.Visible = true;
                                return;
                            }


                            platoNuevo.nombre = nombre;
                            platoNuevo.descripcion = descripcion;
                            platoNuevo.precio = precio;
                            platoNuevo.stock = stock;


                            negocio.agregarPlato(platoNuevo);

                            

                        }
                        else
                        {
                            // Validacion: Valores inválidos en precios y stock.
                            lblError.Text = "Por favor, ingrese valores válidos para el precio y el stock.";
                            lblError.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {

                        // Mensaje de error no previsto.
                        lblError.Text = "Error al guardar el plato. Por favor, intente nuevamente." + ex.Message;
                        lblError.Visible = true;
                    }
                break;

                case 'B':

                break;

                default:
                    
                break;
            }

            Response.Redirect("Almacen.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Almacen.aspx");
        }
    }
}