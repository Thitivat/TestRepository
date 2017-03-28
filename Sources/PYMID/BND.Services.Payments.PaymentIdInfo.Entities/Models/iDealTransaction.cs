namespace BND.Services.Payments.PaymentIdInfo.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class iDealTransaction
    {
        public iDealTransaction()
        {
            TransactionStatusHistories = new HashSet<iDealTransactionStatusHistory>();
        }

        public string TransactionID { get; set; }

        public string AcquirerID { get; set; }

        public string MerchantID { get; set; }

        public int? SubID { get; set; }

        public string IssuerID { get; set; }

        public string IssuerAuthenticationURL { get; set; }

        public string MerchantReturnURL { get; set; }

        public string EntranceCode { get; set; }

        public string PurchaseID { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string ConsumerName { get; set; }

        public string ConsumerIBAN { get; set; }

        public string ConsumerBIC { get; set; }

        public DateTime TransactionRequestDateTimestamp { get; set; }

        public DateTime TransactionResponseDateTimestamp { get; set; }

        public DateTime TransactionCreateDateTimestamp { get; set; }

        public string BNDIBAN { get; set; }

        public string PaymentType { get; set; }

        public int ExpirationSecondPeriod { get; set; }

        public string ExpectedCustomerIBAN { get; set; }

        public bool IsSystemFail { get; set; }

        public int TodayAttempts { get; set; }

        public DateTime? LatestAttemptsDateTimestamp { get; set; }

        public virtual ICollection<iDealTransactionStatusHistory> TransactionStatusHistories { get; set; }
    }
}
