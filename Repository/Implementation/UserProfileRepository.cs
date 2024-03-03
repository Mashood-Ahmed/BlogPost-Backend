using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.API.Authorization;
using Portfolio.API.Data;
using Portfolio.API.Models.DTO;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Repository.Implementation
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _imapper;

        public UserProfileRepository
        (
            ApplicationDbContext dbcontext,
            IJwtUtils jwtUtils,
            IMapper imapper
        )
        {
            this._dbContext = dbcontext;
            this._jwtUtils = jwtUtils;
            this._imapper = imapper;
        }

        public async Task<UserProfile> GetProfileAsync(Guid Id){ 
            
            var profile = await this._dbContext.Profiles.FindAsync(Id);
            return profile == null ? throw new KeyNotFoundException("User Profile not Found") : profile;
        }

        public async Task<UserProfile> GetProfileByUserAsync(Guid Id)
        {
            var profile = await this._dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == Id);
            return profile == null ? throw new KeyNotFoundException("Profile not Found") : profile;
        }

        public async Task<UserProfile> UpdateAsync(UserProfile profile)
        {
            _dbContext.Entry(profile).State = EntityState.Modified;
            await this._dbContext.SaveChangesAsync();

            return profile;
        }

        public async Task DeleteAsync(UserProfile profile)
        {
            _dbContext.Profiles.Remove(profile);
            await this._dbContext.SaveChangesAsync();
        }

    }
}
