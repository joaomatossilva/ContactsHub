using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactsHub.Data;
using ContactsHub.Data.Model;

namespace ContactsHub.Pages.MyContacts
{
    using Extensions;
    using Microsoft.Build.Framework;

    public class EditModel : PageModel
    {
        public class EditContactViewModel
        {
            public Guid Id { get; set; }
            
            public ContactType Type { get; set; }

            public string? Name { get; set; }
            
            [Required]
            public string Value { get; set; }
        }
        
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public EditModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditContactViewModel Contact { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.GetUserId();
            var contact =  await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (contact == null)
            {
                return NotFound();
            }
            
            Contact = new EditContactViewModel
            {
                Id = contact.Id,
                Type = contact.Type,
                Name = contact.Name,
                Value = contact.Value,
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.GetUserId();
            var contact =  await _context.Contacts.FirstOrDefaultAsync(m => m.Id == Contact.Id && m.UserId == userId);
            if (contact == null)
            {
                return NotFound();
            }

            contact.Name = Contact.Name;
            contact.Value = Contact.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(Contact.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContactExists(Guid id)
        {
          return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
