using ContactAppData.Interfaces;
using ContactAppData.Models;

namespace ContactAppData.Repository
{
  public class ContactRepository : IContactRepository
  {
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public IQueryable<Contact> GetContacts()
    {
      return _context.Contacts.AsQueryable();
    }

    public async Task AddContactAsync(Contact contact)
    {
      await _context.Contacts.AddAsync(contact);
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
