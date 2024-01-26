using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Bibliotek.Domain.Models;

namespace Bibliotek.Pages.Admin.Edit
{
    public class UserModel : PageModel
    {
        private readonly ILoanerService _loanerService;

        public UserModel(ILoanerService loanerService)
        {
            _loanerService = loanerService;
        }
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("TempLoanerID").HasValue)
            {
                return RedirectToPage("/errors/500");
            }
            if (!HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/errors/403");
            }
            else
            {
                return Page();
            }

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
        public string ConfirmPassword { get; set; } = string.Empty;
        [BindProperty, Required]
        public string AdminPassword { get; set; } = string.Empty;
        [BindProperty]
        public bool IsChecked { get; set; }

        public IActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("TempLoanerID");
            return RedirectToPage("/Admin/Users");
        }
        public IActionResult OnPostUpdate()
        {
            int? tempid = HttpContext.Session.GetInt32("TempLoanerID");
            int id = tempid.HasValue ? tempid.Value : 0;
            string fullName = FirstName + " " + LastName;
            if (AdminPassword != "Admin1234!")
            {
                return Page();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    _loanerService.EditLoanerName(id, fullName);
                }
                if (!string.IsNullOrWhiteSpace(EmailAddress))
                {
                    _loanerService.EditLoanerEmail(id, EmailAddress);
                }
                if (!string.IsNullOrWhiteSpace(PhoneNumber))
                {
                    _loanerService.EditLoanerNumber(id, Convert.ToInt32(PhoneNumber));
                }
                if (!string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    _loanerService.EditLoanerPassword(id, ConfirmPassword);
                }
                if (IsChecked)
                {
                    _loanerService.AddAdmin(id);
                }
                else
                {
                    _loanerService.RemoveAdmin(id);
                }
                HttpContext.Session.Remove("TempLoanerID");
                return RedirectToPage("/Admin/Users");
            }
        }
    }
}
