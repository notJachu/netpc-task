using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Contact> Contacts { get; set; }
}