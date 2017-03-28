using System.Data.Common;
using System.Data.Entity;
using BND.Websites.BackOffice.SanctionListsManagement.Models;

namespace BND.Websites.BackOffice.SanctionListsManagement.Domain
{
    /// <summary>
    /// Class SanctionListsDbContext is an entity framework database context that inherited from
    /// <see cref="System.Data.Entity.DbContext"/> class.
    /// </summary>
    /// <remarks>
    /// This class is internal class you cannot use outside.
    /// </remarks>
    internal class SanctionListsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SanctionListsDbContext"/> class.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        public SanctionListsDbContext(DbConnection connection)
            : base(connection, true)
        { }

        /// <summary>
        /// Gets or sets the addresses representing all data in sl.Addresses table.
        /// </summary>
        /// <value>The addresses.</value>
        public virtual DbSet<p_Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the banks representing all data in sl.Banks table.
        /// </summary>
        /// <value>The banks.</value>
        public virtual DbSet<p_Bank> Banks { get; set; }

        /// <summary>
        /// Gets or sets the births representing all data in sl.Births table.
        /// </summary>
        /// <value>The births.</value>
        public virtual DbSet<p_Birth> Births { get; set; }

        /// <summary>
        /// Gets or sets the citizenships representing all data in sl.Citizenships table.
        /// </summary>
        /// <value>The citizenships.</value>
        public virtual DbSet<p_Citizenship> Citizenships { get; set; }

        /// <summary>
        /// Gets or sets the contact info representing all data in sl.ContactInfo table.
        /// </summary>
        /// <value>The contact infoes.</value>
        public virtual DbSet<p_ContactInfo> ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the entities representing all data in sl.Entities table.
        /// </summary>
        /// <value>The entities.</value>
        public virtual DbSet<p_Entity> Entities { get; set; }

        /// <summary>
        /// Gets or sets the enum action types representing all data in sl.EnumActionTypes table.
        /// </summary>
        /// <value>The enum action types.</value>
        public virtual DbSet<p_EnumActionType> EnumActionTypes { get; set; }

        /// <summary>
        /// Gets or sets the enum contact information types representing all data in sl.EnumContactInfoTypes table.
        /// </summary>
        /// <value>The enum contact information types.</value>
        public virtual DbSet<p_EnumContactInfoType> EnumContactInfoTypes { get; set; }

        /// <summary>
        /// Gets or sets the enum countries representing all data in sl.EnumCountries table.
        /// </summary>
        /// <value>The enum countries.</value>
        public virtual DbSet<p_EnumCountry> EnumCountries { get; set; }

        /// <summary>
        /// Gets or sets the enum genders representing all data in sl.EnumGenders table.
        /// </summary>
        /// <value>The enum genders.</value>
        public virtual DbSet<p_EnumGender> EnumGenders { get; set; }

        /// <summary>
        /// Gets or sets the enum identification types representing all data in sl.EnumIdentificationTypes table.
        /// </summary>
        /// <value>The enum identification types.</value>
        public virtual DbSet<p_EnumIdentificationType> EnumIdentificationTypes { get; set; }

        /// <summary>
        /// Gets or sets the enum languages representing all data in sl.EnumLanguages table.
        /// </summary>
        /// <value>The enum languages.</value>
        public virtual DbSet<p_EnumLanguage> EnumLanguages { get; set; }

        /// <summary>
        /// Gets or sets the enum list types representing all data in sl.EnumListTypes table.
        /// </summary>
        /// <value>The enum list types.</value>
        public virtual DbSet<p_EnumListType> EnumListTypes { get; set; }

        /// <summary>
        /// Gets or sets the enum statuses representing all data in sl.EnumStatuses table.
        /// </summary>
        /// <value>The enum statuses.</value>
        public virtual DbSet<p_EnumStatus> EnumStatuses { get; set; }

        /// <summary>
        /// Gets or sets the enum subject types representing all data in sl.EnumSubjectTypes table.
        /// </summary>
        /// <value>The enum subject types.</value>
        public virtual DbSet<p_EnumSubjectType> EnumSubjectTypes { get; set; }

        /// <summary>
        /// Gets or sets the identifications representing all data in sl.Identifications table.
        /// </summary>
        /// <value>The identifications.</value>
        public virtual DbSet<p_Identification> Identifications { get; set; }

        /// <summary>
        /// Gets or sets the list archives representing all data in sl.ListArchive table.
        /// </summary>
        /// <value>The list archives.</value>
        public virtual DbSet<p_ListArchive> ListArchive { get; set; }

        /// <summary>
        /// Gets or sets the logs representing all data in sl.Logs table.
        /// </summary>
        /// <value>The logs.</value>
        public virtual DbSet<p_Log> Logs { get; set; }

        /// <summary>
        /// Gets or sets the name aliases representing all data in sl.NameAliases table.
        /// </summary>
        /// <value>The name aliases.</value>
        public virtual DbSet<p_NameAlias> NameAliases { get; set; }

        /// <summary>
        /// Gets or sets the regulations representing all data in sl.Regulations table.
        /// </summary>
        /// <value>The regulations.</value>
        public virtual DbSet<p_Regulation> Regulations { get; set; }

