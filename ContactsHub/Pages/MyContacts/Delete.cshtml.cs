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

    public class DeleteModel : PageModel
    {
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public DeleteModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var userId = User.GetUserId();
            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (contact == null)
            {
                return NotFound();
            }
            else 
            {
                Contact = contact;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }
            var userId = User.GetUserId();
            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (contact != null)
            {
                Contact = contact;
                _context.Contacts.Remove(Contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
