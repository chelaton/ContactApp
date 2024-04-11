using ContactAppData.Models;

namespace ContactAppServices.Interfaces
{
    public interface IContactService
    {
    Task<bool> ContactExist(Guid userId, int id);
    Task CreateContact(Guid userId, string name, string surName, string phone, string email);
    Task DeleteContact(Guid userId, int id);
    Task EditContact(Guid userId, int id, string name, string surName, string phone, string email);
    Task<Contact> GetContactByIdAsync(Guid userId, int contactId);
    Task<IEnumerable<Contact>> GetContactsAsync(Guid userId);
    }
}