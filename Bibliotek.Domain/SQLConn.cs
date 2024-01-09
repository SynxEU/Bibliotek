using Bibliotek.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Bibliotek.Domain
{
    public class SQLConn
    {
        private string connectionString;
        public SQLConn (IConfiguration configuration) { connectionString = configuration.GetConnectionString("Default"); }
        public List<Books> GetBooks()
        {
            List<Books> ListOfBook = new List<Books>();
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("GetAllBooks", conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    ListOfBook.Add(new Books
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Author_ID = reader.GetInt32(2),
                        Loaner_ID = reader.GetInt32(3),
                    });
                }
            }
            return ListOfBook; 
        }
        public Books CreateBook(string title, int authorID, int genreID)
        { 
            Books Book = new Books();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com1 = new SqlCommand("CreateBook", conn) { CommandType = CommandType.StoredProcedure };
                SqlCommand com2 = new SqlCommand("CreateGenreConnection", conn) { CommandType = CommandType.StoredProcedure };

                com1.Parameters.AddWithValue("@Title", title);
                com1.Parameters.AddWithValue("@Author_ID", authorID);
                com2.Parameters.AddWithValue("Genre_ID", genreID);
                com1.ExecuteNonQuery();
                com2.ExecuteNonQuery();

            }
            return Book;
        }
        public void DeleteBook(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("DeleteBook", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Book_ID", id);
                com.ExecuteNonQuery();
            }
        }
        public void BorrowBook(int bookID, int loanerID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("BorrowBook", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Book_ID", bookID);
                com.Parameters.AddWithValue("Loaner_ID", loanerID);
                com.ExecuteNonQuery();
            }
        }
        public void ReturnBook(int bookID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("ReturnBook", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Book_ID", bookID);
                com.ExecuteNonQuery();
            }
        }
        public string EditBookTitle(int id, string newTitle)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com1 = new SqlCommand("EditBookAuthor", conn) { CommandType = CommandType.StoredProcedure };
                com1.Parameters.AddWithValue("@Book_ID", id);
                com1.Parameters.AddWithValue("@Title", newTitle);
                com1.ExecuteNonQuery();
            }
            return newTitle;
        }
        public string EditBookAuthor(int id, int newAuthorID)
        {
            string newAuthor = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com1 = new SqlCommand("EditBookAuthor", conn) { CommandType = CommandType.StoredProcedure };
                SqlCommand com2 = new SqlCommand("GetAuthorName", conn) { CommandType = CommandType.StoredProcedure };
                com1.Parameters.AddWithValue("@Book_ID", id);
                com1.Parameters.AddWithValue("@Author_ID", newAuthorID);
                com1.ExecuteNonQuery();
                SqlDataReader _reader = com2.ExecuteReader();
                if (_reader.Read())
                    newAuthor = _reader.GetString("Name");
            }
            return newAuthor;
        }
        public string AddBookGenre(int bookID, int genreID)
        {
            string newGenre = string.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com1 = new SqlCommand("AddBookGenre", conn) { CommandType = CommandType.StoredProcedure };
                SqlCommand com2 = new SqlCommand("GetBooksGenre", conn) { CommandType = CommandType.StoredProcedure };
                com1.Parameters.AddWithValue("@Book_ID", bookID);
                com1.Parameters.AddWithValue("@Author_ID", genreID);
                com1.ExecuteNonQuery();
                SqlDataReader _reader = com2.ExecuteReader();
                com2.Parameters.AddWithValue("@Book_ID", bookID);
                if (_reader.Read())
                    newGenre = _reader.GetString("Genre");
            }
            return newGenre;
        }
    }
}
