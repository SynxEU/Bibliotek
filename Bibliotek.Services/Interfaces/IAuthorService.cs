using Bibliotek.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.Service.Interfaces
{
    public interface IAuthorService
    {
        Author CreateAuthor(string name, DateTime dateOfBirth);
        List<Author> GetAuthors();
        Author EditAuthorName(int id, string name);
        Author EditAuthorDOB(int id, DateTime dateOfBirth);
        void DeleteAuthor(int id);
        List<Author> Authors(int id);
    }
}
