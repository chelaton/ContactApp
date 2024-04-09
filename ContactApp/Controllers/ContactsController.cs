using ContactApp.Data;
using ContactApp.Models;
using ContactApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactApp.Controllers
{
  [Authorize]
  public class ContactsController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly string _userId;

    public ContactsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
      _context = context;
      _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("It's broken. ;)");
    }

    // GET: Contacts
    public async Task<IActionResult> Index()
    {
      return View(await _context.Contacts.Where(c=>c.UserId==_userId).ToListAsync());
    }

    // GET: Contacts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contact = await _context.Contacts
          .FirstOrDefaultAsync(m => m.Id == id && m.UserId==_userId);
      if (contact == null)
      {
        return NotFound();
      }

      return View(contact);
    }

    // GET: Contacts/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Contacts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Surname,PhoneNumber,Email")] ContactCreate contactCreate)
    {
      if (ModelState.IsValid)
      {
        var contact = new Contact() { UserId = _userId, Email = contactCreate.Email, Name = contactCreate.Name, Surname = contactCreate.Surname, PhoneNumber = contactCreate.PhoneNumber };
        _context.Add(contact);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(contactCreate);
    }

    // GET: Contacts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id && m.UserId == _userId);
      if (contact == null)
      {
        return NotFound();
      }
      return View(contact);
    }

    // POST: Contacts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
          var contact = new Contact() {Id=contactEdit.Id, UserId = _userId, Email = contactEdit.Email, 
            Name = contactEdit.Name, Surname = contactEdit.Surname, PhoneNumber = contactEdit.PhoneNumber };

          _context.Update(contact);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ContactExists(contactEdit.Id))
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

    // GET: Contacts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contact = await _context.Contacts
          .FirstOrDefaultAsync(m => m.Id == id && m.UserId==_userId);
      if (contact == null)
      {
        return NotFound();
      }

      return View(contact);
    }

    // POST: Contacts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id && m.UserId == _userId);
      if (contact != null)
      {
        _context.Contacts.Remove(contact);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ContactExists(int id)
    {
      return _context.Contacts.Any(e => e.Id == id);
    }
  }
}
