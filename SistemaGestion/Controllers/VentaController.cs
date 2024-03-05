using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness;
using SistemaGestionEntities;
using System.Net;

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
                return base.BadRequest(new { error = ex.Message, HttpStatusCode.BadRequest });
            }

        }


        [HttpPost("{idUsuario}")]
        public IActionResult CargarVenta(int idUsuario,[FromBody]List<Producto> productos)
        {
            try
            {
                VentaBussiness.CargarVenta(idUsuario,productos);
                return base.Created(nameof(CargarVenta),new { mensaje = "Venta Cargada", HttpStatusCode.Created });

            }
            catch (Exception ex)
            {
                return base.Conflict( new { error = ex.Message, HttpStatusCode.Conflict });
            }
        }
    }
}
