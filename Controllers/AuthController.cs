using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAuthRepository authRepository )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authRepository = authRepository;
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

    [HttpPost("/Auth/Register")]
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

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (userId == null || token == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded ? View("ConfirmEmail") : View("Error");
    }

}
