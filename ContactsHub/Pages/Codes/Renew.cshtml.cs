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

    public class RenewModel : PageModel
    {
        private readonly ContactsHub.Data.ApplicationDbContext _context;

        public RenewModel(ContactsHub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Code Code { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.GetUserId();
            var code = await _context.Codes.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (code == null)
            {
                return NotFound();
            }

            Code = code;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.GetUserId();
            var code = await _context.Codes.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (code != null)
            {
                Code = code;
                code.IsActive = false;
                
                var newShortCode = await _context.Codes.GetUnusedCode(6);
                var newCode = new Code
                {
                    Short = newShortCode,
                    UserId = userId,
                    IsActive = true
                };
                _context.Codes.Add(newCode);
                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
