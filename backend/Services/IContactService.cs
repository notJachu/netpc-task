using backend.Models;

namespace backend.Services;

public interface IContactService
{
    Task<List<Contact>> GetContactsAsync();
    Task<Contact?> GetContactByIdAsync(string id);
    Task<Contact> CreateContactAsync(Contact contact);
    Task<Contact> UpdateContactAsync(string id, Contact contact);
    Task<bool> DeleteContactAsync(string id);
}