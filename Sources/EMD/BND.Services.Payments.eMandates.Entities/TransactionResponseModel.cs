using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BND.Services.Payments.eMandates.Entities
{
    public class TransactionResponseModel
    {
        public string TransactionId { get; set; }

        public string IssuerAuthenticationUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime TransactionRequestDateTimeStamp { get; set; }
    }
}
