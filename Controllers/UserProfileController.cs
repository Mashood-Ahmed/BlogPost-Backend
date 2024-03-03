using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portfolio.API.Authorization;
using Portfolio.API.Data;
using Portfolio.API.Helpers;
using Portfolio.API.Models.Domain;
using Portfolio.API.Models.DTO;
using Portfolio.API.Repository.Interface;
using System.Text;

namespace Portfolio.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository profileRepository;

        private readonly IUserRepository userRepository;

        private IMapper imapper;

        private readonly AppSettings appSettings;

        public UserProfileController(
            IUserProfileRepository profileRepository,
            IUserRepository userRepository,
            IMapper imapper,
            IOptions<AppSettings> appSettings
        )
        {
            this.profileRepository = profileRepository;
            this.userRepository = userRepository;
            this.imapper = imapper;
            this.appSettings = appSettings.Value;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(Guid Id)
        {
            var profile = await profileRepository.GetProfileAsync(Id);
            return Ok(profile);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetProfileByUserId(Guid Id)
        {
            var user = await userRepository.GetUserAsync(Id);

            if(user == null)
            {
                return NotFound("Invalid User Id");
            }

            var profile = await profileRepository.GetProfileByUserAsync(Id);

            return Ok(profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfileById(Guid Id, UpdateUserProfileDto model)
        {
            var profile = await profileRepository.GetProfileAsync(Id);

            if(profile == null)
            {
                return NotFound("Invalid Profile Id");
            }


            //Console.WriteLine(model.UserId);

            profile.Bio = model?.Bio;
            profile.Location = model?.Location;
            profile.Contact = model?.Contact;
            profile.Website = model?.Website;
            profile.IsPublic = model?.IsPublic;

            //imapper.Map(model, profile);


            await profileRepository.UpdateAsync(profile);



            return Ok(profile);
        }
    }
}
