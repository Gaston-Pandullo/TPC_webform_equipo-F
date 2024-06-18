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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID de usuario desde la consulta URL
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int idUsuario = Convert.ToInt32(Request.QueryString["id"]);

                    // Crear un objeto Usuario con los datos del formulario
                    Usuario usuario = new Usuario();
                    usuario.id = idUsuario;
                    usuario.User = txtUsername.Text.Trim();
                    usuario.Pass = txtPassword.Text.Trim();
                    usuario.Name = txtName.Text.Trim();
                    usuario.Lastname = txtLastname.Text.Trim();
                    usuario.TipoUsuario = Convert.ToByte(ddlAdmin.SelectedValue);

                    // Llamar al método para guardar o actualizar el usuario
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.ModificarUsuario(usuario);

                    // Redireccionar a alguna página de confirmación o a la lista de usuarios
                    Response.Redirect("Administrador.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}