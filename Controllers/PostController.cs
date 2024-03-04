using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portfolio.API.Authorization;
using Portfolio.API.Data;
using Portfolio.API.Helpers;
using Portfolio.API.Models.Domain;
using Portfolio.API.Models.Domain.Post;
using Portfolio.API.Models.DTO;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository postRepository;
        private readonly ICateogryRepository cateogryRepository;
        private readonly IUserRepository userRepository;
        private readonly AppSettings appSettings;

        public PostController(
            IPostRepository postRepository, 
            ICateogryRepository cateogryRepository, 
            IUserRepository userRepository, 
            IOptions<AppSettings> appSettings
        )
        {
            this.postRepository = postRepository;
            this.cateogryRepository = cateogryRepository;
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid Id)
        {
            var post = await postRepository.GetPostAsync(Id);
            return Ok(post);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetPostsByCategory(Guid Id)
        {
            var category = await cateogryRepository.GetCategoryAsync(Id);

            if (category == null)
            {
                return NotFound("Invalid Category Id");
            }

            var posts = await postRepository.GetCategoryPostAsync(Id);
            return Ok(posts);
        }

        [HttpGet("creator/{id}")]
        public async Task<IActionResult> GetPostByCreator(Guid Id)
        {
            var user = await userRepository.GetUserAsync(Id);

            if (user == null)
            {
                return NotFound("Invalid User Id");
            }

            var posts = await postRepository.GetUserPostAsync(Id);
            return Ok(posts);
        }

        [HttpGet("contributor/{id}")]
        public async Task<IActionResult> GetContributedPosts(Guid Id)
        {
            var user= await userRepository.GetUserAsync(Id);

            if (user== null)
            {
                return NotFound("Invalid User Id");
            }

            var posts = await postRepository.GetUserContributedPostAsync(Id);
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostRequestDTO model)
        {
            ICollection<Category> categories = Enumerable.Empty<Category>().ToList();

            User current = (User)HttpContext.Items["User"];

            if(model.Categories != null || model.Categories.Count > 0)
            {
                categories = await cateogryRepository.MapCategoriesById(model.Categories);
            }

            var post = new _Post
            {
                Title = model.Title,
                Descritpion = model.Descritpion,
                FollowersOnly = model.FollowersOnly,
                LimitContributions = model.LimitContributions,
                ContribuionLimit = model.ContribuionLimit,
                InitialDraft = model.Initialdraft,
                CreatedBy = current.Id,
                CreatedOn = DateTime.Now,
                User = current,
                Categories = categories
            };

            await postRepository.CreateAsync(post);

            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostById(Guid Id)
        {
            var post = await postRepository.GetPostAsync(Id);

            if (post == null)
            {
                return NotFound("Invalid Post Id");
            }

            await postRepository.DeleteAsync(post);

            return Ok("Post Deleted Successfully");
        }


    }
}
