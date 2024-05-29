using dominio;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaService
    {
        AccesoDatos datos = new AccesoDatos();

        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
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

        public Categoria listarXID(int id)
        {
            Categoria categoria = new Categoria();

            try
            {
                datos.limpiarParametros();

                datos.setearConsulta("select Descripcion from Categorias where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    categoria.Id = id;

                    categoria.Descripcion = datos.Lector["Descripcion"]?.ToString();
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

            return categoria;
        }


        public void agregarCategoria(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion) VALUES (@descripcion)");
                datos.setearParametro("@descripcion", descripcion);

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

        public void modificarCategoria(Categoria cat)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE CATEGORIAS SET DESCRIPCION = @descripcion WHERE ID = @id");                
                datos.setearParametro("@descripcion", cat.Descripcion);                
                datos.setearParametro("@id", cat.Id);
                
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

        public void eliminarCategoria(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE Id = @id");
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
