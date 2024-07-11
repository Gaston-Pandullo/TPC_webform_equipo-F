using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_webforms_equipo_F
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //REDIRIGE SI NO ESTA LOGEADO Y QUIERE IR A OTRA PAGINA
            if (!(Page is Login))
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            // OCULTA LINKS 
            if (Seguridad.sesionActiva(Session["usuario"]))
            {
                loginLink.Visible = false;
                logoutLink.Visible = true;
            }else
            {
                loginLink.Visible = true;
                logoutLink.Visible = false;
            }
            // OCULTA LINKS
            if (!Seguridad.esAdmin(Session["usuario"]))
            {
                almacenLink.Visible = false;
                adminLink.Visible = false;
                informeLink.Visible = false;
                meserosLink.Visible = false;
            }
            if(!(Page is _Default) && !(Page is Menu))
            {
                if (!Seguridad.esAdmin(Session["usuario"]) && Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx", false);
        }
    }
}