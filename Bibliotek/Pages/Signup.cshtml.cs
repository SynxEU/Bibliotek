using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bibliotek.Service.Interfaces;
using Bibliotek.Service.Methods;
using Bibliotek.Domain.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Bibliotek.Pages
{
    public class SignupModel : PageModel
    {
        private readonly ILoanerService _loanerService;

        public SignupModel(ILoanerService loanerService)
        {
            _loanerService = loanerService;
        }

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string EmailAddress { get; set; } = string.Empty;

        [BindProperty]
        [MinLength(7)]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        [Compare(nameof(Password), ErrorMessage = "Make sure passwords are matching")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public IActionResult OnPost()
        {
            string fullName = FirstName + " " + LastName;

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                _loanerService.CreateLoaner(fullName, EmailAddress, Password, Convert.ToInt32(PhoneNumber));

                Loaner loaner = new Loaner();

                loaner.Email = EmailAddress;
                loaner = _loanerService.GetLoaner(loaner.Email);

                HttpContext.Session.Boolean("Admin", loaner.Admin);
                HttpContext.Session.SetString("Name", loaner.Name);
                HttpContext.Session.SetInt32("ID", loaner.Id);

                return RedirectToPage("/user/dashboard");
            }

            else
            {
                return Page();
            }

        }
    }
}
