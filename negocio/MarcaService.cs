using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio; 

namespace negocio
{
    public class MarcaService
    {
        AccesoDatos datos = new AccesoDatos();

        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    if (!(datos.Lector["Descripcion"] is DBNull))
                    {
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    }

                    lista.Add(aux);
                }
                return lista;
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

        public Marca listarXID(int id)
        {
            Marca marca = new Marca();

            try
            {
                datos.limpiarParametros();
                datos.setearConsulta("select Descripcion from Marcas where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    marca.Id = id;

                    marca.Descripcion = datos.Lector["Descripcion"]?.ToString();
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

            return marca;
        }

        public void agregarMarca(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO MARCAS (Descripcion) VALUES (@descripcion)");
                datos.setearParametro("@descripcion", marca.Descripcion);

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

        public void modificarMarca(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET DESCRIPCION = @descripcion WHERE ID = @id");
                datos.setearParametro("@descripcion", marca.Descripcion);
                datos.setearParametro("@id", marca.Id);

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

        public void eliminarMarca(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @id");
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


    }
}

