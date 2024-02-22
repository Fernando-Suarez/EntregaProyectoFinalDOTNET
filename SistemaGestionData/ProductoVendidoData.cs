using SistemaGestionEntities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionData
{
    public static class ProductoVendidoData
    {
        private static string connectionString;

        static ProductoVendidoData()
        {
            ProductoVendidoData.connectionString = "Server=.;Database=coderhouse;Trusted_Connection=true;";
        }
        public static List<ProductoVendido> ListarProductosVendidos()
        {
            try 
            {
                List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
                string query = "SELECT Id,Stock,IdProducto,IdVenta FROM ProductoVendido;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductoVendido productoVendido = new ProductoVendido();
                        productoVendido.Id = Convert.ToInt32(reader["Id"]);
                        productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                        productoVendido.Stock = Convert.ToInt32(reader["Stock"]);
                        productoVendido.IdVenta = Convert.ToInt32(reader["IdVenta"]);

                        productosVendidos.Add(productoVendido);


                    }
                }
                return productosVendidos;
            } catch (Exception ex)
            {
                throw new Exception("Producto Vendidos no Encontrados", ex);
            }
        }

        public static ProductoVendido ObtenerProductoVendido(int id)
        {
            try
            {
                ProductoVendido? productoVendido = ProductoVendidoData.ListarProductosVendidos().Find(p => p.Id == id);
                
                 
                    return productoVendido;

            }catch (Exception ex)
            {

            throw new Exception("Producto Vendido no Encontrado",ex);
            }



        }

        public static List<ProductoVendido> TraerProductosVendidos(int idUsuario)
        {
            try
            {
                List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();
                List<Venta> listaIdVenta = VentaData.ObtenerVentaPorIdUsuario(idUsuario);

                foreach(Venta venta in listaIdVenta)
                {
                    listaProductosVendidos.Add(ProductoVendidoData.ObtenerProductoVendido(venta.Id));
                }
                //// con el idUsuario sacar el id de venta de la tabla Venta, crear una lista de productosVendidos con los id de venta y retornar la lista de productos vendidos
                return listaProductosVendidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Productos Vendidos Al Usuario no Encontrados", ex);
            }
        }



        public static void CrearProductoVendido(ProductoVendido producto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ProductoVendido(Stock,IdProducto,IdVenta) values(@Stock,@IdProducto,@IdVenta)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("Stock", producto.Stock);
                    command.Parameters.AddWithValue("IdProducto", producto.IdProducto);
                    command.Parameters.AddWithValue("IdVenta", producto.IdVenta);
                    connection.Open();

                    command.ExecuteNonQuery();


                }
            } catch (Exception ex)
            {
                throw new Exception("No se pudo crear el producto vendido", ex);
            }
        }


        
        public static void ModificarProductoVendido(int id , ProductoVendido productoVendido)
        {
           try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ProductoVendido SET Stock = @stock, IdProducto = @idProducto, IdVenta = @idVenta WHERE Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("Id", productoVendido.Id);
                    command.Parameters.AddWithValue("Stock", productoVendido.Stock);
                    command.Parameters.AddWithValue("IdProducto", productoVendido.IdProducto);
                    command.Parameters.AddWithValue("IdVenta", productoVendido.IdVenta);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo modificar el producto vendido", ex);
            }
        }

  
        public static bool EliminarProductoVendido(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ProductoVendido WHERE id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("id", id);
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;

                }
            }catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el producto vendido", ex);
            }

        }

        public static void MarcarProductoVendido(int idVenta, List<Producto> productos)
        {
            try
            {
                foreach (Producto producto in productos)
                {
                    ProductoVendido productoVendido = new ProductoVendido();
                    productoVendido.IdVenta = idVenta;
                    productoVendido.IdProducto = producto.Id;
                    productoVendido.Stock = producto.Stock;
                    ProductoVendidoData.CrearProductoVendido(productoVendido);
                }

            }
            catch(Exception ex) 
            {
                throw new Exception("No se pudo cargar el producto vendido", ex);
            }
        }


        public static void ActualizarStockProductoVendido(List<Producto> productos)
        {
            try
            {
                foreach (Producto producto in productos)
                {
                    Producto productoActual = ProductoData.ObtenerProducto(producto.Id);
                    productoActual.Stock -= producto.Stock;
                    ProductoData.ModificarProducto( productoActual);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo actualizar el  stock del producto", ex);
            }
        }

    }
}
