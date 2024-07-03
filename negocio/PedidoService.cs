using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PedidoService
    {
        AccesoDatos datos = new AccesoDatos();

        public int GetPedidoIdByMesaId(int mesaId)
        {
            int pedidoId = 0;
            string query = @"SELECT p.idPedido FROM PEDIDOS p WHERE p.idMesa = @idMesa";
            try
            {
                datos.setearConsulta(query);
                datos.setearParametro("@idMesa", mesaId);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    pedidoId = (int)datos.Lector["idPedido"];
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

            return pedidoId;
        }
        public Pedido GetPedidoByMesaId(int mesaId)
        {
            Pedido pedido = null;
            string query = @"SELECT p.idPedido, p.fechaPedido, p.total, p.idMesa FROM PEDIDOS p WHERE p.idMesa = @idMesa";

            try
            {
                datos.setearConsulta(query);
                datos.setearParametro("@idMesa", mesaId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    if (pedido == null)
                    {
                        pedido = new Pedido
                        {
                            idPedido = (int)datos.Lector["idPedido"],
                            fechaPedido = (DateTime)datos.Lector["fechaPedido"],
                            total = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("total")) ? 0 : (decimal)datos.Lector["total"],
                            idMesa = (int)datos.Lector["idMesa"],
                            comandas = new List<Comanda>()
                        };
                    }

                    if (!datos.Lector.IsDBNull(datos.Lector.GetOrdinal("idComanda")))
                    {
                        var idComanda = (int)datos.Lector["idComanda"];
                        var comanda = pedido.comandas.FirstOrDefault(c => c.id == idComanda);
                        if (comanda == null)
                        {
                            comanda = new Comanda
                            {
                                id = idComanda,
                                idPedido = (int)datos.Lector["idPedido"],
                                items = new List<ItemMenu>()
                            };
                            pedido.comandas.Add(comanda);
                        }

                        comanda.items.Add(new ItemMenu
                        {
                            id = (int)datos.Lector["idItem"],
                            nombre = (string)datos.Lector["nombre"],
                            precio = (decimal)datos.Lector["precio"],
                            cantidad = (int)datos.Lector["cantidad"]
                        });
                    }
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

            return pedido;
        }
        public List<Comanda> GetComandasByPedidoId(int pedidoId)
        {
            List<Comanda> comandas = new List<Comanda>();
            string query = @"
        SELECT c.idComanda, c.idPedido,
               im.id AS idItemMenu, im.nombre, im.descripcion, im.precio, im.stock, im.cantidad, im.estado, im.categoria
        FROM COMANDA c
        INNER JOIN ITEM_MENU im ON c.idItem = im.id
        WHERE c.idPedido = @idPedido";

            try
            {
                datos.setearConsulta(query);
                datos.setearParametro("@idPedido", pedidoId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idComanda = (int)datos.Lector["idComanda"];
                    int idPedido = (int)datos.Lector["idPedido"];

                    // Verificar si la comanda ya está en la lista
                    Comanda comanda = comandas.FirstOrDefault(c => c.id == idComanda);
                    if (comanda == null)
                    {
                        comanda = new Comanda
                        {
                            id = idComanda,
                            idPedido = idPedido,
                            precioTotal = 0, // Ajusta esto según lo que necesites
                            Fecha = DateTime.Now,
                            items = new List<ItemMenu>()
                        };
                        comandas.Add(comanda);
                    }

                    // Agregar el item de menú a la comanda
                    ItemMenu itemMenu = new ItemMenu
                    {
                        id = (int)datos.Lector["idItemMenu"],
                        nombre = (string)datos.Lector["nombre"],
                        descripcion = (string)datos.Lector["descripcion"],
                        precio = (decimal)datos.Lector["precio"],
                        stock = (int)datos.Lector["stock"],
                        cantidad = (int)datos.Lector["cantidad"],
                        estado = (bool)datos.Lector["estado"],
                        categoria = ((string)datos.Lector["categoria"])[0]
                    };
                    comanda.items.Add(itemMenu);
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

            return comandas;
        }

        public Pedido GetPedidoById(int pedidoId)
        {
            try
            {
                datos.setearConsulta("SELECT p.idPedido, p.fechaPedido, p.total, p.idMesa FROM PEDIDOS p WHERE p.idPedido = @pedidoId");
                datos.setearParametro("@pedidoId", pedidoId);
                datos.ejecutarLectura();

                Pedido pedido = new Pedido();
                if (datos.Lector.Read())
                {
                    {
                        pedido.idPedido = (int)datos.Lector["idPedido"];
                        pedido.fechaPedido = (DateTime)datos.Lector["fechaPedido"];
                        pedido.total = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("total")) ? 0 : (decimal)datos.Lector["total"];
                        pedido.idMesa = (int)datos.Lector["idMesa"];
                        pedido.comandas = new List<Comanda>();
                    };
                    //if (!datos.Lector.IsDBNull(datos.Lector.GetOrdinal("idComanda")))
                    //{
                    //    var idComanda = (int)datos.Lector["idComanda"];
                    //    var comanda = pedido.comandas.FirstOrDefault(c => c.id == idComanda);
                    //    if (comanda == null)
                    //    {
                    //        comanda = new Comanda
                    //        {
                    //            id = idComanda,
                    //            idPedido = (int)datos.Lector["idPedido"],
                    //            items = new List<ItemMenu>()
                    //        };
                    //        pedido.comandas.Add(comanda);
                    //    }
                    //
                    //    comanda.items.Add(new ItemMenu
                    //    {
                    //        id = (int)datos.Lector["idItem"],
                    //        nombre = (string)datos.Lector["nombre"],
                    //        precio = (decimal)datos.Lector["precio"],
                    //        cantidad = (int)datos.Lector["cantidad"]
                    //    });
                    //}
                }
                return pedido;
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

        public void ActualizarPedido(Pedido pedido)
        {
            try
            {
                if (!ExisteMesa(pedido.idMesa))
                {
                    throw new Exception("El idMesa especificado no existe en la base de datos.");
                }

                foreach (Comanda comanda in pedido.comandas)
                {
                    foreach (ItemMenu item in comanda.items)
                    {
                        AccesoDatos datos = new AccesoDatos();
                        datos.limpiarParametros();
                        datos.setearConsulta("INSERT INTO COMANDA (idPedido, idItem, cantidad) VALUES (@idPedido, @idItem, @cantidad)");
                        datos.setearParametro("@idPedido", pedido.idPedido);
                        datos.setearParametro("@idItem", item.id);
                        datos.setearParametro("@cantidad", item.cantidad);

                        datos.ejecutarAccion();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el pedido en la base de datos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public bool ExisteMesa(int idMesa)
        {
            bool existe = false;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM MESA WHERE idMesa = @idMesa");
                datos.setearParametro("@idMesa", idMesa);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = Convert.ToInt32(datos.Lector[0]);
                    existe = (count > 0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia de la mesa en la base de datos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return existe;
        }
    }
}

