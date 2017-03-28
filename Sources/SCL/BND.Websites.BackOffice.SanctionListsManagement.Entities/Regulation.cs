using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents regulations.
    /// </summary>
    public class Regulation
    {
        /// <summary>
        /// Regulation Id.
        /// </summary>
        public int RegulationId { get; set; }

        /// <summary>
        /// Regulation title.
        /// </summary>
        public string RegulationTitle { get; set; }

        /// <summary>
        /// Regulation date.
        /// </summary>
        public DateTime? RegulationDate { get; set; }

        /// <summary>
        /// Publication date.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Publication title.
        /// </summary>
        public string PublicationTitle { get; set; }

        /// <summary>
        /// URL where the regulation is published.
        /// </summary>
        public string PublicationUrl { get; set; }

        /// <summary>
        /// Programme.
        /// </summary>
        public string Programme { get; set; }


        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// <see cref="Entities.ListType"/> that regulation belongs to.
        /// </summary>
        public ListType ListType { get; set; }
    }
}
