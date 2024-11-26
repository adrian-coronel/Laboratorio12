using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.payloads.request
{
    public class ProductRequestV2
    {
        public int ProductId { get; set; }
        public float Price { get; set; }
    }
}
