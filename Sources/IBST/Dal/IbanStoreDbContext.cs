
using System.Data.Common;
using System.Data.Entity;
using Bndb.IBanStore.Dal.Pocos;

/// <summary>
/// The Bndb.IBanStore.Dal namespace contains all classes about data access layer of IBanStore.
/// </summary>
namespace Bndb.IBanStore.Dal
{
    /// <summary>
    /// Class IbanStoreDbContext is an entity framework database context that inherited from <see cref="System.Data.Entity.DbContext"/> class.
    /// </summary>
    /// <remarks>
    /// This class is internal class you cannot use outside.
    /// </remarks>
    public partial class IbanStoreDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IbanStoreDbContext"/> class.
        /// </summary>
        public IbanStoreDbContext(DbConnection connection)
            : base(connection, true)
        {
        }

        /// <summary>
        /// Gets or sets the bban file.
        /// </summary>
        /// <value>The bban file.</value>
        public virtual DbSet<p_BbanFile> BbanFile { get; set; }

        /// <summary>
        /// Gets or sets the bban file history.
        /// </summary>
        /// <value>The bban file history.</value>
        public virtual DbSet<p_BbanFileHistory> BbanFileHistory { get; set; }

        /// <summary>
        /// Gets or sets the bban file import.
        /// </summary>
        /// <value>The bban file import.</value>
        public virtual DbSet<p_BbanFileImport> BbanFileImport { get; set; }

        /// <summary>
        /// Gets or sets the enum bban file status.
        /// </summary>
        /// <value>The enum bban file status.</value>
        public virtual DbSet<p_EnumBbanFileStatus> EnumBbanFileStatus { get; set; }

        /// <summary>
        /// Gets or sets the enum i ban status.
        /// </summary>
        /// <value>The enum i ban status.</value>
        public virtual DbSet<p_EnumIbanStatus> EnumIBanStatus { get; set; }

        /// <summary>
        /// Gets or sets the i ban.
        /// </summary>
        /// <value>The i ban.</value>
        public virtual DbSet<p_Iban> IBan { get; set; }

        /// <summary>
        /// Gets or sets the i ban history.
        /// </summary>
        /// <value>The i ban history.</value>
        public virtual DbSet<p_IbanHistory> IBanHistory { get; set; }

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
            modelBuilder.Entity<p_BbanFile>()
                .Property(e => e.Hash)
                .IsUnicode(false);

            modelBuilder.Entity<p_BbanFile>()
                .HasMany(e => e.BbanFileHistory)
                .WithRequired(e => e.BbanFile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_BbanFile>()
                .HasMany(e => e.BbanFileImport)
                .WithRequired(e => e.BbanFile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_BbanFile>()
                .HasMany(e => e.IBan)
                .WithRequired(e => e.BbanFile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_BbanFileHistory>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<p_BbanFileHistory>()
                .Property(e => e.Context)
                .IsUnicode(false);

            modelBuilder.Entity<p_BbanFileHistory>()
                .Property(e => e.ChangedBy)
                .IsUnicode(false);

            modelBuilder.Entity<p_BbanFileImport>()
                .Property(e => e.Bban)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumBbanFileStatus>()
                .Property(e => e.BbanFileStatus)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumBbanFileStatus>()
                .HasMany(e => e.BbanFileHistory)
                .WithRequired(e => e.EnumBbanFileStatus)
                .HasForeignKey(e => e.BbanFileStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumIbanStatus>()
                .Property(e => e.IBanStatus)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumIbanStatus>()
                .HasMany(e => e.IBanHistory)
                .WithRequired(e => e.EnumIBanStatus)
                .HasForeignKey(e => e.IBanStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Iban>()
                .Property(e => e.CountryCode)
                .IsUnicode(false);

            modelBuilder.Entity<p_Iban>()
                .Property(e => e.BankCode)
                .IsUnicode(false);

            modelBuilder.Entity<p_Iban>()
                .Property(e => e.Bban)
                .IsUnicode(false);

            modelBuilder.Entity<p_Iban>()
                .HasMany(e => e.IBanHistory)
                .WithRequired(e => e.IBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_IbanHistory>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<p_IbanHistory>()
                .Property(e => e.Context)
                .IsUnicode(false);

            modelBuilder.Entity<p_IbanHistory>()
                .Property(e => e.ChangedBy)
                .IsUnicode(false);
        }
    }
}
