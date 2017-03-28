using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.iDealClients.Directory;
using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using BND.Services.Payments.iDeal.iDealClients.Status;
using BND.Services.Payments.iDeal.iDealClients.Transaction;
using System.IO;
using System.Xml.Linq;

namespace BND.Services.Payments.iDeal.iDealClients.Models
{
    /// <summary>
    /// Class iDealHttpResponseHandler for convert the response to response base.
    /// </summary>
    public class iDealHttpResponseHandler : IiDealHttpResponseHandler
    {
        #region [Method]
        /// <summary>
        /// Handles the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="signatureProvider">The signature provider.</param>
        /// <returns>iDealResponseBase.</returns>
        /// <exception cref="iDealException"></exception>
        /// <exception cref="System.IO.InvalidDataException">Unknown response</exception>
        /// <exception cref="InvalidDataException">Unknown response</exception>
        public iDealResponseBase HandleResponse(string response, ISignatureProvider signatureProvider)
        {
            var xDocument = XElement.Parse(response);
            switch (xDocument.Name.LocalName)
            {
                case "DirectoryRes":
                    return new DirectoryResponse(response);
                case "AcquirerTrxRes":
                    return new TransactionResponse(response);
                case "AcquirerStatusRes":
                    return new StatusResponse(response);
                case "ErrorRes":
                case "AcquirerErrorRes":
                    throw new iDealException(xDocument);
                default:
                    throw new InvalidDataException("Unknown response");
            }
        }
        #endregion
    }
}
