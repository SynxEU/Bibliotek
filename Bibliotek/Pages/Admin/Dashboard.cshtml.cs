using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages.Admin
{
    public class AdminProfileModel : PageModel
    {

        private readonly IAuthorService _authorService;
        private readonly ILoanerService _loanerService;
        private readonly IBookService _bookService;

        public AdminProfileModel(IAuthorService authorsService, ILoanerService loanerService, IBookService bookService)
        {
            _authorService = authorsService;
            _loanerService = loanerService;
            _bookService = bookService;
        }
        [BindProperty]
        public List<Author> ListOfAuthors { get; set; } = new List<Author>();
        [BindProperty]
        public List<Books> ListOfBooks { get; set; } = new List<Books>();
        [BindProperty]
        public List<Loaner> ListOfLoaners { get; set; } = new List<Loaner>();
        [BindProperty]
        public List<Genre> ListOfGenres { get; set; } = new List<Genre>();

        public IActionResult OnGet()
        {
            ListOfLoaners = _loanerService.GetLoaners();
            ListOfLoaners.RemoveAt(0);
            ListOfBooks = _bookService.GetAllBooks();
            ListOfAuthors = _authorService.GetAuthors();
            ListOfGenres = _bookService.GetGenres();
            if (!HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/errors/403");
            }
            else
            {
                return Page();
            }

        }
    }
}
