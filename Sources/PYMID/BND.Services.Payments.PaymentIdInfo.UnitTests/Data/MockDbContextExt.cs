using BND.Services.Payments.PaymentIdInfo.Models;
using System.Data.Entity;

namespace BND.Services.Payments.PaymentIdInfo.Data.Test
{
    public partial class MockDbContext
    {
        public virtual DbSet<p_iDealTransaction> Transactions { get; set; }
        public virtual DbSet<p_iDealTransactionStatusHistory> TransactionStatusHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.TransactionID)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.AcquirerID)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.MerchantID)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.IssuerID)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.IssuerAuthenticationURL)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.MerchantReturnURL)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.EntranceCode)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.PurchaseID)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.Amount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.Currency)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.ConsumerIBAN)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.ConsumerBIC)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.PaymentType)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .Property(e => e.ExpectedCustomerIBAN)
                .IsUnicode(false);

            modelBuilder.Entity<p_iDealTransaction>()
                .HasMany(e => e.TransactionStatusHistories)
                .WithRequired(e => e.Transaction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_iDealTransactionStatusHistory>()
                .Property(e => e.TransactionID)
                .IsUnicode(false);
        }
    }
}
