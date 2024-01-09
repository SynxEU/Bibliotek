using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;

namespace Bibliotek.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookService _bookService;
        public IndexModel(ILogger<IndexModel> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }
        public void OnGet()
        {
            List<Books> books = _bookService.GetAllBooks();
        }
    }
}