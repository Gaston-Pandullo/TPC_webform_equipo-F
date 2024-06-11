using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public int GetCantidadMesasAsignadas(int idMesero)
        {
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM MESA WHERE MESERO = @idMesero");
                datos.setearParametro("@idMesero", idMesero);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]);
                }
                return 0;
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

        public bool MesaEstaOcupada(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ocupada FROM mesa WHERE IDMESA = @idMesa");
                datos.setearParametro("@idMesa", idMesa);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (bool)datos.Lector["ocupada"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return false;
        }

        public void MarcarMesaComoOcupada(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE mesa SET ocupada = 1 WHERE IDMESA = @idMesa");
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

        public void MarcarMesaComoNoOcupada(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE mesa SET ocupada = 0 WHERE IDMESA = @idMesa");
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

        public int ObtenerUltimoIDComanda(int mesaId)
        {
            AccesoDatos datos = new AccesoDatos();
            int ultimoIDComanda = 0;

            try
            {
                datos.setearConsulta("SELECT TOP 1 IDCOMANDA FROM COMANDA WHERE IDMESA = @idMesa ORDER BY IDCOMANDA DESC");
                datos.setearParametro("@idMesa", mesaId);
                datos.ejecutarAccion();

                if (datos.Lector.Read())
                {
                    ultimoIDComanda = Convert.ToInt32(datos.Lector["IDCOMANDA"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return ultimoIDComanda;
        }

        public void GuardarDetallesComanda(int idComanda, int idMesa, List<int> idPlatos)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO COMANDA (IDCOMANDA, IDMESA, IDPLATO) VALUES (@idComanda, @idMesa, @idPlato)");

                // Itera sobre la lista de idPlatos y guarda cada ID en la tabla COMANDA
                foreach (int idPlato in idPlatos)
                {
                    datos.setearParametro("@idComanda", idComanda);
                    datos.setearParametro("@idMesa", idMesa);
                    datos.setearParametro("@idPlato", idPlato);
                    datos.ejecutarAccion();
                }
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
