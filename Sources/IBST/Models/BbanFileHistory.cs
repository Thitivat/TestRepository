using System;
namespace BND.Services.IbanStore.Models
{
    public class BbanFileHistory
    {
        /// <summary>
        ///  Id from database table BbanFileHistory.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// latest status from database
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }

        /// <summary>
        /// latest changed name by user
        /// </summary>
        /// <value>The changed by.</value>
        public string ChangedBy { get; set; }

        /// <summary>
        /// latest changed date
        /// </summary>
        /// <value>The changed date.</value>
        public DateTime ChangedDate { get; set; }

        /// <summary>
        ///  create by system name
        /// </summary>
        /// <value>The context.</value>
        public string Context { get; set; }
    }
}