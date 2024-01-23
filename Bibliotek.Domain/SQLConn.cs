using Bibliotek.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Bibliotek.Domain
{
    public class SQLConn
    {
        private string connectionString;
        public SQLConn(IConfiguration configuration) { connectionString = configuration.GetConnectionString("Default"); }
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
                    Books book = new Books();
                    book.Id = reader.GetInt32("Book_ID");
                    book.Title = reader.GetString("Title");
                    book.Author_ID = reader.GetInt32("Author_ID");
                    if (!reader.IsDBNull(reader.GetOrdinal("Loaner_ID")))
                    {
                        book.Loaner_ID = reader.GetInt32("Loaner_ID");
                    }
                    else
                    {
                        book.Loaner_ID = 0; 
                    }

                    ListOfBook.Add(book);
                }
            }
            return ListOfBook;
        }
        public Author Authors(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("GetAuthorsById", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Author_ID", id);
                com.ExecuteNonQuery();
                Author author = new Author();

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    author = (new Author
                    {
                        Name = reader.GetString("Name"),
                    });
                }
                return author;
            }
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
                com2.Parameters.AddWithValue("@Genre_ID", genreID);
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
        public void DeleteLoaner(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("DeleteLoaner", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Loaner_ID", id);
                com.ExecuteNonQuery();
            }
        }
        public void DeleteAuthor(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("DeleteAuthor", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Author_ID", id);
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
                com1.Parameters.AddWithValue("@Genre_ID", genreID);
                com1.ExecuteNonQuery();
                com2.Parameters.AddWithValue("@Book_ID", bookID);
                SqlDataReader _reader = com2.ExecuteReader();
                
                if (_reader.Read())
                    newGenre = _reader.GetString("Genre");
            }
            return newGenre;
        }
        public Loaner CreateLoaner(string name, string email, string password, int number)
        {
            Loaner loaner = new Loaner();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("CreateLoaner", conn) { CommandType = CommandType.StoredProcedure };

                com.Parameters.AddWithValue("@Name", name);
                com.Parameters.AddWithValue("@Email", email);
                com.Parameters.AddWithValue("@Password", password);
                com.Parameters.AddWithValue("@Number", number);
                com.ExecuteNonQuery();
                loaner.Email = email;
                loaner.Name = name;
                loaner.Password = password;
                loaner.Number = number;
            }
            return loaner;
        }
        public Loaner GetLoaner(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("GetLoaner", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Email", email);
                com.ExecuteNonQuery();

                Loaner loaner = new Loaner();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    loaner = (new Loaner
                    {
                        Id = reader.GetInt32("Loaner_ID"),
                        Name = reader.GetString("Name"),
                        Email = reader.GetString("E-Mail"),
                        Password = reader.GetString("Password"),
                        Number = reader.GetInt32("Number"),
                        Admin = reader.GetBoolean("Admin")
                    });
                }
                return loaner;
            }
        }
        public Loaner GetLoanerById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("GetLoanerById", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Loaner_ID", id);
                com.ExecuteNonQuery();

                Loaner loaner = new Loaner();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    loaner = (new Loaner
                    {
                        Id = reader.GetInt32("Loaner_ID"),
                        Name = reader.GetString("Name"),
                        Email = reader.GetString("E-Mail"),
                        Password = reader.GetString("Password"),
                        Number = reader.GetInt32("Number"),
                        Admin = reader.GetBoolean("Admin")
                    });
                }
                return loaner;
            }
        }
        public Author CreateAuthor(string name, DateTime dateOfBirth)
        {
            Author author = new Author();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("CreateAuthor", conn) { CommandType = CommandType.StoredProcedure };

                com.Parameters.AddWithValue("@Name", name);
                com.Parameters.AddWithValue("@DOB", dateOfBirth);
                com.ExecuteNonQuery();
                author.Name = name;
                author.DOB = dateOfBirth;
            }
            return author;
        }
        public List<Author> GetAuthors() 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Author> author = new List<Author>();
                conn.Open();
                SqlCommand com = new SqlCommand("GetAuthors", conn) { CommandType = CommandType.StoredProcedure };
                com.ExecuteNonQuery();

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    author.Add(new Author
                    {
                        Id = reader.GetInt32("Author_ID"),
                        Name = reader.GetString("Name"),
                        DOB = reader.GetDateTime("DOB")
                    });
                }
                return author;
            }
        }
        public List<Loaner> GetLoaners()
        {
            List<Loaner> listOfLoaner = new List<Loaner>();
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("GetAllLoaner", conn) { CommandType = CommandType.StoredProcedure };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listOfLoaner.Add(new Loaner
                    {
                        Id = reader.GetInt32("Loaner_ID"),
                        Name = reader.GetString("Name"),
                        Email = reader.GetString("E-Mail"),
                        Password = reader.GetString("Password"),
                        Number = reader.GetInt32("Number"),
                        Admin = reader.GetBoolean("Admin")
                    });
                }
            }
            return listOfLoaner;
        }
        public Loaner EditLoanerName(int id, string fullName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("EditLoanerName", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Loaner_ID", id);
                com.Parameters.AddWithValue("@Name", fullName);
                com.ExecuteNonQuery();

                Loaner loaner = new Loaner();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    loaner = (new Loaner
                    {
                        Id = reader.GetInt32("Loaner_ID"),
                        Name = reader.GetString("Name"),
                    });
                }
                return loaner;
            }
        }
        public Loaner EditLoanerEmail(int id, string mail)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("EditLoanerEmail", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Loaner_ID", id);
                com.Parameters.AddWithValue("@Mail", mail);
                com.ExecuteNonQuery();

                Loaner loaner = new Loaner();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    loaner = (new Loaner
                    {
                        Id = reader.GetInt32("Loaner_ID"),
                        Email = reader.GetString("E-Mail"),
                    });
                }
                return loaner;
            }
        }
        public Loaner EditLoanerPassword(int id, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("EditLoanerPassword", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Loaner_ID", id);
                com.Parameters.AddWithValue("@Password", password);
                com.ExecuteNonQuery();

                Loaner loaner = new Loaner();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    loaner = (new Loaner
                    {
                        Id = reader.GetInt32("Loaner_ID"),
                        Password = reader.GetString("Password"),
                    });
                }
                return loaner;
            }
        }
        public Loaner EditLoanerNumber(int id, int number)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("EditLoanerNumber", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Loaner_ID", id);
                com.Parameters.AddWithValue("@Number", number);
                com.ExecuteNonQuery();

                Loaner loaner = new Loaner();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    loaner = (new Loaner
                    {
                        Id = reader.GetInt32("Loaner_ID"),
                        Number = reader.GetInt32("Number"),
                    });
                }
                return loaner;
            }
        }
        public List<Genre> Genres(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Genre> listOfGenres = new List<Genre>();
                conn.Open();
                SqlCommand com = new SqlCommand("GetBooksGenre", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Book_ID", id);
                com.ExecuteNonQuery();

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    listOfGenres.Add(new Genre
                    {
                        GenreName = reader.GetString("Genre"),
                    });
                }
                return listOfGenres;
            }
        }
        public List<Genre> GetGenres()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Genre> listOfGenres = new List<Genre>();
                conn.Open();
                SqlCommand com = new SqlCommand("GetAllGenre", conn) { CommandType = CommandType.StoredProcedure };
                com.ExecuteNonQuery();

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    listOfGenres.Add(new Genre
                    {
                        GenreName = reader.GetString("Genre"),
                    });
                }
                return listOfGenres;
            }
        }
        public bool AddAdmin(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("AddAdmin", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Loaner_ID", id);
                com.Parameters.AddWithValue("@Admin", true);
                com.ExecuteNonQuery();

                return true;
            }
        }
        public bool RemoveAdmin(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("AddAdmin", conn) { CommandType = CommandType.StoredProcedure };
                com.Parameters.AddWithValue("@Loaner_ID", id);
                com.Parameters.AddWithValue("@Admin", false);
                com.ExecuteNonQuery();

                return true;
            }
        }
        public Author EditAuthorName(int id, string name)
        {
            Author author = new Author();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("EditAuthorName", conn) { CommandType = CommandType.StoredProcedure };

                com.Parameters.AddWithValue("@Author_ID", id);
                com.Parameters.AddWithValue("@Name", name);
                com.ExecuteNonQuery();
                author.Name = name;
            }
            return author;
        }
        public Author EditAuthorDOB(int id, DateTime dateOfBirth)
        {
            Author author = new Author();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand com = new SqlCommand("EditAuthorDOB", conn) { CommandType = CommandType.StoredProcedure };

                com.Parameters.AddWithValue("@Author_ID", id);
                com.Parameters.AddWithValue("@DOB", dateOfBirth);
                com.ExecuteNonQuery();
                author.DOB = dateOfBirth;
            }
            return author;
        }
    }
}
