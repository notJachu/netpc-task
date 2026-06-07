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

        // Seed a default user for testing
        var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Contact>();
        var testUser = new Contact
        {
            Id = "11111111-1111-1111-1111-111111111111",
            UserName = "test@test.com",
            NormalizedUserName = "TEST@TEST.COM",
            Email = "test@test.com",
            NormalizedEmail = "TEST@TEST.COM",
            EmailConfirmed = true,
            FirstName = "Test",
            LastName = "User",
            Phone = "123123123",
            BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            CategoryId = 1,
            SubcategoryId = 1,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };
        testUser.PasswordHash = hasher.HashPassword(testUser, "Password123!");

        modelBuilder.Entity<Contact>().HasData(testUser);
    }
}