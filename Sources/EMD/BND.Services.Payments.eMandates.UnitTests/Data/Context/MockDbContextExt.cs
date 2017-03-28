using BND.Services.Payments.eMandates.Models;
using System.Data.Entity;

namespace BND.Services.Payments.eMandates.UnitTests.Data.Context
{
    public partial class MockDbContext
    {
        public virtual DbSet<DebtorBank> DebtorBanks { get; set; }
        public virtual DbSet<Directory> Directories { get; set; }
        public virtual DbSet<EMandate> EmanDates { get; set; }
        public virtual DbSet<EnumSequenceType> EnumSequenceTypes { get; set; }
        public virtual DbSet<EnumTransactionType> EnumTransactionTypes { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<RawMessage> RawMessages { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionStatusHistory> TransactionStatusHistories { get; set; }

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
    }

}
