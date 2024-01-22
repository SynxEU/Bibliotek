using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages.errors
{
    public class _500Model : PageModel
    {
        public void OnGet() { }
        public void OnPost()
        {
            RedirectToPage("/index");
        }
    }
}
