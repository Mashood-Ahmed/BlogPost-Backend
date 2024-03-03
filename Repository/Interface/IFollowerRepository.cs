using Portfolio.API.Models.Domain;

namespace Portfolio.API.Repository.Interface
{
    public interface IFollowerRepository
    {
        Task<Follower> GetFollowerAsync(Guid Id);

        Task<Follower> GetFollowerByUserAsync(Guid FollowerId, Guid FollowingId);

        Task<IEnumerable<Follower>> GetUserNonFollowerAsync(Guid Id);

        Task<IEnumerable<Follower>> GetUserFollowerAsync(Guid Id);

        Task<IEnumerable<Follower>> GetUserFollowingAsync(Guid Id);

        Task<Follower> CreateAsync(Follower follower);

        Task<Follower> UpdateAsync(Follower follower);

        Task DeleteAsync(Follower follower);

    }
}
