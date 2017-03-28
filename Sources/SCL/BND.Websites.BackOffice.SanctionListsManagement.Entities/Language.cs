namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents languages. See <a href="https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes">List of ISO 639-1 codes</a>
    /// </summary>
    public class Language
    {
        /// <summary>
        /// A three-letter code of the language.
        /// </summary>
        public string Iso3 { get; set; }

        /// <summary>
        /// A two-letter code of the language.
        /// </summary>
        public string Iso2 { get; set; }

        /// <summary>
        /// Language name.
        /// </summary>
        public string Name { get; set; }
    }
}
