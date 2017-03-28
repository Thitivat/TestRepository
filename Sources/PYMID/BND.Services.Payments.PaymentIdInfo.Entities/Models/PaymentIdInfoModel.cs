using System;

namespace BND.Services.Payments.PaymentIdInfo.Entities
{
    public class PaymentIdInfoModel
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionId { get; set; }
        public string BndIban { get; set; }
        public string SourceIban { get; set; }
        public string SourceAccountHolderName { get; set; }
        public string SourceBic { get; set; }
    }
}
