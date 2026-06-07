using backend.Database;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class ContactService(AppDbContext _dbContext, UserManager<Contact> _userManager) : IContactService
{
        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Contact?> GetContactByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Contact> CreateContactAsync(Contact contact, string password)
        {
            contact.UserName = contact.Email;
            var result = await _userManager.CreateAsync(contact, password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create contact: {errors}");
            }
            return contact;
        }

        public async Task<Contact> UpdateContactAsync(string id, Contact contact)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteContactAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await _dbContext.Categories
                .Include(c => c.Subcategories)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subcategories = c.Subcategories.Select(s => new SubcategoryDto
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                }).ToListAsync();
        }
}