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
    using Microsoft.AspNetCore.Identity;

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
            var userId = User.GetUserId();
            Friends = await _context.Friends
                .Where(f => f.UserId == userId)
                .Include(f => f.FriendUser)
                .ToListAsync();
        }
    }
}
