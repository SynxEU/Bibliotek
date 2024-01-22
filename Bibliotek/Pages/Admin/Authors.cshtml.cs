using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages.Admin
{
    public class AuthorsModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public AuthorsModel(IAuthorService authorsService)
        {
            _authorService = authorsService;
        }
        [BindProperty]
        public List<Author> ListOfAuthors { get; set; } = new List<Author>();
        public IActionResult OnGet()
        {
            ListOfAuthors = _authorService.GetAuthors();

            if (!HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/errors/403");
            }
            else
            {
                return Page();
            }

        }
        public IActionResult OnPostEdit(int authorId)
        {
            HttpContext.Session.SetInt32("TempAuthorID", authorId);
            return RedirectToPage("/Admin/Edit/Author");
        }
        public IActionResult OnPostCreate()
        {
            return RedirectToPage("/Admin/Create/Author");
        }
        public IActionResult OnPostDelete(int authorId)
        {
            _authorService.DeleteAuthor(authorId);
            return RedirectToPage("/Admin/Authors");
        }
    }
}
