using BND.Services.Payments.iDeal.iDealClients.Base;
using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace BND.Services.Payments.iDeal.iDealClients.Models
{
    /// <summary>
    /// Class iDealHttpRequest that provides the method for send request and return response base.
    /// </summary>
    public class iDealHttpRequest : IiDealHttpRequest
    {
        #region [Method]
        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="idealRequest">The ideal request.</param>
        /// <param name="signatureProvider">The signature provider.</param>
        /// <param name="url">The URL.</param>
        /// <param name="iDealHttpResponseHandler">The i deal HTTP response handler.</param>
        /// <returns>iDealResponseBase.</returns>
        public iDealResponseBase SendRequest(iDealRequestBase idealRequest, ISignatureProvider signatureProvider, string url, 
                                             IiDealHttpResponseHandler iDealHttpResponseHandler)
        {
            // Create request
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = "text/xml;charset=UTF-8";
            request.Method = "POST";

            // Set content
            XmlDocument requestXml = idealRequest.ToXml(signatureProvider);
            requestXml = signatureProvider.SignXmlFile(requestXml);
            var postBytes = Encoding.UTF8.GetBytes(requestXml.OuterXml);

            // Send
            var requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // Return result
            var response = (HttpWebResponse)request.GetResponse();
            string responseRead = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return iDealHttpResponseHandler.HandleResponse(responseRead, signatureProvider);
        }
        #endregion
    }
}
