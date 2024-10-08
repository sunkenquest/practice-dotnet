using Microsoft.AspNetCore.Mvc;

namespace practice_dotnet.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }

        [Route("Error/401")]
        public IActionResult UnauthorizedPage()
        {
            return View("Unauthorized");
        }

        [Route("Error/500")]
        public IActionResult GenericErrorPage()
        {
            return View("GenericError");
        }
    }
}
