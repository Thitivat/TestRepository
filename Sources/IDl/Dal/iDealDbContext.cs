namespace BND.Services.Payments.iDeal.Dal
{
    using BND.Services.Payments.iDeal.Dal.Pocos;
    using System.Data.Common;
    using System.Data.Entity;

    /// <summary>
    /// Class iDealDbContext is a database context implementiing
    /// <a href="https://msdn.microsoft.com/en-us/library/gg696172(v=vs.103).aspx" target="_blank">entity framework.</a>.
    /// </summary>
    public partial class iDealDbContext : DbContext
    {
        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="iDealDbContext"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public iDealDbContext(DbConnection connection)
            : base(connection, true)
        { }

        #endregion


        #region [Properties]

        /// <summary>
        /// Gets or sets the directories from database.
        /// </summary>
        /// <value>The directories.</value>
        public virtual DbSet<p_Directory> Directories { get; set; }

        /// <summary>
        /// Gets or sets the issuers from database.
        /// </summary>
        /// <value>The issuers.</value>
        public virtual DbSet<p_Issuer> Issuers { get; set; }

        /// <summary>
        /// Gets or sets the logs from database.
        /// </summary>
        /// <value>The logs.</value>
        public virtual DbSet<p_Log> Logs { get; set; }
        /// <summary>
        /// Gets or sets the transactions from database.
        /// </summary>
        /// <value>The transactions.</value>
        public virtual DbSet<p_Transaction> Transactions { get; set; }
        /// <summary>
        /// Gets or sets the settings from database.
        /// </summary>
        /// <value>The settings.</value>
        public virtual DbSet<p_Setting> Settings { get; set; }

        /// <summary>
        /// Gets or sets the transaction histories from database.
        /// </summary>
        /// <value>The transaction histories.</value>
        public virtual DbSet<p_TransactionStatusHistory> TransactionStatusHistories { get; set; }
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
            // Setups directory poco.
            modelBuilder.Entity<p_Directory>()
                .Property(e => e.AcquirerID)
                .IsFixedLength()
                .IsUnicode(false);
            modelBuilder.Entity<p_Directory>()
                .HasMany(e => e.Issuers)
                .WithRequired(e => e.Directory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Issuer>()
                .Property(e => e.AcquirerID)
                .IsFixedLength()
                .IsUnicode(false);
            modelBuilder.Entity<p_Issuer>()
                .Property(e => e.CountryNames)
                .IsFixedLength();
            modelBuilder.Entity<p_Issuer>()
                .Property(e => e.IssuerID)
                .IsUnicode(false);

            // Setups log poco.
            // For unit test with effort.
            if (base.Database.Connection is System.Data.SqlClient.SqlConnection)
            {
                modelBuilder.Entity<p_Log>()
                    .Property(e => e.Timestamp)
                    .HasColumnType("datetime2");
            }
            modelBuilder.Entity<p_Log>()
                .Property(e => e.Hostname)
                .IsUnicode(false);
            modelBuilder.Entity<p_Log>()
                .Property(e => e.AppName)
                .IsUnicode(false);
            modelBuilder.Entity<p_Log>()
                .Property(e => e.ProcId)
                .IsUnicode(false);
            modelBuilder.Entity<p_Log>()
                .Property(e => e.MsgId)
                .IsUnicode(false);
            modelBuilder.Entity<p_Log>()
                .Property(e => e.StructuredData)
                .IsUnicode(false);
            modelBuilder.Entity<p_Log>()
                .Property(e => e.Msg)
                .IsUnicode(false);


            // Setups transaction poco.
            modelBuilder.Entity<p_Transaction>()
               .Property(e => e.TransactionID)
               .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.AcquirerID)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.MerchantID)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.IssuerID)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.IssuerAuthenticationURL)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.MerchantReturnURL)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.EntranceCode)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.PurchaseID)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.Amount)
                .HasPrecision(12, 2);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.Currency)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.Language)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.ConsumerIBAN)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.ConsumerBIC)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.PaymentType)
                .IsUnicode(false);
            modelBuilder.Entity<p_Transaction>()
                .Property(e => e.ExpectedCustomerIBAN)
                .IsUnicode(false);

            // Setups transaction status history poco.
            modelBuilder.Entity<p_Transaction>()
                .HasMany(e => e.TransactionStatusHistories)
                .WithRequired(e => e.Transaction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_TransactionStatusHistory>()
                .Property(e => e.TransactionID)
                .IsUnicode(false);

            // Setups setting poco.
            modelBuilder.Entity<p_Setting>()
                .Property(e => e.Key)
                .IsUnicode(false);
            modelBuilder.Entity<p_Setting>()
                .Property(e => e.Value)
                .IsUnicode(false);
        }

        #endregion
    }
}
