using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages.Admin.Create
{
    public class AuthorModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public AuthorModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public IActionResult OnGet()
        {
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
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public DateTime DOB { get; set; }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Admin/Authors");
        }
        public IActionResult OnPostCreate()
        {
            if (!string.IsNullOrWhiteSpace(Name) && DOB > DateTime.MinValue)
            {
                _authorService.CreateAuthor(Name, DOB);
                return RedirectToPage("/Admin/Authors");
            }
            else
            {
                ModelState.AddModelError("Author", "Author details are wrong");
                return Page();
            }
        }
    }
}
