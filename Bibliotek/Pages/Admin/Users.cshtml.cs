using Bibliotek.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using Bibliotek.Service.Interfaces;
using Bibliotek.Service.Methods;

namespace Bibliotek.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly ILoanerService _loanerService;

        public UsersModel(ILoanerService loanerService)
        {
            _loanerService = loanerService;
        }
        [BindProperty]
        public List<Loaner> ListOfLoaners { get; set; } = new List<Loaner>();
        public IActionResult OnGet()
        {
            ListOfLoaners = _loanerService.GetLoaners();
            ListOfLoaners.RemoveAt(0);

            if (!HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/errors/403");
            }
            else
            {
                return Page();
            }

        }
        public IActionResult OnPostEdit(int loanerId) 
        {
            HttpContext.Session.SetInt32("TempLoanerID", loanerId);
            return RedirectToPage("/Admin/Edit/User");
        }
        public IActionResult OnPostDelete(int loanerId)
        {
            _loanerService.DeleteLoaner(loanerId);
            return RedirectToPage("/Admin/Users");
        }
    }
}
