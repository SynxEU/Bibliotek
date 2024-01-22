using Bibliotek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace Bibliotek.Pages.Admin.Edit
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
            if (!HttpContext.Session.GetInt32("TempBookID").HasValue)
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
        public string Title { get; set; } = string.Empty;

        [BindProperty]
        public int Author { get; set; }
        [BindProperty]
        public int Genre { get; set; }
        public IActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("TempBookID");
            return RedirectToPage("/Admin/Books");
        }
        public IActionResult OnPostUpdate()
        {
            int? tempid = HttpContext.Session.GetInt32("TempBookID");
            int id = tempid.HasValue ? tempid.Value : 0;

            if (!string.IsNullOrWhiteSpace(Title))
            {
                _bookService.EditBookTitle(id, Title);
            }
            if (Author > 0)
            {
                _bookService.EditBookAuthor(id, Author);
            }
            if (Genre > 0)
            {
                _bookService.AddBookGenre(id, Genre);
            }

            HttpContext.Session.Remove("TempBookID");
            return RedirectToPage("/Admin/Books");
        }
    }
}
