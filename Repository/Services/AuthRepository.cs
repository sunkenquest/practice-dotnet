using Microsoft.AspNetCore.Identity;
using practice_dotnet.Models;
using practice_dotnet.Repository.Interface;

namespace practice_dotnet.Repository.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthRepository(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Login(LoginDto model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

        }
    }
}
