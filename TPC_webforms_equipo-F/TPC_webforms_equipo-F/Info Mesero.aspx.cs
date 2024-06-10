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
    public partial class InfoMesero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMeseros();
            }
        }

        protected void ddlMeseros_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMeseroSeleccionado = Convert.ToInt32(ddlMeseros.SelectedValue);

            MeserosService negocio = new MeserosService();
            Mesero mesero = negocio.GetById(idMeseroSeleccionado);
            MesasService negocioM = new MesasService();
            int canMesas = negocioM.GetCantidadMesasAsignadas(idMeseroSeleccionado);

            lblNombre.Text = mesero.name;
            lblApellido.Text = mesero.lastname;
            lblCantidaddeMesas.Text = canMesas.ToString();
             
        }

        private void CargarMeseros()
        {
            MeserosService negocio = new MeserosService();
            List<Mesero> meseros = negocio.getAll();
            

            ddlMeseros.DataSource = meseros;
            ddlMeseros.DataTextField = "name";
            ddlMeseros.DataValueField = "id_mesero";
            ddlMeseros.DataBind();

            ddlMeseros.Items.Insert(0, new ListItem("-- Seleccione un mesero --", "0"));
        }
    }
}