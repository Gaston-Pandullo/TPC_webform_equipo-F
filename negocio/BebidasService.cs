using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class BebidasService
    {
        private AccesoDatos datos = new AccesoDatos();
        public List<Bebidas> getAll()
        {
            List<Bebidas> bebidas = new List<Bebidas>();
            try
            {
                datos.setearConsulta("SELECT * FROM BEBIDAS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Bebidas aux = new Bebidas();
                    aux.id = datos.Lector["id_Bebida"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id_Bebida"]) : 0;
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["precio"]) : 0.0f;
                    aux.stock = datos.Lector["stock"] != DBNull.Value ? (int)datos.Lector["stock"] : 0;
                    aux.alcoholica = datos.Lector["alcoholica"] != DBNull.Value ? Convert.ToByte(datos.Lector["alcoholica"]) : (byte)0;
                    aux.estado = datos.Lector["estado"] != DBNull.Value ? Convert.ToBoolean(datos.Lector["estado"]) : false;

                    bebidas.Add(aux);
                }
                return bebidas;
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
        public void updateStock(int id, int stock, bool incrementar)
        {
            try
            {
                datos.limpiarParametros();
                datos.setearConsulta("UPDATE BEBIDAS SET stock = @stock WHERE id_Bebida = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@stock", incrementar ? stock + 1 : stock - 1);
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
        public void agregarBebida(Bebidas bebida)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO BEBIDAS (nombre,descripcion,precio,stock) VALUES (@nombre,@descripcion,@precio,@stock)");
                datos.setearParametro("@nombre", bebida.nombre);
                datos.setearParametro("@descripcion", bebida.descripcion);
                datos.setearParametro("@precio", bebida.precio);
                datos.setearParametro("@stock", bebida.stock);

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
