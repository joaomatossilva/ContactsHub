using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactsHub.Data;
using ContactsHub.Data.Model;

namespace ContactsHub.Pages.MyContacts
{
    using Extensions;

    public class IndexModel : PageModel
    {
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public IndexModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Contacts != null)
            {
                var userId = User.GetUserId();
                Contact = await _context.Contacts
                    .Where(c => c.UserId == userId)
                    .ToListAsync();
            }
        }
    }
}
