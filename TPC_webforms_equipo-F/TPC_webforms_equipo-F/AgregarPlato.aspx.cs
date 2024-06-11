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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Plato platoNuevo = new Plato();
            PlatosService negocio = new PlatosService();

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

                // Validacion: El plato debe tener nombre.
                if (nombre == "")
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

                    Response.Redirect("Almacen.aspx");
                
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
                lblError.Text = "Error al guardar el plato. Por favor, intente nuevamente." +ex.Message;
                lblError.Visible = true;

            }


        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Almacen.aspx");
        }
    }
}