using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages
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
        public void OnGet()
        {
            ListOfBooks = _bookService.GetAllBooks();
            foreach (Books book in ListOfBooks)
            {
                List<Genre> genresForBook = _bookService.Genres(book.Id);
                book.Genres = genresForBook;
            }
        }
        public IActionResult OnPost(int bookId)
        {
            int? tempid = HttpContext.Session.GetInt32("ID");
            int id = tempid.HasValue ? tempid.Value : 0;
            _bookService.BorrowBook(bookId, id);
            return RedirectToPage("/books");
        }
    }
}
