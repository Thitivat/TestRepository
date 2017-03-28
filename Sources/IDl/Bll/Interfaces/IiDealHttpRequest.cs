using BND.Services.Payments.iDeal.iDealClients.Base;

namespace BND.Services.Payments.iDeal.iDealClients.Interfaces
{
    /// <summary>
    /// Interface IiDealHttpRequest that provides method to send the request.
    /// </summary>
    public interface IiDealHttpRequest
    {
        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="idealRequest">The ideal request.</param>
        /// <param name="signatureProvider">The signature provider.</param>
        /// <param name="url">The URL.</param>
        /// <param name="iDealHttpResponseHandler">The i deal HTTP response handler.</param>
        /// <returns>iDealResponseBase.</returns>
        iDealResponseBase SendRequest(iDealRequestBase idealRequest, ISignatureProvider signatureProvider, string url, 
                                      IiDealHttpResponseHandler iDealHttpResponseHandler);
    }
}
