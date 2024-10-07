using Microsoft.AspNetCore.Identity;
using practice_dotnet.Models;
using practice_dotnet.Models.DTO.Auth;
using practice_dotnet.Repository.Interface;

namespace practice_dotnet.Repository.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthRepository(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<SignInResult> Login(LoginDto model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            return await _userManager.CreateAsync(user, model.Password);
        }
    }
}
