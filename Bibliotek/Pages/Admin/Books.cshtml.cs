using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Bibliotek.Service.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages.Admin
{
    public class BooksModel : PageModel
    {
        private readonly IBookService _bookService;

        public BooksModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        [BindProperty]
        public List<Books> ListOfBooks { get; set; } = new List<Books>();
        [BindProperty]
        public List<Genre> BookGenres { get; set; } = new List<Genre>();
        public IActionResult OnGet()
        {
            ListOfBooks = _bookService.GetAllBooks();
            foreach (Books book in ListOfBooks)
            {
                List<Genre> genresForBook = _bookService.Genres(book.Id);
                book.Genres = genresForBook;
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
        public IActionResult OnPostEdit(int bookId)
        {
            HttpContext.Session.SetInt32("TempBookID", bookId);
            return RedirectToPage("/Admin/Edit/Book");
        }
        public IActionResult OnPostCreate()
        {
            return RedirectToPage("/Admin/Create/Book");
        }
        public IActionResult OnPostDelete(int bookId)
        {
            _bookService.DeleteBook(bookId);
            return RedirectToPage("/Admin/Books");
        }
    }
}
