using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Subcategory
{
    public int Id { get; set; }
    
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}