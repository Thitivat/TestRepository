using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Business.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
