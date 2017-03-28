namespace BND.Services.Payments.PaymentIdInfo.Entities
{
    using System;

    public partial class iDealTransactionStatusHistory
    {
        public int TransactionStatusHistoryID { get; set; }

        public string TransactionID { get; set; }

        public string Status { get; set; }

        public DateTime StatusRequestDateTimeStamp { get; set; }

        public DateTime StatusResponseDateTimeStamp { get; set; }

        public DateTime? StatusDateTimeStamp { get; set; }

        public virtual iDealTransaction Transaction { get; set; }
    }
}
