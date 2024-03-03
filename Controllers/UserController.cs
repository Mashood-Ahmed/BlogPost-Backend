using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portfolio.API.Authorization;
using Portfolio.API.Helpers;
using Portfolio.API.Models.Domain;
using Portfolio.API.Models.Domain.Auth;
using Portfolio.API.Models.DTO;
using Portfolio.API.Repository.Interface;


namespace Portfolio.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        private IMapper mapper;

        private readonly AppSettings appSettings;
        
        public UserController(
            IUserRepository userRepository,
            IMapper mapper,
            IOptions<AppSettings> appSettings) 
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userRepository.GetAsyncAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid Id) 
        {
            var user = await userRepository.GetUserAsync(Id);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto model)
        {
        
            var response = await userRepository.Register(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            
            var response = await userRepository.Authenticate(model);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserById(Guid Id, UpdateUserRequestDto model)
        {
            var user = await userRepository.GetUserAsync(Id);
            if (user == null)
            {
                return NotFound("Invalid User Id.");
            }

            mapper.Map(model, user);

            await userRepository.UpdateAsync(user);

            return Ok(user);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var user = await userRepository.GetUserAsync(Id);
            if (user == null)
            {
                return NotFound("Invalid User Id.");
            }

            await userRepository.DeleteAsync(user);

            return Ok();
        }
    }
}
