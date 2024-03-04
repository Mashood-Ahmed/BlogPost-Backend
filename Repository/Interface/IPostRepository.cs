using Portfolio.API.Models.Domain.Post;

namespace Portfolio.API.Repository.Interface
{
    public interface IPostRepository
    {
        Task<_Post> GetPostAsync(Guid Id);

        Task<_Post> CreateAsync(_Post post);

        Task<_Post> UpdateAsync(_Post post);

        Task DeleteAsync(_Post post);
    }
}
