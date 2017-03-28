using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities
{
    /// <summary>
    /// Represents information about regulation from xml.
    /// </summary>
    public class EuRegulation
    {

        /// <summary>
        /// Title of regulation.
        /// </summary>
        public string RegulationTitle { get; set; }

        /// <summary>
        /// Date of regulation.
        /// </summary>
        public DateTime? RegulationDate { get; set; }

        /// <summary>
        /// Published date of regulation.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Published title of regulation.
        /// </summary>
        public string PublicationTitle { get; set; }

        /// <summary>
        /// Published url of regulation.
        /// </summary>
        public string PublicationUrl { get; set; }

        /// <summary>
        /// Programme of regulation.
        /// </summary>
        public string Programme { get; set; }

        /// <summary>
        /// Remark of the name regulation.
        /// </summary>
        public string Remark { get; set; }
    }
}
