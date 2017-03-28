using System;

namespace BND.Services.IbanStore.Models
{
    /// <summary>
    /// Class IbanHistory.
    /// </summary>
    public class IbanHistory
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public string Context { get; set; }

        /// <summary>
        /// Gets or sets the changed by.
        /// </summary>
        /// <value>The changed by.</value>
        public string ChangedBy { get; set; }

        /// <summary>
        /// Gets or sets the changed date.
        /// </summary>
        /// <value>The changed date.</value>
        public DateTime ChangedDate { get; set; }
    }
}
