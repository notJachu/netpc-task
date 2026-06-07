namespace backend.Models;

public class ContactUpdateCreateDto
{
    public  string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BirthDate { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int? SubcategoryId { get; set; }
    public string? CustomSubcategory { get; set; }
}