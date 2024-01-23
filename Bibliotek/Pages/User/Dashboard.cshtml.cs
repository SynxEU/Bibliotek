using Bibliotek.Service.Interfaces;
using Bibliotek.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Reflection.PortableExecutable;

namespace Bibliotek.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ILoanerService _loanerService;

        public DashboardModel(ILoanerService loanerService)
        {
            _loanerService = loanerService;
        }
        [BindProperty]
        public Loaner UserDetails { get; set; }
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || HttpContext.Session.GetBoolean("Admin"))
            {
				return RedirectToPage("/errors/403");
            }
            else
            {
				int? tempid = HttpContext.Session.GetInt32("ID");
				int id = tempid.HasValue ? tempid.Value : 0;
                UserDetails = _loanerService.GetLoanerById(id);
				return Page();
            }
        }
    }
}
