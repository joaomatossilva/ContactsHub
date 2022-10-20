namespace ContactsHub.Data.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Contact
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey("User")]
    public string UserId { get; set; }
    
    public User User { get; set; }
    
    public ContactType Type { get; set; }
    
    public string? Name { get; set; }
    
    public string Value { get; set; }
}