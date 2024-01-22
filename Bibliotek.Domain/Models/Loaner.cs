using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.Domain.Models
{
    public class Loaner
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Password { get; set; } = string.Empty;
        public bool Admin { get; set; }
    }
}
