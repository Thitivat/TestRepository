using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Payments.iDeal.JobQueue.Dal.Pocos
{
    /// <summary>
    /// Class p_JobList.
    /// </summary>
    [Table("job.JobList")]
    public partial class p_JobList
    {
        /// <summary>
        /// Gets or sets the job list identifier.
        /// </summary>
        /// <value>The job list identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobListID { get; set; }

        /// <summary>
        /// Gets or sets the name of the job class.
        /// </summary>
        /// <value>The name of the job class.</value>
        [Required]
        [StringLength(100)]
        public string JobClassName { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the job parameters.
        /// </summary>
        /// <value>The job parameters.</value>
        [StringLength(1000)]
        public string JobParameters { get; set; }

        /// <summary>
        /// Gets or sets the started by.
        /// </summary>
        /// <value>The started by.</value>
        [Required]
        [StringLength(50)]
        public string StartedBy { get; set; }

        /// <summary>
        /// Gets or sets the inserted.
        /// </summary>
        /// <value>The inserted.</value>
        public DateTime Inserted { get; set; }

        /// <summary>
        /// Gets or sets the started.
        /// </summary>
        /// <value>The started.</value>
        public DateTime? Started { get; set; }

        /// <summary>
        /// Gets or sets the completed.
        /// </summary>
        /// <value>The completed.</value>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Gets or sets the job priority identifier.
        /// </summary>
        /// <value>The job priority identifier.</value>
        public int JobPriorityID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="p_JobList"/> is parallel.
        /// </summary>
        /// <value><c>true</c> if parallel; otherwise, <c>false</c>.</value>
        public bool Parallel { get; set; }

        /// <summary>
        /// Gets or sets the synchronize chain identifier.
        /// </summary>
        /// <value>The synchronize chain identifier.</value>
        public int? SyncChainID { get; set; }

        /// <summary>
        /// Gets or sets the job status identifier.
        /// </summary>
        /// <value>The job status identifier.</value>
        public int JobStatusID { get; set; }
    }
}
