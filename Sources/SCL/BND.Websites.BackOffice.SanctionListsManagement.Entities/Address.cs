namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents address information.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Address Identifier.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Action identifier from source (while importing from external source). 
        /// If it is not provided then it is set to be same as Adress Id.
        /// </summary>
        public int? OriginalAddressId { get; set; }

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
        /// Other information associated with address.
        /// </summary>
        public string Other { get; set; }
        
        /// <summary>
        /// Country, part of the address information.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Regulation which includes this address.
        /// </summary>
        public Regulation Regulation { get; set; }

        /// <summary>
        /// Remark associated with this address.
        /// </summary>
        public Remark Remark { get; set; }

        /// <summary>
        /// <see cref="Entities.Remark"/>.
        /// </summary>
        public int EntityId { get; set; }
    }
}
