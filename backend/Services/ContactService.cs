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
        
        /// <summary>
        /// Gets a contact by ID, including their category and subcategory information.
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>Contact on success, null on fail</returns>
        public async Task<Contact?> GetContactByIdAsync(string id)
        {
            return await _dbContext.Users
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        /// <summary>
        /// Creates a new contact with the specified information and password.
        /// The contact's username is set to their email address.
        /// </summary>
        /// <param name="contact">Contact object</param>
        /// <param name="password">Contact password (plaintext)</param>
        /// <returns>Contact with set username</returns>
        /// <exception cref="Exception"></exception>
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
        
        /// <summary>
        /// Updates contact information and optionally their password. If a new password is provided, it will be updated as well.
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <param name="contact">Contact</param>
        /// <param name="newPassword">Optional, new password (plaintext)</param>
        /// <returns>Updated contact</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Contact> UpdateContactAsync(string id, Contact contact, string? newPassword = null)
        {
            var existingContact = await _userManager.FindByIdAsync(id);
            if (existingContact == null)
            {
                throw new Exception("Contact not found");
            }

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            existingContact.UserName = contact.Email;
            existingContact.Phone = contact.Phone;
            existingContact.BirthDate = contact.BirthDate;
            existingContact.CategoryId = contact.CategoryId;
            existingContact.SubcategoryId = contact.SubcategoryId;
            existingContact.CustomSubcategory = contact.CustomSubcategory;

            var result = await _userManager.UpdateAsync(existingContact);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to update contact: {errors}");
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingContact);
                var passResult = await _userManager.ResetPasswordAsync(existingContact, token, newPassword);
                if (!passResult.Succeeded)
                {
                    var errors = string.Join(", ", passResult.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to update password: {errors}");
                }
            }

            return existingContact;
        }

        /// <summary>
        /// Deletes a contact by their ID.
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>true if success, false if fail</returns>
        public async Task<bool> DeleteContactAsync(string id)
        {
            var contact = await _userManager.FindByIdAsync(id);
            if (contact == null)
            {
                return false;
            }
            
            var result = await _userManager.DeleteAsync(contact);
            return result.Succeeded;
        }
        
        /// <summary>
        /// Gets all categories with their subcategories.
        /// </summary>
        /// <returns>Category list with subcategories.</returns>
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