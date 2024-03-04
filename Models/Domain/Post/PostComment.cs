using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfolio.API.Models.Domain.Post
{
    public class PostComment
    {
        public Guid Id { get; set; }

        public string CommentText { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public virtual _Post Post { get; set; }

    }
}
