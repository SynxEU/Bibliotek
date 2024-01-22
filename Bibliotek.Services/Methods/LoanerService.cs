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

        public LoanerService(IConfiguration configuration) { _connection = new SQLConn(configuration); }

        public Loaner CreateLoaner(string name, string email, string password, int number) { return _connection.CreateLoaner(name, email, password, number); }

        public Loaner GetLoaner(string email) { return _connection.GetLoaner(email); }
        public Loaner GetLoanerById(int id) { return _connection.GetLoanerById(id); }
        public List<Loaner> GetLoaners() { return _connection.GetLoaners(); }
        public Loaner EditLoanerName(int id, string fullName) { return _connection.EditLoanerName(id, fullName); }
        public Loaner EditLoanerEmail(int id, string mail) { return _connection.EditLoanerEmail(id, mail); }
        public Loaner EditLoanerPassword(int id, string password) { return _connection.EditLoanerPassword(id, password); }
        public Loaner EditLoanerNumber(int id, int number) { return _connection.EditLoanerNumber(id, number); }
        public bool AddAdmin(int id) { return _connection.AddAdmin(id); }
        public bool RemoveAdmin(int id) { return _connection.RemoveAdmin(id); }
        public void DeleteLoaner(int id) { _connection.DeleteLoaner(id); }
    }
}
