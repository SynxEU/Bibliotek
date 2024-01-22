using Bibliotek.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.Service.Interfaces
{
    public interface IBookService
    {
        List<Books> GetAllBooks();
        Books CreateBook(string title, int authorID, int genreID);
        void DeleteBook(int id);
        void BorrowBook(int bookID, int loanerID);
        void ReturnBook(int bookID);
        string EditBookTitle(int id, string newTitle);
        string EditBookAuthor(int id, int newAuthorID);
        string AddBookGenre(int bookID, int genreID);
        List<Genre> Genres(int id);
        List<Genre> GetGenres();
    }
}
