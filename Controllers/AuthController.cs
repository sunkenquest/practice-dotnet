using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using practice_dotnet.Models;
using practice_dotnet.Repository.Interface;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IAuthRepository _authRepository;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAuthRepository authRepository )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authRepository = authRepository;
    }

    [Route("/Auth/Login")]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("/Auth/Login")]
    public async Task<IActionResult> Login([FromForm] LoginDto model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _authRepository.Login(model);

                if (result.Succeeded)
                {
                    return Ok(new { Success = true });
                }
                return Unauthorized();
            }

            return BadRequest(ModelState);
        }
        catch (Exception ) {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error logging in.");
        }

    }
}
