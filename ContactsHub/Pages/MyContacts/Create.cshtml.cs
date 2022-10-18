using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactsHub.Data;
using ContactsHub.Data.Model;

namespace ContactsHub.Pages.MyContacts
{
    using Extensions;
    using Microsoft.Build.Framework;

    public class CreateModel : PageModel
    {
        public class CreateViewModel
        {
            [Required]
            public ContactType Type { get; set; }
            
            public string? Name { get; set; }
            
            [Required]
            public string Value { get; set; }
        }
        
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public CreateModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateViewModel Contact { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contact = new Contact()
            {
                Type = Contact.Type,
                Name = Contact.Name,
                Value = Contact.Value,
                UserId = User.GetUserId()
            };
            
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
