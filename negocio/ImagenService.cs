using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ImagenService
    {
        private readonly AccesoDatos accesoDatos = new AccesoDatos();

        public List<Imagen> Listar(int id)
        {
            List<Imagen> images = new List<Imagen>();

            try
            {
                accesoDatos.limpiarParametros();

                accesoDatos.setearConsulta("SELECT Id as IdImagen, ImagenUrl as URL FROM IMAGENES WHERE IdArticulo = @articuloID");
                accesoDatos.setearParametro("@articuloID", id);
                accesoDatos.ejecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Imagen auxImg = new Imagen();
                    auxImg.Codigo = accesoDatos.Lector["IdImagen"] as int? ?? auxImg.Codigo;
                    auxImg.Url = accesoDatos.Lector["URL"]?.ToString();

                    images.Add(auxImg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }

            return images;
        }

        public void Agregar(Imagen image, int articleId)
        {
            try
            {
                accesoDatos.setearConsulta(
                    "insert into imagenes(IdArticulo, ImagenUrl) values (@articleId, @imageUrl)"
                );
                accesoDatos.setearParametro("@articleId", articleId);
                accesoDatos.setearParametro("@imageUrl", image.Url);
                accesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public void Add(List<Imagen> images, int articleId)
        {
            try
            {
                foreach (Imagen image in images)
                {
                    accesoDatos.setearConsulta(
                        "insert into imagenes(IdArticulo, ImagenUrl) values (@articleId, @imageUrl)"
                    );
                    accesoDatos.setearParametro("@articleId", articleId);
                    accesoDatos.setearParametro("@imageUrl", image.Url);
                    accesoDatos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public void Editar(Imagen image)
        {
            try
            {
                accesoDatos.setearConsulta("update imagenes set ImagenUrl = @imageUrl where Id = @Id");
                accesoDatos.setearParametro("@Id", image.Codigo);
                accesoDatos.setearParametro("@imageUrl", image.Url);
                accesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public void Borrar(Imagen image)
        {
            try
            {
                accesoDatos.setearConsulta("delete from imagenes where Id = @Id");
                accesoDatos.setearParametro("@Id", image.Codigo);
                accesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public int ListarXID(Imagen image)
        {
            if (image == null)
            {
                return 0;
            }

            int id = 0;

            try
            {
                accesoDatos.setearConsulta("select Id from imagenes where ImagenUrl = @ImageUrl");
                accesoDatos.setearParametro("@ImageUrl", image.Url);
                accesoDatos.ejecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    id = (int)accesoDatos.Lector["Id"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }

            return id;
        }
    }
}
