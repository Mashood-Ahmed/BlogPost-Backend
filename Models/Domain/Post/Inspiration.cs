using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfolio.API.Models.Domain.Post
{
    public class Inspiration
    {
        public Guid Id { get; set; }

        public string FileUrl { get; set; }

        public Guid PostId { get; set; }

        public virtual _Post Post { get; set; }
    }
}
