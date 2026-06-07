using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Category
{
    public int Id { get; set; }
    
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
}