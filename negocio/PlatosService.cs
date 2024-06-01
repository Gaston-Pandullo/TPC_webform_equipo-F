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
            List<Plato> platos = new List<Plato>();
            try
            {
                datos.setearConsulta("SELECT * FROM PLATOS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Plato aux = new Plato();
                    aux.id = datos.Lector["id_Plato"] != DBNull.Value ? Convert.ToInt32(datos.Lector["id_Plato"]) : 0;
                    aux.nombre = datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : string.Empty;
                    aux.descripcion = datos.Lector["descripcion"] != DBNull.Value ? (string)datos.Lector["descripcion"] : string.Empty;
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["precio"]) : 0.0f;
                    aux.preparable = datos.Lector["preparable"] != DBNull.Value ? Convert.ToBoolean(datos.Lector["preparable"]) : false;
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
    }

}
