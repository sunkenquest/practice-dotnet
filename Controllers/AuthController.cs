using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
    private readonly IEmailSender _emailSender;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAuthRepository authRepository, IConfiguration configuration, IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authRepository = authRepository;
        _configuration = configuration;
        _emailSender = emailSender;
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

    [HttpPost("/auth/logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            _authRepository.ClearTokenCookie();
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error occurred during logout.");
        }
    }

    [HttpGet("/auth/forgot-password")]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost("/auth/forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromForm]ForgotPasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = Url.Action("ResetPassword", "Auth", new { token, email = user.Email }, Request.Scheme);

        await _emailSender.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{resetLink}'>link</a>");

        return RedirectToAction("ForgotPasswordConfirmation");
    }

    [HttpGet("/auth/forgot-password-confirmation")]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        var model = new ResetPasswordDto { Token = token, Email = email };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return RedirectToAction("ResetPasswordConfirmation");
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction("ResetPasswordConfirmation");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
}
