using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MesasService
    {
        AccesoDatos datos = new AccesoDatos();
        public List<Mesa> getAll()
        {
            List<Mesa> mesas = new List<Mesa>();
            try
            {
                datos.setearConsulta("SELECT IDMESA, MESERO, OCUPADA FROM MESA");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Mesa aux = new Mesa();
                    aux.id_mesa = datos.Lector["IDMESA"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IDMESA"]) : 0;
                    aux.id_mesero = datos.Lector["MESERO"] != DBNull.Value ? Convert.ToInt32(datos.Lector["MESERO"]) : 0;
                    aux.ocupada = datos.Lector["OCUPADA"] != DBNull.Value ? Convert.ToBoolean(datos.Lector["OCUPADA"]) : false;

                    mesas.Add(aux);
                }
                return mesas;
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
        public void asignarMesero(int idMesa, int idMesero)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MESA SET MESERO = @idMesero WHERE IDMESA = @idMesa");
                datos.setearParametro("@idMesero", idMesero);
                datos.setearParametro("@idMesa", idMesa);
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
    }
}
