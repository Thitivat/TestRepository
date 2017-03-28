using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Payments.iDeal.Dal.Pocos
{
    /// <summary>
    /// Class p_Log representing Log table in iDeal database.
    /// </summary>
    [Table("ideal.Log")]
    public partial class p_Log
    {
        /// <summary>
        /// Gets or sets the log identifier.
        /// </summary>
        /// <value>The log identifier.</value>
        [Key]
        public int LogId { get; set; }

        /// <summary>
        /// Gets or sets the prival.
        /// </summary>
        /// <value>The prival.</value>
        public byte? Prival { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public byte? Version { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the hostname.
        /// </summary>
        /// <value>The hostname.</value>
        [StringLength(255)]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        [StringLength(48)]
        public string AppName { get; set; }

        /// <summary>
        /// Gets or sets the process identifier.
        /// </summary>
        /// <value>The process identifier.</value>
        [StringLength(128)]
        public string ProcId { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        [StringLength(32)]
        public string MsgId { get; set; }

        /// <summary>
        /// Gets or sets the structured data.
        /// </summary>
        /// <value>The structured data.</value>
        public string StructuredData { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Msg { get; set; }
    }
}
