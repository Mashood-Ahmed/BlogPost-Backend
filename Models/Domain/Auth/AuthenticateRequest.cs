using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Models.Domain.Auth
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
