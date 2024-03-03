using Portfolio.API.Models.Domain;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Portfolio.API;

public class UserProfile
{

    public Guid Id { get; set; }

    public string? Bio { get; set; }

    public string? Location { get; set; }

    public string? Contact { get; set; }

    public string? Website { get; set; }

    public Boolean? IsPublic { get; set; }

    public Guid UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; } = default!;


}
