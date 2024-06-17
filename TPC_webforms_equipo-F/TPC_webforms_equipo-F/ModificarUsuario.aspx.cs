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
    public partial class ModificarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica si se pasó el parámetro "id" en la URL
                if (Request.QueryString["id"] != null)
                {
                    int idUsuario = Convert.ToInt32(Request.QueryString["id"]);

                    // Lógica para cargar los datos del usuario en el formulario
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    Usuario usuario = negocio.ObtenerUsuarioPorId(idUsuario);

                    if (usuario != null)
                    {
                        // Asignar valores a los controles del formulario
                        txtUsername.Text = usuario.User;
                        txtPassword.Text = usuario.Pass;
                        txtName.Text = usuario.Name;
                        txtLastname.Text = usuario.Lastname;
                        ddlAdmin.SelectedValue = usuario.TipoUsuario.ToString();
                    }
                    else
                    {
                        // Si no se encuentra el usuario
                        Response.Redirect("Administrador.aspx"); 
                    }
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administrador.aspx");
        }
    }
}