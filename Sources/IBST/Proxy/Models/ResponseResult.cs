using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bndb.IBanStore.Proxy.Models
{
    public class ResponseResult
    {
        public int StatusCode { get; set; }
        public Iban Iban { get; set; }
    }
}
