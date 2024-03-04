using Microsoft.EntityFrameworkCore;
using Portfolio.API.Data;
using Portfolio.API.Models.Domain;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Repository.Implementation
{
    public class CategoryRepository : ICateogryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        public async Task<IEnumerable<Category>> GetAsyncAll()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            var category =  await dbContext.Categories.FindAsync(id);
            return category == null ? throw new KeyNotFoundException("Category Not Found") : category;
        }

        public async Task<ICollection<Category>> MapCategoriesById(List<Guid> CategoryIds)
        {
            var categories = await dbContext.Categories.Where(c => CategoryIds.Contains(c.Id)).ToListAsync();
            return categories;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);

            await dbContext.SaveChangesAsync();

            return category;    

        }

        public async Task<Category> UpdateAsync(Category category)
        {
            dbContext.Entry(category).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task DeleteAsync(Category category)
        {
            dbContext.Categories.Remove(category);

            await dbContext.SaveChangesAsync();
            
        }
    }
}
