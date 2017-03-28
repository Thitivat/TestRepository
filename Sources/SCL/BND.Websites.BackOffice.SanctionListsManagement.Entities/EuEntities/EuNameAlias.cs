namespace BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities
{
    /// <summary>
    /// Represents information about name alias from xml.
    /// </summary>
    public class EuNameAlias
    {
        /// <summary>
        /// NameAlias Identifier.
        /// </summary>
        public int NameAliasId { get; set; }

        /// <summary>
        /// The Entity Identifier of this NameAlias.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Last name of name alias.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// First name of name alias.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Middle name of name alias.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Whole name of name alias.
        /// </summary>
        public string WholeName { get; set; }

        /// <summary>
        /// prefix name of name alias.
        /// </summary>
        public string PrefixName { get; set; }

        /// <summary>
        /// title of name alias.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Quality of name alias.
        /// </summary>
        public int? Quality { get; set; }

        /// <summary>
        /// Function of name alias.
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// Gender of name alias.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Language of name alias.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Regulation which includes this name alias.
        /// </summary>
        public EuRegulation Regulation { get; set; }

        /// <summary>
        /// Remark of the name alias.
        /// </summary>
        public string Remark { get; set; }
    }
}
