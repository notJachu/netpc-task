using backend.Models;

namespace backend.Services;

public interface IContactService
{
    Task<List<Contact>> GetContactsAsync();
    Task<Contact> GetContactByIdAsync(int id);
    Task<Contact> CreateContactAsync(Contact contact);
    Task<Contact> UpdateContactAsync(int id, Contact contact);
    Task<bool> DeleteContactAsync(int id);
}