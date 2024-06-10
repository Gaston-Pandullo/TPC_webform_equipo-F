using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class PlatosService
    {
        AccesoDatos datos = new AccesoDatos();
        public List<Plato> getAll()
        {
            //Lista de platos
            List<Plato> platos = new List<Plato>();
            try
            {
                // Carga de la lista de platos
                datos.setearConsulta("SELECT * FROM PLATOS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Plato aux = new Plato();
                    aux.id = datos.Lector["id_Plato"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id_Plato"]) : 0;
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["precio"]) : 0.0f;
                    aux.estado = datos.Lector["estado"] != DBNull.Value ? Convert.ToBoolean(datos.Lector["estado"]) : false;

                    platos.Add(aux);
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
        public void agregarPlato(Plato plato)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO PLATOS (nombre,descripcion,precio,stock) VALUES (@nombre,@descripcion,@precio,@stock)");
                datos.setearParametro("@nombre", plato.nombre);
                datos.setearParametro("@descripcion", plato.descripcion);
                datos.setearParametro("@precio", plato.precio);
                datos.setearParametro("@stock", plato.stock);

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
