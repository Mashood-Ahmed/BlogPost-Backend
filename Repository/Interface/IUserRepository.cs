using Portfolio.API.Models.Domain;
using Portfolio.API.Models.Domain.Auth;
using Portfolio.API.Models.DTO;

namespace Portfolio.API.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsyncAll();

        Task<User> GetUserAsync(Guid id);

        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

        Task<AuthenticateResponse> Register(RegisterRequestDto user);

        Task<User> UpdateAsync(User user);

        Task DeleteAsync(User user);
    }
}
