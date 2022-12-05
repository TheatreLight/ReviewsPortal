using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Models;

namespace ReviewsPortal.Data
{
    public class ReviewsPortalContext : DbContext
    {
        public ReviewsPortalContext(DbContextOptions<ReviewsPortalContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasOne(r => r.User).WithMany(u => u.Reviews);
            modelBuilder.Entity<Review>().HasOne(r => r.Group).WithMany(g => g.Reviews);
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>().HasOne(c => c.Review).WithMany(u => u.Comments).OnDelete(DeleteBehavior.NoAction);
        }

    }
}
