using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using practice_dotnet.Models;
using practice_dotnet.Models.DTO.Auth;
using practice_dotnet.Repository.Interface;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAuthRepository authRepository, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authRepository = authRepository;
        _configuration = configuration;
    }

    [HttpPost("/auth/login")]
    public async Task<IActionResult> Login([FromForm] LoginDtoInput model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _authRepository.Login(model);

                if (result.Succeeded)
                {
                    var token = await _authRepository.GenerateAccessToken(model.Email);

                    _authRepository.SetTokenInCookie(token, 60);

                    return RedirectToAction("Index", "Home");
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

    [HttpPost("/auth/register")]
    public async Task<IActionResult> Register([FromForm] RegisterDto model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _authRepository.Register(model);

                if (result.Succeeded)
                {
                    return Ok(new { Success = true });
                }
                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error registering.");
        }
    }
}
