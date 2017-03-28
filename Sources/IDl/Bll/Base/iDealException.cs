using System;
using System.Xml.Linq;

namespace BND.Services.Payments.iDeal.iDealClients.Base
{
    /// <summary>
    /// Class iDealException is a error response class.
    /// </summary>
    public class iDealException : SystemException
    {
        #region [Properties]
        /// <summary>
        /// Gets the create date time stamp.
        /// </summary>
        public DateTime CreateDateTimeStamp { get; private set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Gets the error detail.
        /// </summary>
        public string ErrorDetail { get; private set; }

        /// <summary>
        /// Gets or sets the consumer message.
        /// </summary>
        public string ConsumerMessage { get; set; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get { return string.Format("Code: {0}, Message: {1}, Detail: {2}", ErrorCode, ErrorMessage, ErrorDetail); }
        }
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="iDealException" /> class.
        /// </summary>
        /// <param name="xDocument">The x document.</param>
        public iDealException(XElement xDocument)
        {
            XNamespace xmlNamespace = Properties.Resources.XML_NAMESPACE;

            CreateDateTimeStamp = DateTime.Parse(xDocument.Element(xmlNamespace + "createDateTimestamp").Value);

            ErrorCode = xDocument.Element(xmlNamespace + "Error").Element(xmlNamespace + "errorCode").Value;
            ErrorMessage = xDocument.Element(xmlNamespace + "Error").Element(xmlNamespace + "errorMessage").Value;
            ErrorDetail = xDocument.Element(xmlNamespace + "Error").Element(xmlNamespace + "errorDetail").Value;
            ConsumerMessage = xDocument.Element(xmlNamespace + "Error").Element(xmlNamespace + "consumerMessage").Value;
        }
        #endregion
    }
}
