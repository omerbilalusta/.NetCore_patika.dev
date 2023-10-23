using Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public class Movie_Order : BaseModel
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
