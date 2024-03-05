using Portfolio.API.Models.Domain.Post;
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

        [JsonIgnore]
        public virtual UserProfile Profile { get; set; }

        [JsonIgnore]
        public virtual ICollection<Follower> Followers { get; set; }

        [JsonIgnore]
        public virtual ICollection<Follower> Following { get; set; }

        [JsonIgnore]
        public virtual ICollection<_Post> Posts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contribution> Contributions { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contributor> PostContributed { get; set; }

        [JsonIgnore]
        public virtual ICollection<PostLike> Likes { get; set; }

        [JsonIgnore]
        public virtual ICollection<PostComment> Comments { get; set; }


    }
}
