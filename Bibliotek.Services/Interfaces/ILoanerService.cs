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
        Loaner CreateLoaner(string name, string email, int number);
    }
}
