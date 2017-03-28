using System.Data.Entity;
using BND.Services.Security.OTP.Dal.Pocos;
using System.Data.Common;
using System.Data.SqlClient;
using BND.Services.Security.OTP.Dal.Attributes;

namespace BND.Services.Security.OTP.Dal
{
    /// <summary>
    /// Class OtpContext is an entity framework database context that inherited from
    /// <see cref="System.Data.Entity.DbContext"/> class.
    /// </summary>
    /// <remarks>
    /// This class is internal class you cannot use outside.
    /// </remarks>
    public class OtpContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OtpContext"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public OtpContext(DbConnection connection)
            : base(connection, true)
        {
        }

        /// <summary>
        /// Gets or sets the Accounts representing all data in otp.Account table.
        /// </summary>
        /// <value>The accounts.</value>
        public virtual DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the Attempts representing all data in otp.Attempt table.
        /// </summary>
        /// <value>The attempts.</value>
        public virtual DbSet<Attempt> Attempts { get; set; }

        /// <summary>
        /// Gets or sets the EnumChannelTypes representing all data in otp.EnumChannelType table.
        /// </summary>
        /// <value>The enum channel types.</value>
        public virtual DbSet<EnumChannelType> EnumChannelTypes { get; set; }

        /// <summary>
        /// Gets or sets the EnumStatus representing all data in otp.EnumStatus table.
        /// </summary>
        /// <value>The enum status.</value>
        public virtual DbSet<EnumStatus> EnumStatus { get; set; }

        /// <summary>
        /// Gets or sets the Logs representing all data in otp.Log table.
        /// </summary>
        /// <value>The logs.</value>
        public virtual DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Gets or sets the OneTimePasswords representing all data in otp.OneTimePassword table.
        /// </summary>
        /// <value>The one time passwords.</value>
        public virtual DbSet<OneTimePassword> OneTimePasswords { get; set; }

        /// <summary>
        /// Gets or sets the Settings representing all data in otp.Setting table.
        /// </summary>
        /// <value>The settings.</value>
        public virtual DbSet<Setting> Settings { get; set; }

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
            modelBuilder.Entity<Account>()
                .Property(e => e.ApiKey)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.IpAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Salt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.OneTimePassword)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attempt>()
                .Property(e => e.OtpId)
                .IsUnicode(false);

            modelBuilder.Entity<Attempt>()
                .Property(e => e.IpAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Attempt>()
                .Property(e => e.UserAgent)
                .IsUnicode(false);

            modelBuilder.Entity<EnumChannelType>()
                .Property(e => e.ChannelType)
                .IsUnicode(false);

            modelBuilder.Entity<EnumChannelType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EnumChannelType>()
                .HasMany(e => e.OneTimePassword)
                .WithRequired(e => e.EnumChannelType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnumStatus>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<EnumStatus>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EnumStatus>()
                .HasMany(e => e.OneTimePassword)
                .WithRequired(e => e.EnumStatus)
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

            modelBuilder.Entity<OneTimePassword>()
                .Property(e => e.OtpId)
                .IsUnicode(false);

            modelBuilder.Entity<OneTimePassword>()
                .Property(e => e.Suid)
                .IsUnicode(false);

            modelBuilder.Entity<OneTimePassword>()
                .Property(e => e.ChannelType)
                .IsUnicode(false);

            modelBuilder.Entity<OneTimePassword>()
                .Property(e => e.ChannelSender)
                .IsUnicode(false);

            modelBuilder.Entity<OneTimePassword>()
                .Property(e => e.ChannelAddress)
                .IsUnicode(false);

            modelBuilder.Entity<OneTimePassword>()
                .Property(e => e.ChannelMessage)
                .IsUnicode(false);

            modelBuilder.Entity<OneTimePassword>()
                .Property(e => e.RefCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OneTimePassword>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<OneTimePassword>()
                .HasMany(e => e.Attempt)
                .WithRequired(e => e.OneTimePassword)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Key)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Value)
                .IsUnicode(false);

            if (Database.Connection is SqlConnection)
            {
                modelBuilder.Conventions.Add<SqlColumnTypeAttributeConvention>();
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
