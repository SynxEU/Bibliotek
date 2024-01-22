using Bibliotek.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.Service.Interfaces
{
    public interface ILoanerService
    {
        Loaner CreateLoaner(string name, string email, string password, int number);
        Loaner GetLoaner(string email);
        List<Loaner> GetLoaners();
        Loaner EditLoanerName(int id, string fullName);
        Loaner EditLoanerEmail(int id, string mail);
        Loaner EditLoanerPassword(int id, string password);
        Loaner EditLoanerNumber(int id, int number);
        Loaner GetLoanerById(int id);
        bool AddAdmin(int id);
        bool RemoveAdmin(int id);
        void DeleteLoaner(int id);

    }
}
