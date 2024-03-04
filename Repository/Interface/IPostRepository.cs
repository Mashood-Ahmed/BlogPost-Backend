using Portfolio.API.Models.Domain;
using Portfolio.API.Models.Domain.Post;

namespace Portfolio.API.Repository.Interface
{
    public interface IPostRepository
    {
        Task<_Post> GetPostAsync(Guid PostId);

        Task<IEnumerable<_Post>> GetCategoryPostAsync(Guid CategoryId);

        Task<IEnumerable<_Post>> GetUserPostAsync(Guid UserId);

        Task<IEnumerable<_Post>> GetUserContributedPostAsync(Guid UserId);

        Task<_Post> CreateAsync(_Post post);

        Task<_Post> UpdateAsync(_Post post);

        Task DeleteAsync(_Post post);
    }
}
