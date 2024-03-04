using Portfolio.API.Models.Domain;

namespace Portfolio.API.Repository.Interface
{
    public interface ICateogryRepository
    {
        Task<IEnumerable<Category>> GetAsyncAll();

        Task<Category> GetCategoryAsync(Guid id);

        Task<ICollection<Category>> MapCategoriesById(List<Guid> CategoryIds);

        Task<Category> CreateAsync(Category category);

        Task<Category> UpdateAsync(Category category);

        Task DeleteAsync(Category category);

    }
}
