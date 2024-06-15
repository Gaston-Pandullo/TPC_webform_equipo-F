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
                datos.setearConsulta("SELECT id_Bebida AS Id, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio, stock AS Stock, categoria FROM BEBIDAS " +
                    "UNION ALL " +
                    "SELECT id_Plato AS Id, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio, stock AS Stock, categoria FROM PLATOS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ItemMenu aux = new ItemMenu();
                    aux.id = datos.Lector["Id"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Id"]) : 0;
                    aux.nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["precio"]) : 0.0f;
                    aux.stock = datos.Lector["Stock"] != DBNull.Value ? Convert.ToInt32(datos.Lector["Stock"]) : 0;
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
    }
}
