using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Dto
{
    public class CustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CustomerResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<OrderResponse> Orders { get; set; }
        public virtual List<GenreResponse> FavoriteGenres { get; set; }
    }
}
