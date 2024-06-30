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
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
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
        public void updateStock(int id, int stock)
        {
            try
            {
                datos.limpiarParametros();
                datos.setearConsulta("UPDATE BEBIDAS SET stock = @stock WHERE id_Bebida = @id");
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
        public void modificarBebida(Bebidas bebida)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE BEBIDAS SET nombre=@NOMBRE,descripcion=@descripcion,precio=@precio,stock=@stock where id_Bebida=@id_bebida");
                datos.setearParametro("@NOMBRE", bebida.nombre);
                datos.setearParametro("@descripcion", bebida.descripcion);
                datos.setearParametro("@precio", bebida.precio);
                datos.setearParametro("@stock", bebida.stock);
                datos.setearParametro("@id_bebida", bebida.id);


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
        public void eliminarBebida(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM BEBIDAS where id_Bebida=@id");
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
        public decimal ObtenerPrecioPorNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT precio FROM BEBIDAS WHERE nombre = @nombre");
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
    }

}
