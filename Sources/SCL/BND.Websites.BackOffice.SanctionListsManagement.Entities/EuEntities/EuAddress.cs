namespace BND.Websites.BackOffice.SanctionListsManagement.Entities.EuEntities
{
    /// <summary>
    /// Represents address information from xml.
    /// </summary>
    public class EuAddress
    {
        /// <summary>
        /// Address Identifier.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// The Entity Identifier of this address.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Number, part of the address information.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Street, part of the address information.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Zipcode, part of the address information.
        /// </summary>
        public string Zipcode { get; set; }

        /// <summary>
        /// City, part of the address information.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Country, part of the address information.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Other information associated with address.
        /// </summary>
        public string Other { get; set; }

        /// <summary>
        /// Regulation which includes this address.
        /// </summary>
        public EuRegulation Regulation { get; set; }

        /// <summary>
        /// Remark of the address.
        /// </summary>
        public string Remark { get; set; }
    }
}
