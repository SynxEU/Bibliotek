using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Bibliotek.Pages.Admin.Create
{
    public class BookModel : PageModel
    {
        private readonly IBookService _bookService;

        public BookModel(IBookService bookService)
        {
            _bookService = bookService;
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
        public string Title { get; set; } = string.Empty;

        [BindProperty]
        public int Author { get; set; }

        [BindProperty]
        public int Genre { get; set; }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Admin/Books");
        }
        public IActionResult OnPostCreate()
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                _bookService.CreateBook(Title, Author, Genre);
            }
            return RedirectToPage("/Admin/Books");
        }
    }
}
