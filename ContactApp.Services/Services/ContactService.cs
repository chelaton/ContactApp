using ContactAppData.Interfaces;
using ContactAppData.Models;
using ContactAppServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactAppServices.Services
{
  public class ContactService : IContactService
  {
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
      _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<Contact>> GetContactsAsync(Guid userId)
    {
      return await _contactRepository.GetContacts().Where(c => c.UserId == userId).ToListAsync();
    }
    
    public async Task<Contact> GetContactByIdAsync(Guid userId, int contactId)
    {
      return await _contactRepository.GetContacts().FirstOrDefaultAsync(c => c.UserId == userId && c.Id==contactId);
    }

    public async Task CreateContact(Guid userId, string name, string surName, string phone, string email) // jen jsem nechtel vytvaret dalsi model ;)
    {
      var contact = new Contact() { UserId = userId, Email = email, Name = name, Surname = surName, PhoneNumber = phone };
      await _contactRepository.AddContactAsync(contact);
      await _contactRepository.SaveChangesAsync();
      return;
    }

    public async Task EditContact(Guid userId, int id, string name, string surName, string phone, string email)
    {
      var contactUnedited = await GetContactByIdAsync(userId, id);
      if (contactUnedited != null)
      {
        contactUnedited.Email = email;
        contactUnedited.Name = name;
        contactUnedited.Surname = surName;
        contactUnedited.PhoneNumber = phone;

        _contactRepository.EditContact(contactUnedited);
        await _contactRepository.SaveChangesAsync();
      }
    }
    
    public async Task DeleteContact(Guid userId, int id)
    {
      var contactToDelete = await GetContactByIdAsync(userId, id);
      if (contactToDelete != null)
      {
        _contactRepository.DeleteContact(contactToDelete);
        await _contactRepository.SaveChangesAsync();
      }
    }

    public async Task<bool> ContactExist(Guid userId, int id)
    {
      return await _contactRepository.ContactExists(userId, id);
    }
  }
}
