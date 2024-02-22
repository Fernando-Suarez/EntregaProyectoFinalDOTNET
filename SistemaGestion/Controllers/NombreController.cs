using Microsoft.AspNetCore.Mvc;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NombreController : Controller
    {
        [HttpGet]
        public string ObtenerNombre()
        {
            return "Fernando Suarez";
        }
    }
}
