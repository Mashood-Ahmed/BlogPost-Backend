using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Data;
using Portfolio.API.Models.Domain;
using Portfolio.API.Models.DTO;
using Portfolio.API.Repository.Implementation;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICateogryRepository categoryRepository;

        public CategoryController(ICateogryRepository cateogryRepository)
        {
            this.categoryRepository = cateogryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await categoryRepository.GetAsyncAll(); // Assuming you have a method in your repository to get all categories
            var response = categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            }).ToList();

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(Guid id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            return Ok(category);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestDto request)
        {

            //Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);
            

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryById(Guid id, CategoryRequestDto request )
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            if(category == null)
            {
                return NotFound();
            }

            category.Name = request.Name;
            category.UrlHandle = request.UrlHandle;

            await categoryRepository.UpdateAsync(category);

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryById(Guid id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound("Invalid Category Id.");
            }

            await categoryRepository.DeleteAsync(category);
                
            return Ok();
        }
    }
}
