using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages.Admin.Edit
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
            if (!HttpContext.Session.GetInt32("TempAuthorID").HasValue)
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
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public DateTime DOB { get; set; }

        public IActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("TempAuthorID");
            return RedirectToPage("/Admin/Authors");
        }
        public IActionResult OnPostUpdate()
        {
            int? tempid = HttpContext.Session.GetInt32("TempAuthorID");
            int id = tempid.HasValue ? tempid.Value : 0;

            if (!string.IsNullOrWhiteSpace(Name))
            {
                _authorService.EditAuthorName(id, Name);
            }
            if (DOB > DateTime.MinValue)
            {
                _authorService.EditAuthorDOB(id, DOB);
            }

            HttpContext.Session.Remove("TempAuthorID");
            return RedirectToPage("/Admin/Authors");
        }
    }
}

