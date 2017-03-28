using BND.Services.Payments.iDeal.Dal.Pocos;
using System.Data.Entity;

namespace BND.Services.Payments.iDeal.Dal.Tests
{
    public partial class MockDbContext
    {
        public virtual DbSet<p_Directory> Directories { get; set; }

        public virtual DbSet<p_Issuer> Issuers { get; set; }

        public virtual DbSet<p_Log> Logs { get; set; }

        public virtual DbSet<p_Transaction> Transactions { get; set; }

        public virtual DbSet<p_Setting> Settings { get; set; }

        public virtual DbSet<p_TransactionStatusHistory> TransactionStatusHistories { get; set; }

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
                .HasKey(x => new { x.AcquirerID, x.CountryNames, x.IssuerID });
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
    }
}
