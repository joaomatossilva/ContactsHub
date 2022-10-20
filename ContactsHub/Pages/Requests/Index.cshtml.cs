
namespace ContactsHub.Pages.Requests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using ContactsHub.Data;
    using ContactsHub.Data.Model;
    using Extensions;

    public class IndexModel : PageModel
    {

        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public IndexModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FriendRequest> FriendRequestOutgoing { get;set; } = default!;
        
        public IList<FriendRequest> FriendRequestIncoming { get;set; } = default!;
        
        public async Task OnGetAsync()
        {
            var userId = User.GetUserId();
            FriendRequestOutgoing = await _context.FriendRequests
                .Where(x => x.FromUserId == userId && x.State == FriendRequestState.Pending)
                .Include(f => f.ToUser).ToListAsync();
            FriendRequestIncoming = await _context.FriendRequests
                .Where(x => x.ToUserId == userId && x.State == FriendRequestState.Pending)
                .Include(f => f.FromUser).ToListAsync();
        }

        public async Task<IActionResult> OnPostAccept(Guid id)
        {
            var userId = User.GetUserId();
            var request = await _context.FriendRequests
                .FirstOrDefaultAsync(x => x.Id == id && x.ToUserId == userId);

            if (request == null)
            {
                return NotFound();
            }
            
            request.State = FriendRequestState.Accepted;

            var newFriend = new Friend
            {
                UserId = userId,
                FriendUserId = request.FromUserId
            };
            var reverseFriend = new Friend
            {
                UserId = request.FromUserId,
                FriendUserId = userId
            };
            _context.Friends.Add(newFriend);
            _context.Friends.Add(reverseFriend);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("/Friends/Contacts", new {id = newFriend.Id});
        }
        
        public async Task<IActionResult> OnPostReject(Guid id)
        {
            var userId = User.GetUserId();
            var request = await _context.FriendRequests
                .FirstOrDefaultAsync(x => x.Id == id && x.ToUserId == userId);

            if (request != null)
            {
                request.State = FriendRequestState.Rejected;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
