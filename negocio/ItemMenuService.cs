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
    }
}
