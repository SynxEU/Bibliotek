using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPostDontLogout()
        {
            if (HttpContext.Session.GetBoolean("Admin") == false) { return RedirectToPage("/User/dashboard"); }
            else { return RedirectToPage("/Admin/Dashboard"); }
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("Admin");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("ID");
            return RedirectToPage("/Index");
        }
    }
}
