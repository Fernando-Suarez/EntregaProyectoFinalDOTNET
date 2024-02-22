using SistemaGestionEntities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SistemaGestionData
{
    public static class VentaData
    {
        private static string connectionString;

        static VentaData()
        {
            VentaData.connectionString = "Server=.;Database=coderhouse;Trusted_Connection=true;";
        }
        public static List<Venta> ListarVentas()
        {
            try
            {
                List<Venta> ventas = new List<Venta>();
                string query = "SELECT Id,Comentarios,IdUsuario FROM Venta;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Venta venta = new Venta();
                        venta.Id = Convert.ToInt32(reader["Id"]);
                        venta.Comentarios = reader["Comentarios"].ToString();

                        venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                        ventas.Add(venta);


                    }
                }
                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception("Ventas no Encontradas", ex);
            }
        }

        public static Venta ObtenerVenta(int id)
        {
            try
            {
                Venta? venta = VentaData.ListarVentas().Find(v => v.Id == id);
                return venta;
            }
            catch (Exception ex)
            {
                throw new Exception("Venta no Encontrada", ex);
            }

        }

        public static List<Venta> ObtenerVentaPorIdUsuario(int idUsuario)
        {
            try
            {
                List<Venta> ventas = VentaData.ListarVentas();
                List<Venta> ventasUsuario = new List<Venta>();
                foreach (Venta v in ventas)
                {
                    if(v.IdUsuario == idUsuario)
                    {
                        ventasUsuario.Add(v);
                    }
                }
                return ventasUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Venta no Encontrada", ex);
            }
        }



        public static void CrearVenta(Venta venta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Venta(Comentarios,IdUsuario) values(@comentarios,@idUsuario)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("comentarios", venta.Comentarios);
                    command.Parameters.AddWithValue("idUsuario", venta.IdUsuario);
                    connection.Open();

                    command.ExecuteNonQuery();


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error, Venta no Creada", ex);
            }
        }

        //ModificarVenta
        public static void ModificarVenta(int id, Venta venta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Venta SET Comentarios = @comentarios, IdUsuario = @idUsuario WHERE Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("id", venta.Id);
                    command.Parameters.AddWithValue("comentarios", venta.Comentarios);
                    command.Parameters.AddWithValue("idUsuario", venta.IdUsuario);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puedo Modificar la venta", ex);
            }
        }


        //EliminarVenta
        public static bool EliminarVenta(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query1 = "DELETE FROM ProductoVendido WHERE IdVenta = @id";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("id", id);
                    string query2 = "DELETE FROM Venta WHERE id = @id";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("id", id);

                    connection.Open();

                    command1.ExecuteNonQuery();

                    return command2.ExecuteNonQuery() > 1;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se puedo Eliminar el Producto", ex);
            }
        }

        public static void CargarVenta( int idUsuario , List<Producto> productos)
        {
            //Cargar Venta: Recibe una lista de productos y el numero de IdUsuario de quien la efectuó, primero cargar una nueva venta en la base de datos, luego debe cargar los productos recibidos en la base de ProductosVendidos uno por uno por un lado, y descontar el stock en la base de productos por el otro.
            
            
            Venta venta = new Venta();

            List<string> listaDescripciones = productos.Select(p => p.Descripcion).ToList();

            string comentarios = string.Join('-', listaDescripciones);

            venta.Comentarios = comentarios;
            venta.IdUsuario = idUsuario;
            

            VentaData.CrearVenta(venta);
            int ventaId = VentaData.ListarVentas().Count;

            ProductoVendidoData.MarcarProductoVendido(ventaId, productos);
            ProductoVendidoData.ActualizarStockProductoVendido(productos);


        }

    }
}
