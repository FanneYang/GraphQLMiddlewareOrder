
using Microsoft.EntityFrameworkCore;

namespace FacebookPost
{
    public class FacebookContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Interaction> Interactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=facebook.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<Post>(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany<Interaction>(u => u.Interactions)
                .WithOne(i => i.InteractionUser)
                .HasForeignKey(i => i.UserId);

            modelBuilder.Entity<Post>()
                .HasMany<Interaction>(p => p.Interactions)
                .WithOne(i => i.Post)
                .HasForeignKey(i => i.PostId);
        }
    }
}
