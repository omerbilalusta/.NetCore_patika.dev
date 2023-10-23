using Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public class Genre_Customer : BaseModel
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
