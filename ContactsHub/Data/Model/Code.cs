namespace ContactsHub.Data.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Code
{
    [Key] 
    public Guid Id { get; set; }

    [Required]
    public string Short { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }
    
    public virtual IdentityUser User { get; set; }
    
    public bool IsActive { get; set; }
}