using System;

namespace BND.Services.Payments.iDeal.Models
{
    /// <summary>
    /// Class BookingModel.
    /// </summary>
    public class BookingModel
    {
        /// <summary>
        /// Gets or sets the book from iban.
        /// </summary>
        /// <value>The book from iban.</value>
        public string BookFromIban { get; set; }
        /// <summary>
        /// Gets or sets the book to iban.
        /// </summary>
        /// <value>The book to iban.</value>
        public string BookToIban { get; set; }
        /// <summary>
        /// Gets or sets the book to bic.
        /// </summary>
        /// <value>The book to bic.</value>
        public string BookToBic { get; set; }
        ///// <summary>
        ///// Gets or sets the transaction identifier.
        ///// </summary>
        ///// <value>The transaction identifier.</value>
        //public string TransactionId { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount { get; set; }
        /// <summary>
        /// Gets or sets the book date.
        /// </summary>
        /// <value>The book date.</value>
        public DateTime BookDate { get; set; }
        /// <summary>
        /// Gets or sets the debtor.
        /// </summary>
        /// <value>The debtor.</value>
        public Contract Debtor { get; set; }
        /// <summary>
        /// Gets or sets the creditor.
        /// </summary>
        /// <value>The creditor.</value>
        public Contract Creditor { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

    }
}