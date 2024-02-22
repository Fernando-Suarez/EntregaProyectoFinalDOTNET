using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness;
using SistemaGestionEntities;

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
                throw new Exception("No Se Pudo Obtener El Listado de Productos Vendidos", ex);
            }

        }

    }
}
