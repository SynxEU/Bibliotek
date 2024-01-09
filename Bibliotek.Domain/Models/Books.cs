using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.Domain.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Author_ID { get; set; }
        public int Loaner_ID { get; set; }
    }
}
