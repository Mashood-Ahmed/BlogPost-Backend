using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models.Domain;
using Portfolio.API.Models.Domain.Post;

namespace Portfolio.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<_Post> Posts { get; set; }
        public DbSet<Inspiration> Inspirations { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostComment> PostComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follower>()
               .HasOne(f => f._Follower)
               .WithMany(u => u.Followers)
               .HasForeignKey(f => f.Follower_Id)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follower>()
                .HasOne(f => f._Following)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.Following_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contribution>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contributions)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contributor>()
                .HasOne(c => c.User)
                .WithMany(u => u.PostContributed)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostComment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostLike>()
                .HasOne(p => p.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }

    }
}
