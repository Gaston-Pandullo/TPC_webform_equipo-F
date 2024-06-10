using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MeserosService
    {
        AccesoDatos datos = new AccesoDatos();
        public List<Mesero> getAll()
        {
            List<Mesero> meseros = new List<Mesero>();
            try
            {
                datos.setearConsulta("SELECT M.IDMESERO, U.id AS ID_USUARIO, U.username, U.password, U.name, U.lastname, U.admin, U.created_at, U.updated_at FROM MESERO M LEFT JOIN USERS U ON U.id = M.IDUSUARIO");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Mesero aux = new Mesero();
                    aux.id_mesero = datos.Lector["IDMESERO"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IDMESERO"]) : 0;
                    aux.id_usuario = datos.Lector["ID_USUARIO"] != DBNull.Value ? Convert.ToInt32(datos.Lector["ID_USUARIO"]) : 0;
                    aux.name = datos.Lector["name"] != DBNull.Value ? datos.Lector["name"].ToString() : string.Empty;
                    aux.lastname = datos.Lector["lastname"] != DBNull.Value ? datos.Lector["lastname"].ToString() : string.Empty;

                    meseros.Add(aux);
                }
                return meseros;
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

        public Mesero GetById(int idMesero)
        {
            try
            {
                datos.setearConsulta("SELECT M.IDMESERO, U.id AS ID_USUARIO, U.username, U.password, U.name, U.lastname, U.admin, U.created_at, U.updated_at FROM MESERO M LEFT JOIN USERS U ON U.id = M.IDUSUARIO WHERE M.IDMESERO = @idMesero");
                datos.setearParametro("@idMesero", idMesero);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    Mesero mesero = new Mesero();
                    mesero.id_mesero = Convert.ToInt32(datos.Lector["IDMESERO"]);
                    mesero.id_usuario = Convert.ToInt32(datos.Lector["ID_USUARIO"]);
                    mesero.name = datos.Lector["name"].ToString();
                    mesero.lastname = datos.Lector["lastname"].ToString();
                    // Puedes agregar más propiedades si necesitas
                    return mesero;
                }
                return null; // Si no se encontró el mesero con el ID especificado
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

        
    }
}
