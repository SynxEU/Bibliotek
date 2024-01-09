using Bibliotek.Domain.Models;
using Bibliotek.Domain;
using Bibliotek.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Bibliotek.Service.Methods
{
    public class AuthorService : IAuthorService
    {
        SQLConn _connection;
        public AuthorService(IConfiguration configuration)
        {
            _connection = new SQLConn(configuration);
        }
        public Author CreateAuthor(string name, DateTime dateOfBirth)
        {
            return _connection.CreateAuthor(name, dateOfBirth);
        }
    }
}
