using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models.Domain;

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

            base.OnModelCreating(modelBuilder);
        }

    }
}
