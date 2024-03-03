namespace Portfolio.API.Repository.Interface
{
    public interface IUserProfileRepository
    {

        Task<UserProfile> GetProfileAsync(Guid Id);

        Task<UserProfile> GetProfileByUserAsync(Guid Id);

        Task<UserProfile> UpdateAsync(UserProfile profile);

        Task DeleteAsync(UserProfile profile);

    }
}
