namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents information about the country. See <a href="https://en.wikipedia.org/wiki/ISO_3166-1">ISO_3166</a>.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// A three-letter country code.
        /// </summary>
        public string Iso3 { get; set; }

        /// <summary>
        /// A two-letter country code.
        /// </summary>
        public string Iso2 { get; set; }

        /// <summary>
        /// Country name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Country name formatted.
        /// </summary>
        public string NiceName { get; set; }

        /// <summary>
        /// A numeric country code.
        /// </summary>
        public int? NumCode { get; set; }

        /// <summary>
        /// Country's phone code.
        /// </summary>
        public int? PhoneCode { get; set; }
    }
}
