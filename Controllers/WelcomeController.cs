using Microsoft.AspNetCore.Mvc;

namespace practice_dotnet.Controllers
{
    public class WelcomeController : Controller
    {
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
