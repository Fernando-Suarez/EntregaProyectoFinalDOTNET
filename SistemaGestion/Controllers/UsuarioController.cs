using SistemaGestionData;
using SistemaGestionBussiness;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionEntities;
using System.Net;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsuarioController : Controller
    {


        [HttpGet("/api/Usuario/{nombreUsuario}/{password}")]
        public IActionResult InicioSesion(string nombreUsuario, string password)
        {
            try
            {
                Usuario usuario = UsuarioBussiness.InicioSesion(nombreUsuario, password);
                return base.Ok(new { mensaje = "" , StatusCode = 200, usuario });
            }catch (Exception ex)
            {
                return base.Conflict(new { error = ex.Message, status = HttpStatusCode.Conflict });
            }
        }
       
    
        [HttpGet("{nombreUsuario}")]
        public IActionResult TraerUsuario(string nombreUsuario)
        {
            try
            {
                Usuario usuario = UsuarioBussiness.TraerUsuario(nombreUsuario);

                return base.Ok(new { mensaje = "ok", StatusCode = 200, usuario });
            }
            catch (Exception ex) 
            {
                throw new Exception("No Se Pudo Obtener El Usuario", ex);
            }
            
        }


        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {
            try
            {
                UsuarioBussiness.CrearUsuario(usuario);
                return base.Created(nameof(CrearUsuario),new { mensaje = "Usuario Creado", status = 201, usuario });

            }catch (Exception ex)
            {
                throw new Exception("No Se Pudo Crear El Usuario",ex);
            }
        }
            
           

        [HttpPut("{id}")] 

        public IActionResult ModificarUsuario(int id, [FromBody] Usuario usuario)
        {
            try
            {
                UsuarioBussiness.ModificarUsuario(id, usuario);
                return base.Ok(new { mensaje = "Usuario Modificado", status = 200 });
            } catch (Exception ex)
            {
                throw new Exception("No Se Pudo Modificar el Usuario", ex);
            }

        }
 
    }
}
