﻿using SistemaGestionEntities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Net;

namespace SistemaGestionData
{
    public static class ProductoData
    {
       
        public static List<Producto> ListarProductos()
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                string query = "SELECT id,descripciones,costo,precioVenta,stock,idUsuario FROM Producto;";
                // cambiar el connection = ConnectionAD.GetConnection()
                using (SqlConnection connection = ConnectionADO.GetConnection())
                {

                    SqlCommand command = new SqlCommand(query, connection);
                    

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto();
                        producto.Id = Convert.ToInt32(reader["Id"]);
                        producto.Descripcion = reader["Descripciones"].ToString();
                        producto.Costo = Convert.ToDouble(reader["Costo"]);
                        producto.PrecioVenta = Convert.ToDouble(reader["PrecioVenta"]);
                        producto.Stock = Convert.ToInt32(reader["Stock"]);
                        producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                        productos.Add(producto);


                    }
                }
                return productos;
            } catch (Exception ex)
            {
                throw new Exception("Productos  no Encontrados", ex);
            }
        }

        public static Producto ObtenerProducto(int id)
        {
            try
            {
                Producto? producto = ProductoData.ListarProductos().Find(p => p.Id == id);
                return producto;
            } catch (Exception ex)
            {
                throw new Exception("Producto  no Encontrado", ex);
            }
        }

        public static List<Producto> ProductosPorIdUsuario(int idUsuario) 
        {
            try
            {
                List<Producto> productosUsuario = new List<Producto>();
                
                foreach (Producto item in ProductoData.ListarProductos())
                {
                    if(item.IdUsuario == idUsuario)
                    {
                        
                        productosUsuario.Add(item);
                    }
                }
                if (productosUsuario == null || productosUsuario.Count == 0)
                {
                throw new Exception("No se encuentra productos agregados por el Usuario");
                }
                return productosUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Productos no Encontrados", ex);
            }
        }

        public static void CrearProducto(Producto producto)
        {
            try
            {
                using (SqlConnection connection = ConnectionADO.GetConnection())
                {
                    string query = "INSERT INTO Producto(Descripciones,Costo,PrecioVenta,Stock, IdUsuario) values(@descripciones,@costo,@precioVenta,@stock,@idUsuario)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("Descripciones", producto.Descripcion);
                    command.Parameters.AddWithValue("Costo", producto.Costo);
                    command.Parameters.AddWithValue("PrecioVenta", producto.PrecioVenta);
                    command.Parameters.AddWithValue("Stock", producto.Stock);
                    command.Parameters.AddWithValue("IdUsuario", producto.IdUsuario);
                   

                    command.ExecuteNonQuery();


                }
            } catch (Exception ex) 
            {
                throw new Exception("Producto no Encontrado", ex);
            }
        }

        //ModificarProducto
        public static void  ModificarProducto(Producto producto)
        {
            try
            {
                using (SqlConnection connection = ConnectionADO.GetConnection())
                {
                    string query = "UPDATE Producto SET Descripciones = @descripciones,Costo = @costo,PrecioVenta = @precioVenta,Stock = @stock, IdUsuario = @idUsuario WHERE Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("Id", producto.Id);
                    command.Parameters.AddWithValue("Descripciones", producto.Descripcion);
                    command.Parameters.AddWithValue("Costo", producto.Costo);
                    command.Parameters.AddWithValue("PrecioVenta", producto.PrecioVenta);
                    command.Parameters.AddWithValue("Stock", producto.Stock);
                    command.Parameters.AddWithValue("IdUsuario", producto.IdUsuario);
                    

                    command.ExecuteNonQuery();
                }
            } catch (Exception ex)
            {
                throw new Exception("No se puedo Modificar el producto",ex);
            }
        }


        //EliminarProducto
        public static bool EliminarProducto(int id)
        {
            try
            {
                using (SqlConnection connection = ConnectionADO.GetConnection())
                {
                    string query1 = "DELETE FROM ProductoVendido WHERE idProducto = @id";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("id", id);
                    string query2 = "DELETE FROM Producto WHERE id = @id";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("id", id);
                    
                    
                        
                    command1.ExecuteNonQuery();

                    return command2.ExecuteNonQuery() > 1;

                }
            } catch (Exception ex)
            {
                throw new Exception("No se puedo Eliminar el Producto", ex);
            }

        }

    }
}
