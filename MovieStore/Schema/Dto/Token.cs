using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Dto
{
    public class TokenRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class TokenResponse
    {
        public DateTime ExpireDate { get; set; }
        public string Token { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
