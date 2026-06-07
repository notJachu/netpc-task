using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class Contact : IdentityUser
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int? SubcategoryId { get; set; }
    public Subcategory? Subcategory { get; set; }

    [MaxLength(255)]
    public string? CustomSubcategory { get; set; }
}