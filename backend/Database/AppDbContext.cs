using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class AppDbContext : IdentityDbContext<Contact>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Subcategory> Subcategories => Set<Subcategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Służbowy" },
            new Category { Id = 2, Name = "Prywatny" },
            new Category { Id = 3, Name = "Inny" }
        );

        // Seed Subcategories
        modelBuilder.Entity<Subcategory>().HasData(
            new Subcategory { Id = 1, Name = "Szef", CategoryId = 1 },
            new Subcategory { Id = 2, Name = "Klient", CategoryId = 1 }
        );
    }
}