using System.Collections;
using System.Text.Json.Serialization;

namespace Portfolio.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public Boolean IsAdmin { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        //Navigation Properties
        public UserProfile Profile { get; set; }

        public ICollection<Follower> Followers { get; set; }

        public ICollection<Follower> Following { get; set; }


    }
}
