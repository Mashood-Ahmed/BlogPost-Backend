using Portfolio.API.Models.Domain;

namespace Portfolio.API.Models.DTO
{
    public class CreatePostRequestDTO
    {
        public string Title { get; set; }

        public string Descritpion { get; set; }

        public Boolean FollowersOnly { get; set; }

        public Boolean LimitContributions { get; set; }

        public int ContribuionLimit { get; set; }

        public string Initialdraft { get; set; }

        //public IFormFile InitialDraft { get; set; }

        public List<Guid> Categories { get; set; }
    }
}
