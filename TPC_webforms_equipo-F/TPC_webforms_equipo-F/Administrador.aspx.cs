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
    public partial class Administrador : System.Web.UI.Page
    {
        public List<Usuario> listaUsuarios = new List<Usuario>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioNegocio usuarios = new UsuarioNegocio();
            listaUsuarios = usuarios.getAll();
            MostrarUsuarios(listaUsuarios);
        }

        private void MostrarUsuarios(List<Usuario> listaUsuarios)
        {
            rptUsuarios.DataSource = listaUsuarios;
            rptUsuarios.DataBind();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            // Obtiene el ID del usuario desde el CommandArgument del botón
            Button btn = (Button)sender;
            int idUsuario = Convert.ToInt32(btn.CommandArgument);

            // Redirige a una página de edición o abre un formulario para modificar el usuario
            Response.Redirect($"ModificarUsuario.aspx?id={idUsuario}");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Button button = sender as Button;
            Usuario usuario = new Usuario();

            try
            {
                usuario.id = Convert.ToInt32(button.CommandArgument);
                negocio.EliminarUsuario(usuario.id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoUsuario.aspx");
        }
    }
}