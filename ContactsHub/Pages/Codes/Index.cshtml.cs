using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactsHub.Data;
using ContactsHub.Data.Model;

namespace ContactsHub.Pages.Codes
{
    using Extensions;

    public class IndexModel : PageModel
    {
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public IndexModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Code> Code { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.GetUserId();
            Code = await _context.Codes
                .Where(c => c.UserId == userId && c.IsActive == true)
                .ToListAsync();
        }
    }
}
