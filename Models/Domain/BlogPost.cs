namespace Portfolio.API.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public string Author { get; set; }

        public bool isVisible { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
