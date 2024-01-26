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
        [BindProperty, Required]
        public string Title { get; set; } = string.Empty;

        [BindProperty, Required]
        public int Author { get; set; }

        [BindProperty, Required]
        public int Genre { get; set; }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Admin/Books");
        }
        public IActionResult OnPostCreate()
        {
            if (!string.IsNullOrWhiteSpace(Title) && Author != 0 && Genre != 0)
            {
                _bookService.CreateBook(Title, Author, Genre);
                return RedirectToPage("/Admin/Books");
            }
            else
            {
                ModelState.AddModelError("wrongtitle", "Book details was wrong");
                return Page();
            }
        }
    }
}
