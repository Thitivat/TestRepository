
namespace BND.Services.IbanStore.Models
{
    /// <summary>
    /// Class IbanCountryRule.
    /// </summary>
    public class IbanCountryRule
    {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the length of the iban.
        /// </summary>
        /// <value>The length of the iban.</value>
        public int IbanLength { get; set; }

        /// <summary>
        /// Gets or sets the reg ex.
        /// </summary>
        /// <value>The reg ex.</value>
        public string RegEx { get; set; }

        /// <summary>
        /// Gets or sets the sample.
        /// </summary>
        /// <value>The sample.</value>
        public string Sample { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanCountryRule"/> class.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="ibanLength">Length of the iban.</param>
        /// <param name="regEx">The reg ex.</param>
        /// <param name="sample">The sample.</param>
        public IbanCountryRule(string countryCode, int ibanLength, string regEx, string sample)
        {
            CountryCode = countryCode;
            IbanLength = ibanLength;
            RegEx = regEx;
            Sample = sample;
        }
    }
}
