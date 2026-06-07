namespace backend.Models;

public class ContactDetailsDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    public string BirthDate { get; set; } = string.Empty;
    
    public string Category { get; set; } = string.Empty;
    
    public string? Subcategory { get; set; } = null;
}