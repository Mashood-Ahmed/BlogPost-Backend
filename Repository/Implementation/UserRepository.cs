using AutoMapper;
using BCrypt.Net;
//using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Portfolio.API.Authorization;
using Portfolio.API.Data;
using Portfolio.API.Helpers;
using Portfolio.API.Models.Domain;
using Portfolio.API.Models.Domain.Auth;
using Portfolio.API.Models.DTO;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserRepository(
            ApplicationDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper
        )
        {
            this._dbContext = context;
            this._jwtUtils = jwtUtils;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAsyncAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid Id)
        {
            var user = await _dbContext.Users.FindAsync(Id);
            return user == null ? throw new KeyNotFoundException("User not Found") : user;
        }


        //Login
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
                throw new AppExceptions("Email is Invalid");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                throw new AppExceptions("Password is Invalid");

            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);

            return response;

        }

        public async Task<AuthenticateResponse> Register(RegisterRequestDto model)
        {
            if (_dbContext.Users.Any(x => x.Email == model.Email))
                throw new AppExceptions("Account with email already exists");

                    var user = _mapper.Map<User>(model);

                    user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

                    await _dbContext.Users.AddAsync(user);

                    var profile = new UserProfile
                    {
                        UserId = user.Id,
                        User = user
                    };

                    await _dbContext.Profiles.AddAsync(profile);

                try
                {

                    await _dbContext.SaveChangesAsync();

                    var response = _mapper.Map<AuthenticateResponse>(user);
                    response.Profile = _mapper.Map<UserProfile>(profile);
                    response.Token = _jwtUtils.GenerateToken(user);

                    return response;

                }catch (Exception e)
                {
                    throw;
                }
        }

        public async Task<User> UpdateAsync(User user)
        {

            if (!string.IsNullOrEmpty(user.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}
