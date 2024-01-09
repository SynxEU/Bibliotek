using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotek.Domain.Models;
using Bibliotek.Domain;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Bibliotek.Service
{
    public class BookService : IBookService
    {
        SQLConn _connection;
        public BookService(IConfiguration configuration)
        {
            _connection = new SQLConn(configuration);
        }
        public List<Books> GetAllBooks()
        {
            return _connection.GetBooks();
        }
        public Books CreateBook(string title, int authorID, int genreID)
        {
            return _connection.CreateBook(title, authorID, genreID);
        }
        public void DeleteBook(int id)
        {
            _connection.DeleteBook(id);
        }
        public void BorrowBook(int bookID, int loanerID)
        {
            _connection.BorrowBook(bookID, loanerID);
        }
        public void ReturnBook(int bookID)
        {
            _connection.ReturnBook(bookID);
        }
        public string EditBookTitle(int id, string newTitle)
        {
            return _connection.EditBookTitle(id, newTitle);
        }
        public string EditBookAuthor(int id, int newAuthorID)
        {
            return _connection.EditBookAuthor(id, newAuthorID);
        }
        public string AddBookGenre(int bookID, int genreID)
        {
            return _connection.AddBookGenre(bookID, genreID);
        }
    }
}
