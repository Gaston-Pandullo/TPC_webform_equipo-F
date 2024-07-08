using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ItemMenuService
    {
        AccesoDatos datos = new AccesoDatos();
        public List<ItemMenu> getAll()
        {
            List<ItemMenu> itemMenus = new List<ItemMenu>();
            try
            {
                datos.setearConsulta("SELECT * FROM ITEM_MENU ");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ItemMenu aux = new ItemMenu();
                    aux.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
                    aux.stock = datos.Lector["stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["stock"]) : 0;
                    aux.cantidad = datos.Lector["cantidad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["cantidad"]) : 0;
                    aux.categoria = datos.Lector["categoria"] != DBNull.Value ? Convert.ToChar(datos.Lector["categoria"]) : 'C';

                    itemMenus.Add(aux);
                }
                return itemMenus;
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

        public void DescontarStock(int id, int cantidadADescontar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.limpiarParametros();
                datos.setearConsulta("UPDATE ITEM_MENU SET stock = stock - @cantidadADescontar WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@cantidadADescontar", cantidadADescontar);
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

        public int GetStockById(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.limpiarParametros();
                datos.setearConsulta("SELECT stock FROM ITEM_MENU WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector["stock"]);
                }
                else
                {
                    return 0; // Si no se encuentra el ítem, devolver 0
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

        public void updateStock(int id, int stock)
        {
            try
            {
                datos.limpiarParametros();
                datos.setearConsulta("UPDATE ITEM_MENU SET stock = @stock WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@stock", stock);
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

        public decimal getPrice(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT precio FROM ITEM_MENU WHERE nombre = @nombre");
                datos.setearParametro("@nombre", nombre);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
                }
                else
                {
                    return 0;
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

        public int getIdbyName(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT id FROM ITEM_MENU WHERE nombre = @nombre");
                datos.setearParametro("@nombre", nombre);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector["id"]);
                }
                else
                {
                    return 0; 
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
        public void agregarItem(ItemMenu item)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT Into ITEM_MENU(nombre,descripcion,precio,stock,categoria) VALUES (@nombre,@descripcion,@precio,@stock,@categoria)");
                datos.setearParametro("@nombre", item.nombre);
                datos.setearParametro("@descripcion", item.descripcion);
                datos.setearParametro("@precio", item.precio);
                datos.setearParametro("@stock", item.stock);
                datos.setearParametro("@categoria", item.categoria);

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
        public void modificarItem(ItemMenu item)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ITEM_MENU SET nombre=@NOMBRE,descripcion=@descripcion,precio=@precio,stock=@stock where id=@id_item");
                datos.setearParametro("@NOMBRE", item.nombre);
                datos.setearParametro("@descripcion", item.descripcion);
                datos.setearParametro("@precio", item.precio);
                datos.setearParametro("@stock", item.stock);
                datos.setearParametro("@id_item", item.id);


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

        public void eliminarItem(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from ITEM_MENU where id=@id");
                datos.setearParametro("@id", id);
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
        public List<ItemMenu> getPlatos()
        {
            List<ItemMenu> itemMenus = new List<ItemMenu>();
            try
            {
                datos.setearConsulta("select * from ITEM_MENU where categoria='C'");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ItemMenu aux = new ItemMenu();
                    aux.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
                    aux.stock = datos.Lector["stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["stock"]) : 0;
                    aux.cantidad = datos.Lector["cantidad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["cantidad"]) : 0;
                    aux.categoria = datos.Lector["categoria"] != DBNull.Value ? Convert.ToChar(datos.Lector["categoria"]) : 'C';

                    itemMenus.Add(aux);
                }
                return itemMenus;
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
        public List<ItemMenu> getBebidas()
        {
            List<ItemMenu> itemMenus = new List<ItemMenu>();
            try
            {
                datos.setearConsulta("select * from ITEM_MENU where categoria='B'");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ItemMenu aux = new ItemMenu();
                    aux.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
                    aux.stock = datos.Lector["stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["stock"]) : 0;
                    aux.cantidad = datos.Lector["cantidad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["cantidad"]) : 0;
                    aux.categoria = datos.Lector["categoria"] != DBNull.Value ? Convert.ToChar(datos.Lector["categoria"]) : 'C';

                    itemMenus.Add(aux);
                }
                return itemMenus;
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
        public List<ItemMenu> getPostres()
        {
            List<ItemMenu> itemMenus = new List<ItemMenu>();
            try
            {
                datos.setearConsulta("select * from ITEM_MENU where categoria='P'");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ItemMenu aux = new ItemMenu();
                    aux.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
                    aux.stock = datos.Lector["stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["stock"]) : 0;
                    aux.cantidad = datos.Lector["cantidad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["cantidad"]) : 0;
                    aux.categoria = datos.Lector["categoria"] != DBNull.Value ? Convert.ToChar(datos.Lector["categoria"]) : 'C';

                    itemMenus.Add(aux);
                }
                return itemMenus;
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
        public ItemMenu getItemByID(int id)
        {
            ItemMenu itemMenus = new ItemMenu();
            try
            {
                datos.setearConsulta("SELECT * FROM ITEM_MENU where id=@id_item");
                datos.setearParametro("@id_item", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    itemMenus.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                    itemMenus.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    itemMenus.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    itemMenus.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
                    itemMenus.stock = datos.Lector["stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["stock"]) : 0;
                    itemMenus.cantidad = datos.Lector["cantidad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["cantidad"]) : 0;
                    itemMenus.categoria = datos.Lector["categoria"] != DBNull.Value ? Convert.ToChar(datos.Lector["categoria"]) : 'C';
                }
                return itemMenus;
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

        public List<ItemMenu> getItems_by_filtro(string filtro)
        {
            List<ItemMenu> listaItems = new List<ItemMenu>();
            try
            {
                
                    datos.setearConsulta("select * from ITEM_MENU where nombre like @filtro + '%'");
                    datos.setearParametro("@filtro", filtro);
                    datos.ejecutarLectura();
                    while (datos.Lector.Read())
                    {
                        ItemMenu aux = new ItemMenu();
                        aux.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                        aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                        aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                        aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
                        aux.stock = datos.Lector["stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["stock"]) : 0;
                        aux.cantidad = datos.Lector["cantidad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["cantidad"]) : 0;
                        aux.categoria = datos.Lector["categoria"] != DBNull.Value ? Convert.ToChar(datos.Lector["categoria"]) : 'C';

                        listaItems.Add(aux);
                    }
                    return listaItems;
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
        public List<ItemMenu> getItems_by_filtroCategoria(char filtro)
        {
            List<ItemMenu> listaItems = new List<ItemMenu>();
            try
            {

                datos.setearConsulta("select * from ITEM_MENU where categoria = @filtro");
                datos.setearParametro("@filtro", filtro);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ItemMenu aux = new ItemMenu();
                    aux.id = datos.Lector["id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id"]) : 0;
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
                    aux.stock = datos.Lector["stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["stock"]) : 0;
                    aux.cantidad = datos.Lector["cantidad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["cantidad"]) : 0;
                    aux.categoria = datos.Lector["categoria"] != DBNull.Value ? Convert.ToChar(datos.Lector["categoria"]) : 'C';

                    listaItems.Add(aux);
                }
                return listaItems;
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
        public int ObtenerCantidadesVendidas(int idItem)
        {
            AccesoDatos datos = new AccesoDatos();
            int totalPedido=0;
            try
            {
                datos.setearConsulta("select sum(cantidad) as cantidad from COMANDA where idItem=@id_item");
                datos.setearParametro("@id_item", idItem);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    totalPedido = datos.Lector["cantidad"] != DBNull.Value ? Convert.ToInt32(datos.Lector["cantidad"]) : 0;
                }
                return totalPedido;
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
