using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
                datos.setearConsulta("SELECT idMesa, mesero, ocupada, activo FROM MESA WHERE activo = 1");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Mesa aux = new Mesa();
                    aux.id_mesa = datos.Lector["IDMESA"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IDMESA"]) : 0;
                    aux.id_mesero = datos.Lector["MESERO"] != DBNull.Value ? Convert.ToInt32(datos.Lector["MESERO"]) : 0;
                    aux.ocupada = datos.Lector["OCUPADA"] != DBNull.Value ? Convert.ToBoolean(datos.Lector["OCUPADA"]) : false;
                    aux.activo = datos.Lector["ACTIVO"] != DBNull.Value ? Convert.ToBoolean(datos.Lector["ACTIVO"]) : false;

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

        public void OcuparMesa(int idMesa)
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

        public List<Plato> ObtenerNombresPlatosPorPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Plato> platos = new List<Plato>();
            //Crear lista con datos de la query

            try
            {
                datos.setearConsulta("SELECT P.nombre as nombre, P.precio as precio, COUNT(*) as cantidad FROM COMANDA C INNER JOIN PLATOS P ON C.idPlato = P.id_Plato WHERE C.idPedido = @idPedido GROUP BY P.nombre, P.precio;");
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Plato plato = new Plato();
                    plato.nombre = Convert.ToString(datos.Lector["nombre"]);
                    plato.precio = Convert.ToDecimal(datos.Lector["precio"]);
                    plato.cantidad = Convert.ToInt32(datos.Lector["cantidad"]);
                    Debug.WriteLine($"Plato: {plato.nombre}, Cantidad: {plato.cantidad}, Precio: {plato.precio:C}");
                    platos.Add(plato);

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

            return platos;
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

        public Pedido CrearPedido(int IdMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO PEDIDOS (idMesa, fechaPedido, total) VALUES (@IDMESA, @fechaPedido, @total); SELECT SCOPE_IDENTITY();");
                datos.setearParametro("@IDMESA", IdMesa);
                datos.setearParametro("@fechaPedido", DateTime.Now);
                datos.setearParametro("@total", 0);
                datos.ejecutarAccion();

                datos.limpiarParametros(); // Limpiar parámetros antes de la nueva consulta
                datos.setearConsulta("SELECT idPedido FROM PEDIDOS WHERE idPedido = SCOPE_IDENTITY();");
                int idPedido = Convert.ToInt32(datos.ejecutarEscalar());

                Pedido nuevoPedido = new Pedido
                {
                    idPedido = idPedido,
                    idMesa = IdMesa,
                    fechaPedido = DateTime.Now,
                    total = 0,
                    comandas = new List<Comanda>()
                };
                return nuevoPedido;
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

        public void updateMesaQty(bool add)
        {
            try
            {
                if (add){datos.setearConsulta("INSERT INTO MESA (mesero, ocupada, activo) VALUES (NULL, 0, 1);");}
                else{datos.setearConsulta("UPDATE MESA SET activo = 0 WHERE idMesa = (SELECT MAX(idMesa) FROM MESA WHERE activo = 1);");}
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

        public bool CheckMesasLibres()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                int cantidadMesas = 0;
                datos.setearConsulta("SELECT COUNT(*) as cantidad FROM MESA WHERE MESA.ocupada = 1;");
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {   
                    cantidadMesas = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("cantidad")) ? 0 : (int)datos.Lector["cantidad"];
                }
                return (cantidadMesas > 0);
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
        public decimal calcularTotal(int idPedido)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("select sum(c.cantidad * it.precio) as total from COMANDA c inner join ITEM_MENU it on it.id = c.idItem where idPedido = @idPedido");
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    decimal total = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("total")) ? 0 : (decimal)datos.Lector["total"];
                    return total;
                }
                else
                {
                    throw new Exception("No se encontró el total para el pedido especificado.");
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

        public int ObtenerIdMesaPorNumero(int numeroMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            int idMesa = 0;

            try
            {
                datos.setearConsulta("SELECT idMesa FROM MESA WHERE numeroMesa = @numeroMesa");
                datos.setearParametro("@numeroMesa", numeroMesa);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    idMesa = Convert.ToInt32(datos.Lector["idMesa"]);
                }

                return idMesa;
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
