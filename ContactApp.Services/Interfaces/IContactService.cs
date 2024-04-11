using ContactAppData.Models;

namespace ContactAppServices.Interfaces
{
    public interface IContactService
    {
    Task CreateContact(Guid userId, string name, string surName, string phone, string email);
    Task<Contact> GetContactByIdAsync(Guid userId, int contactId);
    Task<IEnumerable<Contact>> GetContactsAsync(Guid userId);
    }
}