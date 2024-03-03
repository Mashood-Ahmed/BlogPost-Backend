using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portfolio.API.Authorization;
using Portfolio.API.Helpers;
using Portfolio.API.Models.Domain;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly IFollowerRepository followerRepository;

        private readonly IUserRepository userRepository;

        private readonly AppSettings appSettings;

        public FollowerController(
            IFollowerRepository followerRepository,
            IUserRepository userRepository,
            IOptions<AppSettings>  appSettings
        )
        {
            this.followerRepository = followerRepository;
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollowerById(Guid Id)
        {

            var follower = await followerRepository.GetUserFollowingAsync(Id);

            return Ok(follower);

        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetFollowRequests()
        {

            User current = (User)HttpContext.Items["User"];

            var followers = await followerRepository.GetUserNonFollowerAsync(current.Id);

            return Ok(followers);

        }

        [HttpGet("followings/{id}")]
        public async Task<IActionResult> GetFollowersByUserId(Guid Id)
        {
            var user = await userRepository.GetUserAsync(Id);

            if(user == null)
            {
                return NotFound("Invalid User Id");
            }

            var followers = await followerRepository.GetUserFollowerAsync(Id);

            return Ok(followers);

        }

        [HttpGet("followers/{id}")]
        public async Task<IActionResult> GetFollowingByUserId(Guid Id)
        {
            var user = await userRepository.GetUserAsync(Id);

            if (user == null)
            {
                return NotFound("Invalid User Id");
            }

            var followers = await followerRepository.GetUserFollowingAsync(Id);

            return Ok(followers);

        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SendFollowRequest(Guid Id)
        {
            User current = (User)HttpContext.Items["User"];

            var Isfollower = await followerRepository.GetFollowerByUserAsync(current.Id, Id);

            if (Isfollower != null)
            {
                return Ok("Already Sent Request.");
            }

            var user= await userRepository.GetUserAsync(Id);

            if (user == null)
            {
                return NotFound("Invalid User Id");
            }

            var follower = new Follower
            {
                Status = false,
                Follower_Id = current.Id, //current user id;
                Following_Id = user.Id, //user id to follow
                _Follower = current, // current user
                _Following = user //user to follow
            };

            await followerRepository.CreateAsync(follower);

            return Ok(follower);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AcceptFollowRequest(Guid Id)
        {
            var follower = await followerRepository.GetFollowerAsync(Id);

            if (follower == null)
            {
                return NotFound("Invalid Follower Id");
            }

            follower.Status = true;

            await followerRepository.UpdateAsync(follower);

            return Ok(follower);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelRemoveFollowerRequest(Guid Id)
        {
            var follower = await followerRepository.GetFollowerAsync(Id);

            if (follower == null)
            {
                return NotFound("Invalid Follower Id");
            }

            await followerRepository.DeleteAsync(follower);

            return Ok("Request/Follower Removed");

        }


    }
}
