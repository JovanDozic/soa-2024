using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<Core.Domain.Blog> Blogs {  get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");


        modelBuilder.Entity<Core.Domain.Blog>().ToTable("Blogs");
        modelBuilder.Entity<Core.Domain.Blog>()
            .Property(item => item.JsonObject).HasColumnType("jsonb");
    }
}