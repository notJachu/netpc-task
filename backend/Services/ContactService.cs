using backend.Database;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class ContactService(AppDbContext _dbContext) : IContactService
{
        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
    
        public async Task<Contact?> GetContactByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    
        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    
        public async Task<Contact> UpdateContactAsync(string id, Contact contact)
        {
            throw new NotImplementedException();
        }
    
        public async Task<bool> DeleteContactAsync(string id)
        {
            throw new NotImplementedException();
        }
    
}