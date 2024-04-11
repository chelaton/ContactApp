using ContactAppData.Models;

namespace ContactAppData.Interfaces
{
  public interface IContactRepository
  {
    Task AddContactAsync(Contact contact);
    IQueryable<Contact> GetContacts();
    Task SaveChangesAsync();
  }
}