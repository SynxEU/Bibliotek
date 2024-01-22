using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Bibliotek.Pages.User
{
    public class settingsModel : PageModel
    {
        private readonly ILoanerService _loanerService;

        public settingsModel(ILoanerService loanerService)
        {
            _loanerService = loanerService;
        }
        [BindProperty]
        public Loaner loaner { get; set; } = new Loaner();
		public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/errors/403");
            }
            else
            {
				int? tempId = HttpContext.Session.GetInt32("ID");
				int id = tempId.HasValue ? tempId.Value : 0;
				loaner = _loanerService.GetLoanerById(id);
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
        [BindProperty]
        public string CurrentPassword { get; set; } = string.Empty;

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/User/Dashboard"); 
        }
        public IActionResult OnPostUpdate()
        {
            int? tempId = HttpContext.Session.GetInt32("ID");
            int id = tempId.HasValue ? tempId.Value : 0;
            string fullName = FirstName + " " + LastName;
            string currentPassword = _loanerService.GetLoanerById(id).Password;

            if (CurrentPassword != currentPassword || Password != ConfirmPassword)
            {
                return Page();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    _loanerService.EditLoanerName(id, fullName);
                    HttpContext.Session.SetString("Name", fullName);
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
                return RedirectToPage("/User/Dashboard");
            }
        }
        public IActionResult OnPostDelete()
        {
            int? tempId = HttpContext.Session.GetInt32("ID");
            int id = tempId.HasValue ? tempId.Value : 0;
            string currentPassword = _loanerService.GetLoanerById(id).Password;

            if (CurrentPassword != currentPassword)
            {
                return Page();
            }
            else
            {
                HttpContext.Session.Remove("Admin");
                HttpContext.Session.Remove("Name");
                HttpContext.Session.Remove("ID");
                _loanerService.DeleteLoaner(id);
                return RedirectToPage("/index");
            }
        }
    }
}
