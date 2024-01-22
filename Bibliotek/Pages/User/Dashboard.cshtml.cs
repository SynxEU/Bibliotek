using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ILoanerService _loanerService;

        public DashboardModel(ILoanerService loanerService)
        {
            _loanerService = loanerService;
        }
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/errors/403");
            }
            else
            {
                return Page();
            }
        }
    }
}
