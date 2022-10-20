using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactsHub.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string? Code { get; private set; }
    
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet(string? code)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            if (code != null)
            {
                return RedirectToPage("/Invite/Index", new {code});
            }
            return RedirectToPage("/Friends/Index");
        }

        Code = code;
        return Page();
    }
}