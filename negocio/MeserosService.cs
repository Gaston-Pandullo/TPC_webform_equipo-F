using dominio;
using System;
using System.Collections.Generic;

namespace negocio
{
    public class MeserosService
    {
        public List<Mesero> getAll()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Mesero> meseros = new List<Mesero>();
            try
            {
                datos.setearConsulta("SELECT M.IDMESERO, U.id AS ID_USUARIO, U.username, U.password, U.name, U.lastname, U.admin, U.created_at, U.updated_at FROM MESERO M LEFT JOIN USERS U ON U.id = M.IDUSUARIO");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Mesero aux = new Mesero();
                    aux.id_mesero = datos.Lector["IDMESERO"] != DBNull.Value ? Convert.ToInt32(datos.Lector["IDMESERO"]) : 0;
                    aux.id_usuario = datos.Lector["ID_USUARIO"] != DBNull.Value ? Convert.ToInt32(datos.Lector["ID_USUARIO"]) : 0;
                    aux.name = datos.Lector["name"] != DBNull.Value ? datos.Lector["name"].ToString() : string.Empty;
                    aux.lastname = datos.Lector["lastname"] != DBNull.Value ? datos.Lector["lastname"].ToString() : string.Empty;

                    meseros.Add(aux);
                }
                return meseros;
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

        public Mesero GetById(int idMesero)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT M.IDMESERO, U.id AS ID_USUARIO, U.username, U.password, U.name, U.lastname, U.admin, U.created_at, U.updated_at FROM MESERO M LEFT JOIN USERS U ON U.id = M.IDUSUARIO WHERE M.IDMESERO = @idMesero");
                datos.setearParametro("@idMesero", idMesero);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    Mesero mesero = new Mesero();
                    mesero.id_mesero = Convert.ToInt32(datos.Lector["IDMESERO"]);
                    mesero.id_usuario = Convert.ToInt32(datos.Lector["ID_USUARIO"]);
                    mesero.name = datos.Lector["name"].ToString();
                    mesero.lastname = datos.Lector["lastname"].ToString();
                    // Puedes agregar más propiedades si necesitas
                    return mesero;
                }
                return null; // Si no se encontró el mesero con el ID especificado
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

        public int ObtenerPedidosAtendidos(int idMesero)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select count(*) as cantidad from PEDIDOS P inner join MESA M on P.idMesa=M.idMesa where mesero=@identificador");
                datos.setearParametro("@identificador", idMesero);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    int cantidad = Convert.ToInt32(datos.Lector["cantidad"]);
                    return cantidad;
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

        public decimal ObtenerTotalFacturado(int idMesero)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select sum(total) as TotalFacturado from PEDIDOS P inner join MESA M on P.idMesa=M.idMesa where mesero=@identificador");
                datos.setearParametro("@identificador", idMesero);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    if (datos.Lector["TotalFacturado"] != DBNull.Value)
                    {
                        decimal totalFacturado = Convert.ToDecimal(datos.Lector["TotalFacturado"]);
                        return totalFacturado;
                    }
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
    }
}
