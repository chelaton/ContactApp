using System.ComponentModel.DataAnnotations;

namespace ContactApp.Models.ViewModels
{
  public class ContactEdit
  {
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }

    [Required]
    [MaxLength(40)]
    public string Surname { get; set; }

    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [MaxLength(70)]
    public string? Email { get; set; }
  }
}
