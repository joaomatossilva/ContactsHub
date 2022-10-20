namespace ContactsHub.Data.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Friend
{
    [Key]
    public Guid Id { get; set; }
    
    public virtual User User { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }
    
    [Display(@Name = "Friend")]
    public virtual User FriendUser { get; set; }
    
    [Required]
    [ForeignKey("FriendUser")]
    public string FriendUserId { get; set; }
}