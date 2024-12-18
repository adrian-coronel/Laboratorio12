﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public bool Enabled { get; set; }

        
        public virtual List<Invoice> Invoices { get; set; }
    }
}
