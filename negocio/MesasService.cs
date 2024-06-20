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

                if (datos.Lector != null && datos.Lector.Read())
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


        public List<int> ObtenerPlatosPorComanda(int idComanda)
        {
            AccesoDatos datos = new AccesoDatos();
            List<int> platos = new List<int>();

            try
            {
                datos.setearConsulta("SELECT IDPLATO FROM COMANDA WHERE IDCOMANDA = @idComanda");
                datos.setearParametro("@idComanda", idComanda);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    platos.Add(datos.Lector.GetInt32(0));
                }

                return platos;
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

        public List<string> ObtenerNombresPlatosPorPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            List<string> nombresPlatos = new List<string>();

            try
            {
                datos.setearConsulta("SELECT P.nombre FROM COMANDA C INNER JOIN PLATOS P ON C.idPlato = P.id_Plato WHERE C.idPedido = @idPedido");
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    string nombrePlato = Convert.ToString(datos.Lector["nombre"]);
                    nombresPlatos.Add(nombrePlato);
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

            return nombresPlatos;
        }


        public Plato ObtenerPlatoPorID(int idPlato)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM PLATOS WHERE id_Plato = @idPlato");
                datos.setearParametro("@idPlato", idPlato);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Plato plato = new Plato
                    {
                        id = datos.Lector.GetInt32(0),
                        nombre = datos.Lector.GetString(1),
                        descripcion = datos.Lector.GetString(2),
                        precio = datos.Lector.GetDecimal(3),
                    };
                    return plato;
                }
                return null;
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

        public void GuardarDetalleComanda(int mesaId, int idPlato, int idPedido)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("INSERT INTO Comanda (IDMESA, IDPLATO, IDPEDIDO) VALUES (@idMesa, @idPlato, @idPedido)");
                datos.setearParametro("@idMesa", mesaId);
                datos.setearParametro("@idPlato", idPlato);
                datos.setearParametro("@idPedido", idPedido);
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
        public int buscarUltimoIdpedido()
        {
            AccesoDatos datos = new AccesoDatos();
            int ultimoIDPedido = 0;

            try
            {
                datos.setearConsulta("SELECT TOP 1 IDPEDIDO FROM PEDIDOS ORDER BY IDPEDIDO DESC");
                datos.ejecutarLectura();

                if (datos.Lector != null && datos.Lector.Read())
                {
                    ultimoIDPedido = Convert.ToInt32(datos.Lector["IDPEDIDO"]);
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

            return ultimoIDPedido;
        }

        public void PedidoCompleto(int IdMesa, int IdPedido)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("INSERT INTO PEDIDOS (IDMESA, IDPEDIDO) VALUES (@IDMESA, @IDPEDIDO)");
                datos.setearParametro("@IDMESA", IdMesa);
                datos.setearParametro("@IDPEDIDO", IdPedido);
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

        public void ActualizarPrecioEnPedido(decimal total, int idPedido)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE PEDIDOS SET TOTAL = @TOTAL where IDPEDIDO = @idPedido ");
                datos.setearParametro("@TOTAL", total);
                datos.setearParametro("@idPedido", idPedido);
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

        public int buscarUltimoIdpedidoxMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            int ultimoIDPedido = 0;

            try
            {
                datos.setearConsulta("SELECT TOP 1 IDPEDIDO FROM PEDIDOS WHERE IDMESA = @IDMESA ORDER BY IDPEDIDO DESC");
                datos.setearParametro("@IDPEDIDO",ultimoIDPedido);
                datos.setearParametro("@IDMESA",idMesa);
                datos.ejecutarLectura();

                if (datos.Lector != null && datos.Lector.Read())
                {
                    ultimoIDPedido = Convert.ToInt32(datos.Lector["IDPEDIDO"]);
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

            return ultimoIDPedido;
        }
    }
}
