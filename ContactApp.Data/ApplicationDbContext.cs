using ContactAppData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactAppData
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public DbSet<Contact> Contacts { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
  }
}
