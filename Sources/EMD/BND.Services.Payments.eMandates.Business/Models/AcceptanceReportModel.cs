using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.Payments.eMandates.Business.Models
{
    public class AcceptanceReportModel
    {
        /// <summary>
        /// Message Identification
        /// 
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// Message timestamp
        /// 
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Validation reference
        /// 
        /// </summary>
        public string ValidationReference { get; set; }
        /// <summary>
        /// Original Message ID
        /// 
        /// </summary>
        public string OriginalMessageId { get; set; }
        /// <summary>
        /// Refers to the type of validation request that preceded the acceptance report
        /// 
        /// </summary>
        public string MessageNameId { get; set; }
        /// <summary>
        /// Whether or not the mandate is accepted by the debtor
        /// 
        /// </summary>
        public bool AcceptedResult { get; set; }
        /// <summary>
        /// Original mandate ID
        /// 
        /// </summary>
        public string OriginalMandateId { get; set; }
        /// <summary>
        /// Mandate request ID
        /// 
        /// </summary>
        public string MandateRequestId { get; set; }
        /// <summary>
        /// SEPA
        /// 
        /// </summary>
        public string ServiceLevelCode { get; set; }
        /// <summary>
        /// Core or B2B
        /// 
        /// </summary>
        public Instrumentation LocalInstrumentCode { get; set; }
        /// <summary>
        /// Sequence Type: recurring or one-off
        /// 
        /// </summary>
        public EnumSequenceType SequenceType { get; set; }
        /// <summary>
        /// Maximum amount
        /// 
        /// </summary>
        public decimal MaxAmount { get; set; }
        /// <summary>
        /// Reason for eMandate
        /// 
        /// </summary>
        public string EMandateReason { get; set; }
        /// <summary>
        /// Direct Debit ID of the Creditor
        /// 
        /// </summary>
        public string CreditorId { get; set; }
        /// <summary>
        /// SEPA
        /// 
        /// </summary>
        public string SchemeName { get; set; }
        /// <summary>
        /// Name of the Creditor
        /// 
        /// </summary>
        public string CreditorName { get; set; }
        /// <summary>
        /// Country of the postal address of the Creditor
        /// 
        /// </summary>
        public string CreditorCountry { get; set; }
        /// <summary>
        /// The Creditor’s address: P.O. Box or street name + building + add-on + Postcode + City.
        ///             Second Address line only to be used if 70 chars are exceeded in the first line
        /// 
        /// </summary>
        public string[] CreditorAddressLine { get; set; }
        /// <summary>
        /// Name of the company (or daughter-company, or label etc.) for which the Creditor is processing eMandates.
        ///             May only be used when meaningfully different from CreditorName
        /// 
        /// </summary>
        public string CreditorTradeName { get; set; }
        /// <summary>
        /// Account holder name of the account that is used for the eMandate
        /// 
        /// </summary>
        public string DebtorAccountName { get; set; }
        /// <summary>
        /// Reference ID that identifies the Debtor to the Creditor. Issued by the Creditor
        /// 
        /// </summary>
        public string DebtorReference { get; set; }
        /// <summary>
        /// Debtor’s bank account number
        /// 
        /// </summary>
        public string DebtorIban { get; set; }
        /// <summary>
        /// BIC of the Debtor bank
        /// 
        /// </summary>
        public string DebtorBankId { get; set; }
        /// <summary>
        /// Name of the person signing the eMandate. In case of multiple signing, all signer names must be included in this field, separated by commas.
        ///             If the total would exceed the maximum of 70 characters, the names are cut off at 65 characters and “e.a.” is added after the last name.
        /// 
        /// </summary>
        public string DebtorSignerName { get; set; }
        /// <summary>
        /// The response XML
        /// 
        /// </summary>
        public string RawMessage { get; set; }
    }
}
