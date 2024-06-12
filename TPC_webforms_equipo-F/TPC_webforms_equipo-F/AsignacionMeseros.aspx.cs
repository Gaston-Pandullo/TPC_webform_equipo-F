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
    public partial class AsignacionMeseros : System.Web.UI.Page
    {
        private MesasService mesasService = new MesasService();
        private MeserosService meserosService = new MeserosService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvMesas.DataKeyNames = new string[] { "id_mesa" };
                CargarMesas();
            }
        }

        private void CargarMesas()
        {
            List<Mesa> mesas = mesasService.getAll();
            gvMesas.DataSource = mesas;
            gvMesas.DataBind();
        }

        protected void gvMesas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlMeseros = (DropDownList)e.Row.FindControl("ddlMeseros");
                List<Mesero> meseros = meserosService.getAll();

                ddlMeseros.DataSource = meseros;
                ddlMeseros.DataTextField = "NombreCompleto";
                ddlMeseros.DataValueField = "id_mesero";
                ddlMeseros.DataBind();

                int idMeseroAsignado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "id_mesero"));
                if (idMeseroAsignado != 0)
                {
                    ddlMeseros.SelectedValue = idMeseroAsignado.ToString();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvMesas.Rows)
            {
                int idMesa = Convert.ToInt32(gvMesas.DataKeys[row.RowIndex].Value);
                DropDownList ddlMeseros = (DropDownList)row.FindControl("ddlMeseros");
                int idMesero = Convert.ToInt32(ddlMeseros.SelectedValue);

                mesasService.asignarMesero(idMesa, idMesero);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "showAlert();", true);

            // Recargar las mesas para reflejar los cambios
            CargarMesas();
        }
    }
}