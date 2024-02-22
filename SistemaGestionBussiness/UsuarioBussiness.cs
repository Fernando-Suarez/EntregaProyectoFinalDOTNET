using SistemaGestionEntities;
using SistemaGestionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness
{
    public static class UsuarioBussiness
    {
        public static List<Usuario> ListarUsuarios()
        {
            return UsuarioData.ListarUsuarios();
        }

        public static Usuario InicioSesion(string nombreUsuario, string password)
        {
            return UsuarioData.InicioSesion( nombreUsuario,  password);
        }

        public static Usuario ObtenerUsuario(int id)
        {
            return UsuarioData.ObtenerUsuario(id);
        }

        public static Usuario TraerUsuario(string nombreUsuario)
        {
            return UsuarioData.TraerUsuario(nombreUsuario);
        }

        public static void CrearUsuario(Usuario usuario)
        {
            UsuarioData.CrearUsuario(usuario);
        }

        public static void ModificarUsuario(int id, Usuario usuario)
        {
            UsuarioData.ModificarUsuario(id, usuario);
        }

        public static bool EliminarUsuario(int id)
        {
            return UsuarioData.EliminarUsuario(id);
        }
    }
}
