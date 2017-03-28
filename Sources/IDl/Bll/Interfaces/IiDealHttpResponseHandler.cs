using BND.Services.Payments.iDeal.iDealClients.Base;

namespace BND.Services.Payments.iDeal.iDealClients.Interfaces
{
    /// <summary>
    /// Interface IiDealHttpResponseHandler that provides method to handle the response.
    /// </summary>
    public interface IiDealHttpResponseHandler
    {
        /// <summary>
        /// Handles the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="signatureProvider">The signature provider.</param>
        /// <returns>iDealResponseBase.</returns>
        iDealResponseBase HandleResponse(string response, ISignatureProvider signatureProvider);
    }
}
