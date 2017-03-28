using System.Xml;

namespace BND.Services.Payments.iDeal.iDealClients.Interfaces
{
    /// <summary>
    /// Interface ISignatureProvider that provides the methods to set the Signature XML.
    /// </summary>
    public interface ISignatureProvider
    {
        /// <summary>
        /// Signs the XML file.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="privateKey">if set to <c>true</c> [private key].</param>
        /// <returns>XmlDocument.</returns>
        XmlDocument SignXmlFile(XmlDocument doc, bool privateKey = true);
    }
}
