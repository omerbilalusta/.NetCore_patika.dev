using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Dto
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
    }
    public class OrderResponse
    {
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<MovieResponse> Movies { get; set; }
    }
}
