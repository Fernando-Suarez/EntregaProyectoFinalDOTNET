using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness;
using SistemaGestionEntities;
using System.Net;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductoVendidoController : Controller
    {
        [HttpGet("{idUsuario}")]
        public IActionResult TraerProductosVendidos(int idUsuario)
        {
            try
            {
                List<ProductoVendido> productosVendidos = ProductoVendidoBussiness.TraerProductosVendidos(idUsuario);
                return base.Ok(new { mensaje = "ok", status = 200, productosVendidos });

            }
            catch (Exception ex)
            {
                return base.BadRequest(new { error = ex.Message, status = HttpStatusCode.BadRequest });
            }

        }

    }
}
