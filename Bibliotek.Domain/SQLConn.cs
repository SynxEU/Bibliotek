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
        public Books CreateBook(string title, int id)
        { 
            Books Book = new Books();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("CreateBook", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Title", title);
                com.Parameters.AddWithValue("@Author_ID", id);
                com.ExecuteNonQuery();
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
            return newTitle;
        }
        public string EditBookAuthor(int id, int newAuthorID)
        {
            string newAuthor = string.Empty;
            return newAuthor;
        }
        public string AddBookGenre(int bookID, int genreID)
        {
            string newGenre = string.Empty;
            return newGenre;
        }
    }
}
