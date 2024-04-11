using ContactAppData.Interfaces;
using ContactAppData.Models;
using Microsoft.EntityFrameworkCore;

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
    
    public void EditContact(Contact contact)
    {
      _context.Contacts.Update(contact);
    }

    public async Task<bool> ContactExists(Guid userId, int id)
    {
      return await _context.Contacts.AnyAsync(e => e.Id == id && e.UserId==userId);
    }

    public void DeleteContact(Contact contact)
    {
      _context.Contacts.Remove(contact);
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
