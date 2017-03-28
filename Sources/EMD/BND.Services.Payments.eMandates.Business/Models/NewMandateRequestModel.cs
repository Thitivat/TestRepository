using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Business.Models
{
    public class NewMandateRequestModel
    {
        /// <summary>
        /// Gets or sets the entrance code.
        /// </summary>
        /// <value>The entrance code.</value>
        public string EntranceCode { get; set; }
        /// <summary>
        /// This field enables the debtor bank's site to select the debtor's preferred language (e.g. the language selected on the creditor's site),
        ///             if the debtor bank's site supports this: Dutch = 'nl', English = 'en'
        /// 
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Optional: The period of validity of the transaction request as stated by the creditor measured from the receipt by the debtor bank.
        ///             The debtor must authorise the transaction within this period.
        /// 
        /// </summary>
        public TimeSpan? ExpirationPeriod { get; set; }
        /// <summary>
        /// Message ID for pain message
        /// 
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// BIC of the Debtor Bank
        /// 
        /// </summary>
        public string DebtorBankId { get; set; }
        /// <summary>
        /// ID that identifies the mandate and is issued by the creditor
        /// 
        /// </summary>
        public string EMandateId { get; set; }
        /// <summary>
        /// Indicates type of eMandate: one-off or sequenceType direct debit.
        /// 
        /// </summary>
        public EnumSequenceType SequenceType { get; set; }
        /// <summary>
        /// Reason of the mandate
        /// 
        /// </summary>
        public string EMandateReason { get; set; }
        /// <summary>
        /// Reference ID that identifies the debtor to creditor, which is issued by the creditor
        /// 
        /// </summary>
        public string DebtorReference { get; set; }
        /// <summary>
        /// A purchaseID that acts as a reference from eMandate to the purchase-order
        /// 
        /// </summary>
        public string PurchaseId { get; set; }
        /// <summary>
        /// Maximum amount. Not allowed for Core, optional for B2B.
        /// 
        /// </summary>
        public decimal? MaxAmount { get; set; }
    }
}
