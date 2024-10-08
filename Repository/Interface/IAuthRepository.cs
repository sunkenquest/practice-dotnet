using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using practice_dotnet.Models;
using practice_dotnet.Models.DTO.Auth;

namespace practice_dotnet.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<SignInResult> Login(LoginDtoInput model);
        Task<IdentityResult> Register(RegisterDto model);
        Task<string> GenerateAccessToken(string email);
        void SetTokenInCookie(string token, int expirationInMinutes);
        void ClearTokenCookie();
        bool GetTokenFromCookie();
    }
}
