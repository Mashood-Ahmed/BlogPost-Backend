using System.ComponentModel;

namespace Portfolio.API.Models.DTO
{
    public class UpdateUserProfileDto
    {
        public string? Bio { get; set; }

        public string? Location { get; set; }

        public string? Contact { get; set; }

        public string? Website { get; set; }

        public Boolean? IsPublic { get; set; }

        public Guid UserId { get; set; }
    }
}
