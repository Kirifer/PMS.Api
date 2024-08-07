using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pms.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("")]
        [Route("Home")]
        public IActionResult Index()
        {
            return Ok("Pms Api 1.0");
        }
    }
}
