using Microsoft.EntityFrameworkCore;
using Portfolio.API.Data;
using Portfolio.API.Migrations;
using Portfolio.API.Models.Domain;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Repository.Implementation
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public FollowerRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Follower> GetFollowerAsync(Guid Id)
        {
            var follower = await _dbcontext.Followers.FindAsync(Id);
            return follower == null ? throw new KeyNotFoundException("Invalid Follower Id. Follower does not exist.") : follower;
        }

        //Get request where current user has already sent a request to follower
        public async Task<Follower> GetFollowerByUserAsync(Guid FollowerId, Guid FollowingId)
        {
            var follower = await _dbcontext.Followers.FirstOrDefaultAsync(f => f.Follower_Id == FollowerId && f.Following_Id == FollowingId);
            return follower;
        }

        //Get all follow requests
        public async Task<IEnumerable<Follower>> GetUserNonFollowerAsync(Guid Id)
        {
            var followers = await _dbcontext.Followers.Where(f => f.Following_Id == Id && f.Status == false).ToListAsync();
            return followers;
        }

        //Get all accounts that follows this user.
        public async Task<IEnumerable<Follower>> GetUserFollowerAsync(Guid Id)
        {
            var followers = await _dbcontext.Followers.Where(f => f.Following_Id == Id && f.Status == true).ToListAsync();
            return followers;
        }
        //Get all accounts that this user followers.
        public async Task<IEnumerable<Follower>> GetUserFollowingAsync(Guid Id)
        {
            var followers = await _dbcontext.Followers.Where(f => f.Follower_Id == Id && f.Status == true).ToListAsync();
            return followers;
        }

        public async Task<Follower> CreateAsync(Follower follower)
        {
            await _dbcontext.Followers.AddAsync(follower);
            await _dbcontext.SaveChangesAsync();

            return follower;
        }


        public async Task<Follower> UpdateAsync(Follower follower)
        {

            _dbcontext.Entry(follower).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();

            return follower;
        }

        public async Task DeleteAsync(Follower follower)
        {
            _dbcontext.Followers.Remove(follower);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
