using System.ComponentModel.DataAnnotations;

namespace ContactApp.Models
{
  public class Contact
  {
    [Key]
    public int Id { get; set; }

    public string UserId { get; set; }

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

    //je mozno rozsirovat napr o adresu atd.
  }
}
