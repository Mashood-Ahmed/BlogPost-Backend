using System.Text.Json.Serialization;

namespace Portfolio.API.Models.Domain
{
    public class Follower
    {
        public Guid Id { get; set; }

        public Boolean Status { get; set; }

        public Guid Follower_Id { get; set; }

        public Guid Following_Id { get; set; }

        [JsonIgnore]
        public User _Follower { get; set; } = default!;

        public User _Following { get; set; } = default!;
    }
}
