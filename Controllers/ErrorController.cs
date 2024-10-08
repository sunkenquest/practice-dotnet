using Microsoft.AspNetCore.Mvc;

namespace practice_dotnet.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        [HttpGet]
        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }

        [Route("Error/401")]
        [HttpGet]
        public IActionResult UnauthorizedPage()
        {
            return View("Unauthorized");
        }

        [Route("Error/500")]
        [HttpGet]
        public IActionResult GenericErrorPage()
        {
            return View("GenericError");
        }
    }
}
