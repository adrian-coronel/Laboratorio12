using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.payloads.request
{
    public class CustomerRequestV3
    {
        public int CustomerId { get; set; }
        public List<InvoiceRequestV1> Invoices { get; set; }
    }
}
