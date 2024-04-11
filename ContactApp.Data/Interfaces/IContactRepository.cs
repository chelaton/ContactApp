using ContactAppData.Models;

namespace ContactAppData.Interfaces
{
  public interface IContactRepository
  {
    Task AddContactAsync(Contact contact);
    Task<bool> ContactExists(Guid userId, int id);
    void DeleteContact(Contact contact);
    void EditContact(Contact contact);
    IQueryable<Contact> GetContacts();
    Task SaveChangesAsync();
  }
}