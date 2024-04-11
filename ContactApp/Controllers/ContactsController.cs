using ContactApp.Models.ViewModels;
using ContactAppServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactApp.Controllers
{
  [Authorize]
  public class ContactsController : Controller
  {
    private readonly Guid _userId;
    private readonly IContactService _contactService;

    public ContactsController( IHttpContextAccessor httpContextAccessor, IContactService contactService )
    {
      var parseOk = Guid.TryParse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out _userId);
      _contactService = contactService;
    }

    // GET: Contacts
    public async Task<IActionResult> Index()
    {
      var contacts = await _contactService.GetContactsAsync(_userId);

      var contactsIndex = contacts.Select( contact => new ContactIndex() 
      { Id= contact.Id, Name=contact.Name, Surname=contact.Surname, Email=contact.Email, PhoneNumber=contact.PhoneNumber }
      ).ToList();
      
      return View(contactsIndex);
    }

    //// GET: Contacts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contact = await _contactService.GetContactByIdAsync(_userId, (int)id);
      if (contact == null)
      {
        return NotFound();
      }
      var contactDetail = new ContactDetail() { Id = contact.Id, Name = contact.Name, Surname = contact.Surname, Email = contact.Email, PhoneNumber = contact.PhoneNumber };
      return View(contactDetail);
    }

    //// GET: Contacts/Create
    public IActionResult create()
    {
      return View();
    }

    //// POST: Contacts/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Surname,PhoneNumber,Email")] ContactCreate contactCreate)
    {
      if (ModelState.IsValid)
      {
        await _contactService.CreateContact(_userId, contactCreate.Name, contactCreate.Surname, contactCreate.PhoneNumber, contactCreate.Email);
        return RedirectToAction(nameof(Index));
      }
      return View(contactCreate);
    }

    //// GET: Contacts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contact = await _contactService.GetContactByIdAsync(_userId, (int)id);
      if (contact == null)
      {
        return NotFound();
      }
      var contactEdit = new ContactEdit() { Id = contact.Id, Name = contact.Name, Surname = contact.Surname, Email = contact.Email, PhoneNumber = contact.PhoneNumber };

      return View(contactEdit);
    }

    //// POST: Contacts/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,PhoneNumber,Email")] ContactEdit contactEdit)
    {
      if (id != contactEdit.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          await _contactService.EditContact(_userId, contactEdit.Id, contactEdit.Name, contactEdit.Surname, contactEdit.PhoneNumber, contactEdit.Email);
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!await _contactService.ContactExist(_userId, id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(contactEdit);
    }

    //// GET: Contacts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contact = await _contactService.GetContactByIdAsync(_userId, (int)id);
      if (contact == null)
      {
        return NotFound();
      }
      var contactDelete = new ContactDelete() { Id = contact.Id, Name = contact.Name, Surname = contact.Surname, Email = contact.Email, PhoneNumber = contact.PhoneNumber };


      return View(contactDelete);
    }

    //// POST: Contacts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _contactService.DeleteContact(_userId, id);
      return RedirectToAction(nameof(Index));
    }

  }
}
