using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BND.Services.Payments.eMandates.Entities
{
    public class NewTransactionModel
    {
        public string DebtorBankId { get; set; }

        public string DebtorReference { get; set; }

        public string EMandateId { get; set; }

        public string EMandateReason { get; set; }

        public TimeSpan? ExpirationPeriod { get; set; }

        public string Language { get; set; }

        public decimal? MaxAmount { get; set; }

        public string PurchaseId { get; set; }

        public string SequenceType { get; set; }
    }
}
