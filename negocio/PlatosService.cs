﻿using System;
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
        private AccesoDatos datos = new AccesoDatos();
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
                    aux.precio = datos.Lector["precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["precio"]) : 0;
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

        public void modificarPlato(Plato plato)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PLATOS SET nombre=@NOMBRE,descripcion=@descripcion,precio=@precio,stock=@stock where id_Plato=@id_plato");
                datos.setearParametro("@NOMBRE", plato.nombre);
                datos.setearParametro("@descripcion", plato.descripcion);
                datos.setearParametro("@precio", plato.precio);
                datos.setearParametro("@stock", plato.stock);
                datos.setearParametro("@id_plato", plato.id);


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

        public void eliminarPlato(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM PLATOS where id_Plato=@id");
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

        public void updateStock(int id, int stock)
        {
            try
            {
                datos.setearConsulta("UPDATE PLATOS SET stock = @stock WHERE id_Plato = @id");
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
        public decimal ObtenerPrecioPorNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT precio FROM PLATOS WHERE nombre = @nombre");
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
