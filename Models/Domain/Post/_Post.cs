using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfolio.API.Models.Domain.Post
{
    public class _Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Descritpion { get; set; }

        public Boolean FollowersOnly { get; set; }

        public Boolean LimitContributions { get; set; }

        public int ContribuionLimit { get; set; }

        public string InitialDraft { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set;}

        public User User { get; set; }

        public Boolean Status { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Inspiration> Inspirations { get; set; }

        public ICollection<Contributor> Contributors { get; set; }

        public ICollection<Contribution> Contributions { get; set; }

        public ICollection<PostLike> Likes { get; set; }

        public ICollection<PostComment> Comments { get; set; }

    }
}
