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
    }
}
