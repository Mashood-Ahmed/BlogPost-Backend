namespace Portfolio.API.Models.Domain.Auth
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public UserProfile Profile { get; set; }
   
        public string Token { get; set; }
    }
}
