using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactsHub.Pages.Friends;

using Data;
using Data.Model;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Contacts : PageModel
{
    private readonly ApplicationDbContext _context;
    public IList<Contact> Contact { get;set; } = default!;
    
    public Contacts(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGet(Guid id)
    {
        //Check if is your friend
        var userId = User.GetUserId();
        var friend = await _context.Friends
            .Where(x => x.Id == id && x.UserId == userId)
            .FirstOrDefaultAsync();

        if (friend == null)
        {
            return NotFound();
        }

        var friendId = friend.FriendUserId;
        Contact = await _context.Contacts
            .Where(c => c.UserId == friendId)
            .ToListAsync();

        return Page();
    }
}