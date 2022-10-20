namespace ContactsHub.Data.Model;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class FriendRequest
{
    public Guid Id { get; set; }
    
    [Display(@Name = "From")]
    public virtual User FromUser { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public string FromUserId { get; set; }
    
    [Display(@Name = "To")]
    public virtual User ToUser { get; set; }
    
    [Required]
    [ForeignKey("FriendUser")]
    public string ToUserId { get; set; }
    
    public DateTime DateTimeUtc { get; set; }
    
    public FriendRequestState State { get; set; }
}