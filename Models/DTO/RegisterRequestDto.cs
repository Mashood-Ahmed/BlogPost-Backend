using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool isAdmin { get; set; }

    }
}
