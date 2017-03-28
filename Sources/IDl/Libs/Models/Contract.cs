namespace BND.Services.Payments.iDeal.Models
{
    public class Contract
    {
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>The name of the customer.</value>
        public string CustomerName {get;set;}
        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        /// <value>The postcode.</value>
        public string Postcode {get;set;}
        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        public string Street {get;set;}
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City {get;set;}
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; set; }
    }
}