using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.payloads.request
{
    public class InvoiceRequestV1
    {
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public int InvoiceNumber { get; set; }
        public float Total { get; set; }
    }
}
