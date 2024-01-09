using Bibliotek.Domain;
using Bibliotek.Domain.Models;
using Bibliotek.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.Service.Methods
{
    public class LoanerService : ILoanerService
    {
        SQLConn _connectionString;
        public Loaner CreateLoaner(string name, string email, int number)
        {
            return _connectionString.CreateLoaner(name, email, number);
        }
    }
}
