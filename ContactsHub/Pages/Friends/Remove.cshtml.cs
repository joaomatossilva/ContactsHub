using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactsHub.Data;
using ContactsHub.Data.Model;

namespace ContactsHub.Pages.Friends
{
    using Extensions;

    public class RemoveModel : PageModel
    {
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public RemoveModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Friend Friend { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var userId = User.GetUserId();
            var friend = await _context.Friends
                .Where(m => m.Id == id && m.UserId == userId)
                .FirstOrDefaultAsync();
            
            if (friend == null)
            {
                return NotFound();
            }
            
            Friend = friend;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var userId = User.GetUserId();
            var friend = await _context.Friends.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (friend != null)
            {
                Friend = friend;
                var reverseFriend = await _context.Friends.FirstOrDefaultAsync(m => m.FriendUserId == userId && m.UserId == friend.FriendUserId);
                if (reverseFriend != null)
                {
                    _context.Friends.Remove(reverseFriend);
                }
                _context.Friends.Remove(Friend);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
