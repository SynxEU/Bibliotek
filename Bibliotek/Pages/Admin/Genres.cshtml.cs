using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotek.Pages.Admin
{
    public class GenresModel : PageModel
    {
        private readonly IBookService _bookService;

        public GenresModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        [BindProperty]
        public List<Genre> ListOfGenres { get; set; } = new List<Genre>();
        public IActionResult OnGet()
        {
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
