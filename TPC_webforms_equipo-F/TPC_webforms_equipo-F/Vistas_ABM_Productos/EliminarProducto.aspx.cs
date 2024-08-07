﻿using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_webforms_equipo_F.Vistas_ABM_Productos
{
    public partial class EliminarProducto : System.Web.UI.Page
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
            ddlTipoProducto.Items.Insert(1, new ListItem("Plato", "1"));
            ddlTipoProducto.Items.Insert(2, new ListItem("Bebida", "2"));
            ddlTipoProducto.Items.Insert(3, new ListItem("Postres", "3"));
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
            else if (tipoProducto == 3)
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
            string nombre = txtNombre.Text;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblError.Text = "Debe seleccionar un algun producto.";
                lblError.Visible = true;
                return;
            }
            else
            {
                lblError.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "confirmModal", "$('#confirmModal').modal('show');", true);
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int productoId = int.Parse(ddlProducto.SelectedValue);
                //int tipoProductoId = int.Parse(ddlTipoProducto.SelectedValue);

                negocio.eliminarItem(productoId);

                LimpiarCampos();
                ddlTipoProducto.SelectedIndex = 0;
                ddlProducto.Items.Clear();

                Response.Redirect("Almacen.aspx");
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción aquí
                lblError.Text = "Error al eliminar el producto: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Almacen.aspx");
        }
    }
}