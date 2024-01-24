using Bibliotek.Service.Interfaces;
using Bibliotek.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Reflection.PortableExecutable;
using Bibliotek.Service.Methods;

namespace Bibliotek.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ILoanerService _loanerService;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public DashboardModel(ILoanerService loanerService, IBookService bookService, IAuthorService authorService)
        {
            _loanerService = loanerService;
            _bookService = bookService;
            _authorService = authorService;
        }
        [BindProperty]
        public Loaner UserDetails { get; set; }
        [BindProperty]
        public List<Books> ListOfBooks { get; set; } = new List<Books>();
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || HttpContext.Session.GetBoolean("Admin"))
            {
				return RedirectToPage("/errors/403");
            }
            else
            {
				int? tempid = HttpContext.Session.GetInt32("ID");
				int id = tempid.HasValue ? tempid.Value : 0;
                UserDetails = _loanerService.GetLoanerById(id);
                Books loanedBook = new Books();

                loanedBook = _bookService.GetLoanedBooks(id);

                if (loanedBook.Id != 0) 
                { 
                    ListOfBooks.Add(loanedBook);
                }
                foreach (Books book in ListOfBooks)
                {
                    List<Genre> genresForBook = _bookService.Genres(book.Id);
                    book.Genres = genresForBook;

                    Author author = _authorService.Authors(book.Author_ID);
                    book.Author = author;
                }
                return Page();
            }
        }
        public IActionResult OnPostReturn(int bookId)
        {
            _bookService.ReturnBook(bookId);
            return RedirectToPage("/user/dashboard");
        }
    }
}
