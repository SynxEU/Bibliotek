using Bibliotek.Domain;
using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.Service.Methods
{
    public class LoanerService : ILoanerService
    {
        SQLConn _connection;
        public LoanerService(IConfiguration configuration)
        {
            _connection = new SQLConn(configuration);
        }
        public Loaner CreateLoaner(string name, string email, int number)
        {
            return _connection.CreateLoaner(name, email, number);
        }
    }
}
