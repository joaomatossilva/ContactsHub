using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactsHub.Pages.Invite;

using System.ComponentModel.DataAnnotations;
using Data;
using Data.Model;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Index : PageModel
{
    public class CreateRequestViewModel
    {
        [Required]
        public string Code { get; set; }
    }
    
    private readonly ApplicationDbContext _context;
    public string ShareCode { get; set; }
    public string ShareUrl { get; set; }
    [BindProperty]
    public CreateRequestViewModel CreateRequest { get; set; } = default!;
    
    public Index(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGet(string? code)
    {
        var userId = User.GetUserId();
        var myCode = await _context.Codes
            .FirstOrDefaultAsync(x => x.IsActive == true && x.UserId == userId);

        //initialize first code here??
        if (myCode == default)
        {
            var newCode = await _context.Codes.GetUnusedCode(6);
            myCode = new Code
            {
                IsActive = true,
                UserId = userId,
                Short = newCode
            };
            _context.Codes.Add(myCode);
            await _context.SaveChangesAsync();
        }

        ShareCode = myCode.Short;
        ShareUrl = $"{this.Request.Scheme}://{this.Request.Host}/invite?code={myCode.Short}";
        if (code != null)
        {
            CreateRequest = new CreateRequestViewModel
            {
                Code = code
            };
        }
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
        //ignore if it's my own code
        if (userId == code.UserId)
        {
            return RedirectToPage("./Index");
        }
        
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