namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents NameAlias.
    /// </summary>
    public class NameAlias
    {
        /// <summary>
        /// NameAlias Id.
        /// </summary>
        public int NameAliasId { get; set; }

        /// <summary>
        /// NameAlias identifier from source (while importing from external source). 
        /// If it is not provided then it is set to be same as NameAlias Id.
        /// </summary>
        public int? OriginalNameAliasId { get; set; }

        /// <summary>
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// </summary>
        public string WholeName { get; set; }

        /// <summary>
        /// Name prefix. 
        /// </summary>
        public string PrefixName { get; set; }

        /// <summary>
        /// Title. For example Dr, Mr, Mrs, His/Her Most Excellent Majesty
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Describes quality of NameAlias.
        /// </summary>
        public short? Quality { get; set; }

        /// <summary>
        /// Function of subject.
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// <see cref="Entities.Gender"/> of the person. 
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// <see cref="Entities.Language"/>.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// <see cref="Entities.Regulation"/> that includes nameAlias.
        /// </summary>
        public Regulation Regulation { get; set; }

        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// <see cref="Entity"/>
        /// </summary>
        public int EntityId { get; set; }
    }
}
