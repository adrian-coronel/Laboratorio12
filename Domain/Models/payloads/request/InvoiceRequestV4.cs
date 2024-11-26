
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.payloads.request
{
    public class InvoiceRequestV4
    {
        public int InvoiceId { get; set; }    
        public List<DetailRequestV1> Details { get; set; }
    }
}
