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
            if (!IsPostBack)
            {
                UsuarioNegocio usuarios = new UsuarioNegocio();
                listaUsuarios = usuarios.getAll();
                MostrarUsuarios(listaUsuarios);
            }
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
            Button button = sender as Button;
            int idUsuario = Convert.ToInt32(button.CommandArgument);
            hfUserIdToDelete.Value = idUsuario.ToString();

            // Muestra el modal de confirmación
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showConfirmModal();", true);
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            int idUsuario = Convert.ToInt32(hfUserIdToDelete.Value);

            try
            {
                negocio.EliminarUsuario(idUsuario);
                // Recargar la lista de usuarios
                listaUsuarios = negocio.getAll();
                MostrarUsuarios(listaUsuarios);
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                lblError.Text = "Error al eliminar el usuario: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoUsuario.aspx");
        }
    }


}