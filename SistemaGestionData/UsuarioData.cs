using SistemaGestionEntities;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace SistemaGestionData
{
    public static class UsuarioData
    {
        private static string connectionString;

        static UsuarioData()
        {
            UsuarioData.connectionString = "Server=.;Database=coderhouse;Trusted_Connection=true;";
        }
        public static List<Usuario> ListarUsuarios()
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(UsuarioData.connectionString))
                {
                    List<Usuario> usuarios = new List<Usuario>();
                    string query = "SELECT * FROM Usuario";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nombre = reader["Nombre"].ToString();
                    usuario.Apellido = reader["Apellido"].ToString();
                    usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                    usuario.Password = reader["Contraseña"].ToString();
                    usuario.Email = reader["Mail"].ToString();
                    usuarios.Add(usuario);

                    }
                    return usuarios;
                }

            } catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
        

        public static Usuario ObtenerUsuario(int id)
        {
            try
            {
                Usuario? usuario = UsuarioData.ListarUsuarios().Find(p => p.Id == id);
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Usuario no Encontrado", ex);
            }




        }

        public static void CrearUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Usuario(Nombre,Apellido,NombreUsuario,Contraseña,Mail) values(@nombre,@apellido,@nombreUsuario,@password,@email)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("NombreUsuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("Contraseña", usuario.Password);
                    command.Parameters.AddWithValue("Mail", usuario.Email);
                    connection.Open();

                    command.ExecuteNonQuery();


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error, usuario no creado", ex);
            }
        }

  
        public static void ModificarUsuario(Usuario usuario, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario WHERE Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("Id", usuario.Id);
                    command.Parameters.AddWithValue("Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("NombreUsuario", usuario.NombreUsuario);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo Modificar el Usuario", ex);
            }
        }



        public static bool EliminarUsuario(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query1 = "DELETE FROM Venta WHERE IdUsuario = @id";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("id", id);
                    string query2 = "DELETE FROM Usuario WHERE id = @id";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("id", id);

                    connection.Open();

                    command1.ExecuteNonQuery();

                    return command2.ExecuteNonQuery() > 1;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo Eliminar el Usuario", ex);
            }

        }
     }
}
