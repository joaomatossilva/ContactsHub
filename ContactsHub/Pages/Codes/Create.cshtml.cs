using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactsHub.Data;
using ContactsHub.Data.Model;

namespace ContactsHub.Pages.Codes
{
    using Extensions;

    public class CreateModel : PageModel
    {
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public CreateModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.GetUserId();

            var newShortCode = await _context.Codes.GetUnusedCode(6);
            var newCode = new Code
            {
                Short = newShortCode,
                UserId = userId,
                IsActive = true
            };
            
            _context.Codes.Add(newCode);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
