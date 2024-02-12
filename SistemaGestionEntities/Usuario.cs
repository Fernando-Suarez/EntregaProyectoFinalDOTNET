using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionEntities
{
    public class Usuario
    {
        
        private int _id;
        private string _nombre;
        private string _apellido;
        private string _nombreUsuario;
        private string _password;
        private string _email;

        public Usuario() { }
        public Usuario(int id, string nombre, string apellido, string nombreUsuario, string password, string email)
        {
            _id = id;
            _nombre = nombre;
            _apellido = apellido;
            _nombreUsuario = nombreUsuario;
            _password = password;
            _email = email;
        }

        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string Password { get => _password; set => _password = value; }
        public string Email { get => _email; set => _email = value; }

        public override string ToString()
        {
            return ($"Id: {this.Id}, Nombre: {this.Nombre}, Apellido: {this.Apellido}, Nombre de Usuario: {this.NombreUsuario} , Email:{this.Email}");
        }
    }
}
