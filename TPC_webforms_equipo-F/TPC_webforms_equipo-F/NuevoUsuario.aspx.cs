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
    public partial class NuevoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administrador.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            usuario.Name = txtName.Text;
            usuario.Lastname = txtLastName.Text;
            usuario.User = txtUsername.Text;
            usuario.Pass = txtPassword.Text;
            usuario.TipoUsuario = Convert.ToByte(ddlAdmin.SelectedValue);

            negocio.AgregarUsuario(usuario);

            Response.Redirect("Administrador.aspx");

        }
    }
}