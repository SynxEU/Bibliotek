﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek.Domain.Models
{
    public class Loaner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Number { get; set; }
    }
}
