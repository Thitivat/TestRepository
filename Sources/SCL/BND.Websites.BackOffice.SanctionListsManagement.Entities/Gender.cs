namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents genders in the system. See <a href="https://en.wikipedia.org/wiki/ISO/IEC_5218">IEC_5218</a>
    /// </summary>
    public class Gender
    {
        /// <summary>
        /// Gender id.
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// Gender name.
        /// </summary>
        public string Name { get; set; }
    }
}
