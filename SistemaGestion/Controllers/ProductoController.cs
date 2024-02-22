using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness;
using SistemaGestionEntities;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductoController : Controller
    {


        [HttpGet("{idUsuario}")]
        public IActionResult ProductosPorIdUsuario(int idUsuario)
        {
            try
            {
                List<Producto> productos = ProductoBussiness.ProductosPorIdUsuario(idUsuario);

                return base.Ok(new { mensaje = "ok", StatusCode = 200, productos});
            }
            catch (Exception ex)
            {
                throw new Exception("No Se Pudo Obtener El listado de Productos", ex);
            }

        }


        [HttpPost]
        public IActionResult CrearProducto(Producto producto)
        {
            try
            {
                ProductoBussiness.CrearProducto(producto);
                return base.Ok(new { mensaje = "Producto Creado", status = 201 });

            }
            catch (Exception ex)
            {
                throw new Exception("No Se Pudo Crear El Producto", ex);
            }
        }



        [HttpPut()]

        public IActionResult ModificarProducto( [FromBody] Producto producto)
        {
            try
            {
                ProductoBussiness.ModificarProducto( producto);
                return base.Ok(new { mensaje = "Producto Modificado", status = 200 });
            }
            catch (Exception ex)
            {
                throw new Exception("No Se Pudo Modificar el Producto", ex);
            }

        }

        [HttpDelete("{idProducto}")]
        public IActionResult EliminarProducto(int idProducto)
        {
            try
            {
                ProductoBussiness.EliminarProducto(idProducto);
                return base.Ok(new { mensaje = "Ok", status = 200 });
            }
            catch (Exception ex)
            {
                throw new Exception("No Se Pudo Eliminar El Producto", ex);
            }
            
        }
    }
}
