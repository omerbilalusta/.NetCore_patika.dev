using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Dto
{
    public class DirectorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class DirectorResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<MovieResponse> Movies { get; set; }
    }
}
