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
  }
}
