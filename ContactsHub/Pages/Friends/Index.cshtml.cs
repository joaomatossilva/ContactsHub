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
    using System.Configuration;
    using Extensions;

    public class IndexModel : PageModel
    {
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public IndexModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Friend> Friends { get;set; } = default!;

        public async Task OnGetAsync()
        {
            
            // var friend1 = new Friend
            // {
            //     Id = Guid.NewGuid(),
            //     UserId = User.GetUserId(),
            //     FriendUserId = "97bdcbbc-fdce-48ae-99e2-39d5f2350606"
            // };
            // var friend2 = new Friend
            // {
            //     Id = Guid.NewGuid(),
            //     UserId = "97bdcbbc-fdce-48ae-99e2-39d5f2350606",
            //     FriendUserId = User.GetUserId()
            // };
            // _context.Friends.Add(friend1);
            // _context.Friends.Add(friend2);
            // _context.SaveChanges();
            //
            
            
            var userId = User.GetUserId();
            Friends = await _context.Friends
                .Where(f => f.UserId == userId)
                .Include(f => f.FriendUser)
                .ToListAsync();
        }
    }
}
