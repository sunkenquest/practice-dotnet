using Microsoft.AspNetCore.Identity;
using practice_dotnet.Models;
using practice_dotnet.Models.DTO.Auth;

namespace practice_dotnet.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<SignInResult> Login(LoginDto model);
        Task<IdentityResult> Register(RegisterDto model);
    }
}
