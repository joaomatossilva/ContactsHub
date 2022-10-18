using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactsHub.Pages.Requests;

using System.ComponentModel.DataAnnotations;
using Data;
using Data.Model;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Create : PageModel
{
    private readonly ApplicationDbContext _context;

    public class CreateRequestViewModel
    {
        [Required]
        public string Code { get; set; }
    }
    
    [BindProperty]
    public CreateRequestViewModel CreateRequest { get; set; } = default!;

    
    public Create(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var code = await _context.Codes
            .FirstOrDefaultAsync(x => x.Short == CreateRequest.Code);
        
        if (code == null)
        {
            return NotFound();
        }
            
        var userId = User.GetUserId();
        var newRequest = new FriendRequest
        {
            FromUserId = userId,
            ToUserId = code.UserId,
            DateTimeUtc = DateTime.UtcNow,
            State = FriendRequestState.Pending
        };

        _context.FriendRequests.Add(newRequest);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}