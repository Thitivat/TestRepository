using BND.Services.Payments.iDeal.ClientData.Dal.Pocos;
using System.Data.Common;
using System.Data.Entity;

namespace BND.Services.Payments.iDeal.ClientData
{
    /// <summary>
    /// Class ClientDataContext.
    /// </summary>
    public partial class ClientDataDbContext : DbContext
    {
        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="JobQueueDbContext" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ClientDataDbContext(DbConnection connection)
            : base(connection, true)
        {
            Database.SetInitializer<ClientDataDbContext>(null);
        }

        #endregion

        #region [Properties]
        /// <summary>
        /// Gets or sets the client users.
        /// </summary>
        /// <value>The client users.</value>
        public virtual DbSet<p_ClientUser> ClientUsers { get; set; }
        /// <summary>
        /// Gets or sets the product bank accounts.
        /// </summary>
        /// <value>The product bank accounts.</value>
        public virtual DbSet<p_ProductBankAccount> ProductBankAccounts { get; set; }
        /// <summary>
        /// Gets or sets the product clients.
        /// </summary>
        /// <value>The product clients.</value>
        public virtual DbSet<p_ProductClient> ProductClients { get; set; }
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public virtual DbSet<p_Product> Products { get; set; }
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
            modelBuilder.Entity<p_ClientUser>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<p_ClientUser>()
                .Property(e => e.MaritalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<p_ClientUser>()
                .Property(e => e.Burgerservicenummer)
                .HasPrecision(18, 0);

            modelBuilder.Entity<p_ClientUser>()
                .Property(e => e.InsertedBy)
                .IsUnicode(false);

            modelBuilder.Entity<p_ClientUser>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<p_ClientUser>()
                .Property(e => e.Nationality)
                .IsUnicode(false);

            modelBuilder.Entity<p_ClientUser>()
                .Property(e => e.TaxCountry)
                .IsUnicode(false);

            modelBuilder.Entity<p_ClientUser>()
                .HasMany(e => e.ProductClients)
                .WithRequired(e => e.ClientUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_ProductBankAccount>()
                .Property(e => e.Label)
                .IsUnicode(false);

            modelBuilder.Entity<p_ProductBankAccount>()
                .Property(e => e.MatrixIban)
                .IsUnicode(false);

            modelBuilder.Entity<p_ProductBankAccount>()
                .Property(e => e.MatrixProductName)
                .IsUnicode(false);

            modelBuilder.Entity<p_ProductBankAccount>()
                .Property(e => e.InsertedBy)
                .IsUnicode(false);

            modelBuilder.Entity<p_ProductBankAccount>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<p_ProductBankAccount>()
                .Property(e => e.PaymentRecurringInterval)
                .IsUnicode(false);

            modelBuilder.Entity<p_ProductBankAccount>()
                .Property(e => e.PaymentRecurringMethod)
                .IsUnicode(false);

            modelBuilder.Entity<p_ProductBankAccount>()
                .Property(e => e.PaymentSingleMethod)
                .IsUnicode(false);

            modelBuilder.Entity<p_ProductClient>()
                .Property(e => e.ClientUserType)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.ProductGroup)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.InsertedBy)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.ServiceChannelType)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.BankAccountPaymentsName)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.BankIBAN)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.BankBIC)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.ClientType)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.AppointmentUser)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.ProductFlowStatus)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.ProductType)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.IBANPayments)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.BICPayments)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.IBANPayout)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.BICPayout)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .Property(e => e.IncomingChannelType)
                .IsUnicode(false);

            modelBuilder.Entity<p_Product>()
                .HasMany(e => e.ProductBankAccounts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Product>()
                .HasMany(e => e.ProductClients)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Product>()
                .HasMany(e => e.Products1)
                .WithOptional(e => e.Product1)
                .HasForeignKey(e => e.ParentProductId);
        }
        #endregion
    }
}
