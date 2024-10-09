using System.ComponentModel.DataAnnotations;

namespace practice_dotnet.Models.DTO.Auth
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
