using Portfolio.API.Models.Domain.Post;
using System.Text.Json.Serialization;

namespace Portfolio.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UrlHandle { get; set; }

        [JsonIgnore]
        public ICollection<_Post> Posts { get; set; }
    }
}
