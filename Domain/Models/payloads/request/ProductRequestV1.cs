using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.payloads.request
{
    public class ProductRequestV1
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
