using backend.Database;
using backend.Models;

namespace backend.Services;

public class ContactService(AppDbContext _dbContext) : IContactService
{
        public Task<List<Contact>> GetContactsAsync()
        {
            throw new NotImplementedException();
        }
    
        public Task<Contact> GetContactByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    
        public Task<Contact> CreateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    
        public Task<Contact> UpdateContactAsync(int id, Contact contact)
        {
            throw new NotImplementedException();
        }
    
        public Task<bool> DeleteContactAsync(int id)
        {
            throw new NotImplementedException();
        }
    
}