using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages
{
    public class SigninModel : PageModel
    {
        private readonly ILoanerService _loanerService;

        public SigninModel(ILoanerService loanerService)
        {
            _loanerService = loanerService;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public string Mail { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        public IActionResult OnPost()
        {
            Loaner loaner = new Loaner();
            loaner.Email = Mail;
            loaner = _loanerService.GetLoaner(loaner.Email);
            if (loaner.Password == Password)
            {
                HttpContext.Session.Boolean("Admin", loaner.Admin);
                HttpContext.Session.SetString("Name", loaner.Name);
                HttpContext.Session.SetInt32("ID", loaner.Id);
                if (loaner.Admin == true) { return RedirectToPage("/Admin/Dashboard"); }
                else { return RedirectToPage("/user/dashboard"); }
            }
            else
            {
                return RedirectToPage("/Signup");
            }
        }
    }
}
