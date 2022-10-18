using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactsHub.Data;
using ContactsHub.Data.Model;

namespace ContactsHub.Pages.Requests
{
    using Extensions;

    public class CancelModel : PageModel
    {
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public CancelModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public FriendRequest FriendRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.GetUserId();
            var friendRequest = await _context.FriendRequests
                .FirstOrDefaultAsync(m => m.Id == id && m.FromUserId == userId);

            if (friendRequest == null)
            {
                return NotFound();
            }
            else 
            {
                FriendRequest = friendRequest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.FriendRequests == null)
            {
                return NotFound();
            }
            
            var userId = User.GetUserId();
            var friendRequest = await _context.FriendRequests
                .FirstOrDefaultAsync(m => m.Id == id && m.FromUserId == userId);

            if (friendRequest != null)
            {
                FriendRequest = friendRequest;
                _context.FriendRequests.Remove(FriendRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
