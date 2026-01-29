using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
namespace SocialMedia.Data
{
    public class ApplicationDbContext : DbContext
    { 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<PostModel> Posts { get; set; } = null!;
        public DbSet<CommentModel> Comments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User -> Posts (cascade)
            modelBuilder.Entity<PostModel>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Comments (cascade)
            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Post -> Comments (NO cascade)
            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
