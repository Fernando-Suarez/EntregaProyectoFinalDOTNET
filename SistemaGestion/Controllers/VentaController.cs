using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness;
using SistemaGestionEntities;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VentaController : Controller
    {

        [HttpGet("{idUsuario}")]
        public IActionResult ObtenerVentaPorIdUsuario(int idUsuario)
        {
            try
            {
                List<Venta> ventas = VentaBussiness.ObtenerVentaPorIdUsuario(idUsuario);

                return base.Ok(new { mensaje = "ok", StatusCode = 200, ventas });
            }
            catch (Exception ex)
            {
                throw new Exception("No Se Pudo Obtener La Venta", ex);
            }

        }


        [HttpPost("{idUsuario}")]
        public IActionResult CargarVenta(int idUsuario,[FromBody]List<Producto> productos)
        {
            try
            {
                VentaBussiness.CargarVenta(idUsuario,productos);
                return base.Ok(new { mensaje = "Venta Cargada", status = 201 });

            }
            catch (Exception ex)
            {
                throw new Exception("No Se Pudo Cargar La Venta", ex);
            }
        }
    }
}
