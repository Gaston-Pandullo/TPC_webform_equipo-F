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
            

            if (!Seguridad.esAdmin(Session["usuario"]))
            {
                Session.Add("error", "Necesitas permisos de admin para ingresar aca.");
                Response.Redirect("Error.aspx", false);
            }


        }
    }
}