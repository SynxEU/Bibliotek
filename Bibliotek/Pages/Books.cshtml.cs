using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages
{
    public class BooksModel : PageModel
    {
        private readonly IBookService _bookService;
		private readonly IAuthorService _authorService;
		public BooksModel(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }
        [BindProperty]
        public List<Books> ListOfBooks { get; set; } = new List<Books>();
        public void OnGet()
        {
            ListOfBooks = _bookService.GetAllBooks();
            foreach (Books book in ListOfBooks)
            {
                List<Genre> genresForBook = _bookService.Genres(book.Id);
                book.Genres = genresForBook;

                Author author = _authorService.Authors(book.Author_ID);
                book.Author = author;
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

