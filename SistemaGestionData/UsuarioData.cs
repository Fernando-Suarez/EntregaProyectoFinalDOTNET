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
        public static List<Usuario> ListarUsuarios()
        {
            try
            {
                using (SqlConnection connection = ConnectionADO.GetConnection())
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
                Usuario? usuario = UsuarioData.ListarUsuarios().Find(u => u.Id == id);
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Usuario no Encontrado", ex);
            }

        }

        public static Usuario TraerUsuario(string nombreUsuario)
        {
            try
            {
                Usuario? usuario = UsuarioData.ListarUsuarios().Find(u => u.NombreUsuario == nombreUsuario);
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Usuario no Encontrado", ex);
            }
        }

        public static Usuario InicioSesion(string nombreUsuario, string password)
        {
            try
            {
                Usuario? usuario = UsuarioData.ListarUsuarios().Find(u => u.NombreUsuario == nombreUsuario && u.Password == password);
                if (usuario == null)
                {
                    throw new Exception("Usuario o Password incorrectos");
                }
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
                using (SqlConnection connection = ConnectionADO.GetConnection())
                {
                    string query = "INSERT INTO Usuario(Nombre,Apellido,NombreUsuario,Contraseña,Mail) values(@nombre,@apellido,@nombreUsuario,@password,@email)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("password", usuario.Password);
                    command.Parameters.AddWithValue("email", usuario.Email);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error, usuario no creado", ex);
            }
        }

  
        public static void ModificarUsuario(int id, Usuario usuario)
        {
            try
            {
                using (SqlConnection connection = ConnectionADO.GetConnection())
                {
                    string query = "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @password, Mail = @email WHERE Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("id", usuario.Id);
                    command.Parameters.AddWithValue("nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("password", usuario.Password);
                    command.Parameters.AddWithValue("email", usuario.Email);
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
                using (SqlConnection connection = ConnectionADO.GetConnection())
                {

                    string query1 = "DELETE FROM Venta WHERE IdUsuario = @id";
                    SqlCommand command1 = new SqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("id", id);
                    string query2 = "DELETE FROM Usuario WHERE id = @id";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("id", id);
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
