using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using practice_dotnet.Repository.Interface;

namespace practice_dotnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthRepository _authRepository;
        public HomeController(IAuthRepository authRepositorye)
        {
            _authRepository = authRepositorye;
        }

        [Route("/home")]
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var hasToken = _authRepository.GetTokenFromCookie();

            if (!hasToken)
            {
                return RedirectToAction("UnauthorizedPage", "Error");
            }
            Response.Headers.Append("Cache-Control", "no-store");
            return View();
        }
    }
}