        /// <summary>
        /// Gets or sets the remarks representing all data in sl.Remarks table.
        /// </summary>
        /// <value>The remarks.</value>
        public virtual DbSet<p_Remark> Remarks { get; set; }

        /// <summary>
        /// Gets or sets the settings representing all data in sl.Settings table.
        /// </summary>
        /// <value>The settings.</value>
        public virtual DbSet<p_Setting> Settings { get; set; }

        /// <summary>
        /// Gets or sets the updates representing all data in sl.Updates table.
        /// </summary>
        /// <value>The updates.</value>
        public virtual DbSet<p_Update> Updates { get; set; }

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
            modelBuilder.Entity<p_Address>()
                .Property(e => e.CountryIso3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_Bank>()
                .Property(e => e.CountryIso3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_Birth>()
                .Property(e => e.CountryIso3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_Citizenship>()
                .Property(e => e.CountryIso3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_Entity>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.Entity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Entity>()
                .HasMany(e => e.Banks)
                .WithRequired(e => e.Entity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Entity>()
                .HasMany(e => e.Births)
                .WithRequired(e => e.Entity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Entity>()
                .HasMany(e => e.Citizenships)
                .WithRequired(e => e.Entity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Entity>()
                .HasMany(e => e.ContactInfo)
                .WithRequired(e => e.Entity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Entity>()
                .HasMany(e => e.Identifications)
                .WithRequired(e => e.Entity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Entity>()
                .HasMany(e => e.NameAliases)
                .WithRequired(e => e.Entity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumActionType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumActionType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumActionType>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.EnumActionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumContactInfoType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumContactInfoType>()
                .HasMany(e => e.ContactInfo)
                .WithRequired(e => e.EnumContactInfoType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumCountry>()
                .Property(e => e.Iso3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumCountry>()
                .Property(e => e.Iso2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumCountry>()
                .HasMany(e => e.Addresses)
                .WithOptional(e => e.EnumCountry)
                .HasForeignKey(e => e.CountryIso3);

            modelBuilder.Entity<p_EnumCountry>()
                .HasMany(e => e.Banks)
                .WithOptional(e => e.EnumCountry)
                .HasForeignKey(e => e.CountryIso3);

            modelBuilder.Entity<p_EnumCountry>()
                .HasMany(e => e.Births)
                .WithOptional(e => e.EnumCountry)
                .HasForeignKey(e => e.CountryIso3);

            modelBuilder.Entity<p_EnumCountry>()
                .HasMany(e => e.Citizenships)
                .WithRequired(e => e.EnumCountry)
                .HasForeignKey(e => e.CountryIso3)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumCountry>()
                .HasMany(e => e.Identifications)
                .WithOptional(e => e.EnumCountry)
                .HasForeignKey(e => e.IssueCountryIso3);

            modelBuilder.Entity<p_EnumGender>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumGender>()
                .HasMany(e => e.NameAliases)
                .WithRequired(e => e.EnumGender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumIdentificationType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumIdentificationType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumIdentificationType>()
                .HasMany(e => e.Identifications)
                .WithRequired(e => e.EnumIdentificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumLanguage>()
                .Property(e => e.Iso3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumLanguage>()
                .Property(e => e.Iso2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumLanguage>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumLanguage>()
                .HasMany(e => e.NameAliases)
                .WithOptional(e => e.EnumLanguage)
                .HasForeignKey(e => e.LanguageIso3);

            modelBuilder.Entity<p_EnumListType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumListType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumListType>()
                .HasMany(e => e.Entities)
                .WithRequired(e => e.EnumListType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumListType>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.EnumListType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumListType>()
                .HasMany(e => e.Regulations)
                .WithRequired(e => e.EnumListType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumListType>()
                .HasMany(e => e.Updates)
                .WithRequired(e => e.EnumListType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumListType>()
                .HasMany(e => e.Settings)
                .WithRequired(e => e.EnumListType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumStatus>()
                .HasMany(e => e.Entities)
                .WithRequired(e => e.EnumStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_EnumSubjectType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<p_EnumSubjectType>()
                .HasMany(e => e.Entities)
                .WithRequired(e => e.EnumSubjectType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Identification>()
                .Property(e => e.IssueCountryIso3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_Log>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<p_Log>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<p_NameAlias>()
                .Property(e => e.LanguageIso3)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<p_Regulation>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.Regulation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Regulation>()
                .HasMany(e => e.Births)
                .WithRequired(e => e.Regulation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Regulation>()
                .HasMany(e => e.Citizenships)
                .WithRequired(e => e.Regulation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Regulation>()
                .HasMany(e => e.ContactInfo)
                .WithRequired(e => e.Regulation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Regulation>()
                .HasMany(e => e.Entities)
                .WithRequired(e => e.Regulation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Regulation>()
                .HasMany(e => e.Identifications)
                .WithRequired(e => e.Regulation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Regulation>()
                .HasMany(e => e.NameAliases)
                .WithRequired(e => e.Regulation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<p_Remark>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<p_Setting>()
                .Property(e => e.Key)
                .IsUnicode(false);

            modelBuilder.Entity<p_Setting>()
                .Property(e => e.Value)
                .IsUnicode(false);
        }
    }
}
