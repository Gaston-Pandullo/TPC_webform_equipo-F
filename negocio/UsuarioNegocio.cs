using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class UsuarioNegocio
    {

        public bool Login(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT id, username, password, name, lastname, admin FROM USERS Where username = @username AND password = @password");
                datos.setearParametro("@username", usuario.User);
                datos.setearParametro("@password", usuario.Pass);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    usuario.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                    usuario.TipoUsuario = datos.Lector["admin"] != DBNull.Value ? Convert.ToByte(datos.Lector["admin"]) : (byte)0;
                    return true;
                }
                return false;
            }catch (Exception ex){
                throw ex;
            }
            finally{
                datos.cerrarConexion();
            }
        }

        public List<Usuario> getAll()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                datos.setearConsulta("SELECT id, username, password, name, lastname, admin FROM USERS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                    aux.User = datos.Lector["username"] != DBNull.Value ? (string)datos.Lector["username"] : string.Empty;
                    aux.Pass = datos.Lector["password"] != DBNull.Value ? (string)datos.Lector["password"] : string.Empty;
                    aux.Name = datos.Lector["name"] != DBNull.Value ? (string)datos.Lector["name"] : string.Empty;
                    aux.Lastname = datos.Lector["lastname"] != DBNull.Value ? (string)datos.Lector["lastname"] : string.Empty;
                    aux.TipoUsuario = datos.Lector["admin"] != DBNull.Value ? Convert.ToByte(datos.Lector["admin"]) : (byte)0;

                    usuarios.Add(aux);
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminarUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Eliminar filas de la tabla MESA que referencian al usuario a través de MESERO
                datos.setearConsulta("DELETE FROM MESA WHERE MESERO IN (SELECT IDMESERO FROM MESERO WHERE IDUSUARIO = @idUsuario)");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarAccion();
                datos.limpiarParametros();

                // Eliminar filas de la tabla MESERO que referencian al usuario
                datos.setearConsulta("DELETE FROM MESERO WHERE IDUSUARIO = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarAccion();
                datos.limpiarParametros();

                // Eliminar el usuario de la tabla USERS
                datos.setearConsulta("DELETE FROM USERS WHERE id = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into USERS (name,lastname,username,password, admin) values(@NAME, @LASTNAME, @USERNAME, @PASSWORD, @ADMIN)");
                datos.setearParametro("@name", usuario.Name);
                datos.setearParametro("@lastname", usuario.Lastname);
                datos.setearParametro("@username", usuario.User);
                datos.setearParametro("@password", usuario.Pass);
                datos.setearParametro("@admin", usuario.TipoUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = new Usuario();
            usuario = null;

            try
            {
                datos.setearConsulta("SELECT id, Username, Password, Name, Lastname, admin FROM USERS WHERE id = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.id= Convert.ToInt32(datos.Lector["id"]);
                    usuario.User = datos.Lector["Username"].ToString();
                    usuario.Pass = datos.Lector["Password"].ToString();
                    usuario.Name = datos.Lector["Name"].ToString();
                    usuario.Lastname = datos.Lector["Lastname"].ToString();
                    usuario.TipoUsuario = Convert.ToByte(datos.Lector["admin"]);
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades
                throw new Exception("Error al obtener el usuario por ID", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return usuario;
        }

    }
}
