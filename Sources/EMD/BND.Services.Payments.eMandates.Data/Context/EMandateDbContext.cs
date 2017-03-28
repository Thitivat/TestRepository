using BND.Services.Payments.eMandates.Models;
using System.Data.Common;
using System.Data.Entity;

namespace BND.Services.Payments.eMandates.Data.Context
{
    /// <summary>
    /// Class EMandateDbContext is a database context implementiing
    /// <a href="https://msdn.microsoft.com/en-us/library/gg696172(v=vs.103).aspx" target="_blank">entity framework.</a>.
    /// </summary>
    public class EMandateDbContext : DbContext
    {
        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealDbContext"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public EMandateDbContext(DbConnection connection)
            : base(connection, true)
        { }

        #endregion

        #region [Properties]
        /// <summary>
        /// Gets or sets the debtor banks.
        /// </summary>
        /// <value>The debtor banks.</value>
        public virtual DbSet<DebtorBank> DebtorBanks { get; set; }
        /// <summary>
        /// Gets or sets the directories.
        /// </summary>
        /// <value>The directories.</value>
        public virtual DbSet<Directory> Directories { get; set; }
        /// <summary>
        /// Gets or sets the eman dates.
        /// </summary>
        /// <value>The eman dates.</value>
        public virtual DbSet<EMandate> EmanDates { get; set; }
        /// <summary>
        /// Gets or sets the enum sequence types.
        /// </summary>
        /// <value>The enum sequence types.</value>
        public virtual DbSet<EnumSequenceType> EnumSequenceTypes { get; set; }
        /// <summary>
        /// Gets or sets the enum transaction types.
        /// </summary>
        /// <value>The enum transaction types.</value>
        public virtual DbSet<EnumTransactionType> EnumTransactionTypes { get; set; }
        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>The logs.</value>
        public virtual DbSet<Log> Logs { get; set; }
        /// <summary>
        /// Gets or sets the raw messages.
        /// </summary>
        /// <value>The raw messages.</value>
        public virtual DbSet<RawMessage> RawMessages { get; set; }
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual DbSet<Setting> Settings { get; set; }
        /// <summary>
        /// Gets or sets the transactions.
        /// </summary>
        /// <value>The transactions.</value>
        public virtual DbSet<Transaction> Transactions { get; set; }
        /// <summary>
        /// Gets or sets the transaction status histories.
        /// </summary>
        /// <value>The transaction status histories.</value>
        public virtual DbSet<TransactionStatusHistory> TransactionStatusHistories { get; set; }
        #endregion

        #region [Methods]

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.</remarks>

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DebtorBank>()
                .Property(e => e.DebtorBankId)
                .IsUnicode(false);

            modelBuilder.Entity<Directory>()
                .HasMany(e => e.DebtorBanks)
                .WithRequired(e => e.Directory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.CreditorAddressLine)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.CreditorID)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.DebtorBankID)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.DebtorIban)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.LocalInstrumentCode)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.MandateRequestID)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.MaxAmount)
                .HasPrecision(11, 2);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.MessageID)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.MessageNameID)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.OriginalMandateID)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.OriginalMessageID)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.SchemeName)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.SequenceType)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.ServiceLevelCode)
                .IsUnicode(false);

            modelBuilder.Entity<EMandate>()
                .Property(e => e.FrequencyPeriod)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EnumSequenceType>()
                .Property(e => e.SequenceTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<EnumSequenceType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EnumSequenceType>()
                .HasMany(e => e.EmanDates)
                .WithRequired(e => e.EnumSequenceType)
                .HasForeignKey(e => e.SequenceType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnumSequenceType>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.EnumSequenceType)
                .HasForeignKey(e => e.SequenceType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnumTransactionType>()
                .Property(e => e.TransactionTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<EnumTransactionType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EnumTransactionType>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.EnumTransactionType)
                .HasForeignKey(e => e.TransactionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Hostname)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.AppName)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.ProcId)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.MsgId)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.StructuredData)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Msg)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Key)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.TransactionID)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.DebtorBankID)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.EMandateID)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.EMandateReason)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.EntranceCode)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.MaxAmount)
                .HasPrecision(11, 2);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.MessageID)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.PurchaseID)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.OriginalDebtorBankID)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.OriginalIban)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.TransactionType)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.SequenceType)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .HasMany(e => e.TransactionStatusHistories)
                .WithRequired(e => e.Transaction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionStatusHistory>()
                .Property(e => e.TransactionID)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionStatusHistory>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionStatusHistory>()
                .HasMany(e => e.Emandates)
                .WithRequired(e => e.TransactionStatusHistory)
                .WillCascadeOnDelete(false);
        }

        #endregion
    }
}
