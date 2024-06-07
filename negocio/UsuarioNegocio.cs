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

    }
}
