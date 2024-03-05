using Microsoft.EntityFrameworkCore;
using Portfolio.API.Data;
using Portfolio.API.Migrations;
using Portfolio.API.Models.Domain.Post;
using Portfolio.API.Repository.Interface;

namespace Portfolio.API.Repository.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext dbContext;
        
        public PostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<_Post> GetPostAsync(Guid PostId)
        {
            var post = await dbContext.Posts
                .Include(p => p.Categories)
                .Include(p => p.Inspirations)
                .Include(p => p.Contributors)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == PostId);
            return post == null ? throw new KeyNotFoundException("Invalid Post Id. Post not found") : post;
        }

        public async Task<IEnumerable<_Post>> GetCategoryPostAsync(Guid CategoryId)
        {
            dbContext.ChangeTracker.LazyLoadingEnabled = false;

            return await dbContext.Posts
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Categories)
                .Where(p => p.Categories.Any(c => c.Id == CategoryId)).ToListAsync();
        }

        public async Task<IEnumerable<_Post>> GetUserPostAsync(Guid UserId)
        {

            dbContext.ChangeTracker.LazyLoadingEnabled = false;

            return await dbContext.Posts
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Categories)
                .Where(p => p.CreatedBy == UserId)
                .ToListAsync();
        }

        public async Task<IEnumerable<_Post>> GetUserContributedPostAsync(Guid UserId)
        {
            dbContext.ChangeTracker.LazyLoadingEnabled = false;

            return await dbContext.Posts
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Categories)
                .Where(p => p.Contributors.Any(c => c.UserId == UserId))
                .ToListAsync();
        }

        public async Task<_Post> CreateAsync(_Post post)
        {
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();

            return post;

        }

        public async Task<_Post> UpdateAsync(_Post post)
        {
            dbContext.Entry(post).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return post;
        }

        public async Task DeleteAsync(_Post post)
        {
            dbContext.Posts.Remove(post); 
            await dbContext.SaveChangesAsync();

        }

    }
}
