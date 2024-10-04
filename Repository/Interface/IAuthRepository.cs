using Microsoft.AspNetCore.Identity;
using practice_dotnet.Models;

namespace practice_dotnet.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<SignInResult> Login(LoginDto model);

    }
}
