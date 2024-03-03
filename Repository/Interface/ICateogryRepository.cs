using Portfolio.API.Models.Domain;

namespace Portfolio.API.Repository.Interface
{
    public interface ICateogryRepository
    {
        Task<IEnumerable<Category>> GetAsyncAll();

        Task<Category> GetCategoryAsync(Guid id);

        Task<Category> CreateAsync(Category category);

        Task<Category> UpdateAsync(Category category);

        Task DeleteAsync(Category category);

    }
}
