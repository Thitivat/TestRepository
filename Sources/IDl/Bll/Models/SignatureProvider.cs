using BND.Services.Payments.iDeal.iDealClients.Interfaces;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace BND.Services.Payments.iDeal.iDealClients.Models
{
    /// <summary>
    /// Class SignatureProvider that provides method to set SignedXml.
    /// </summary>
    public class SignatureProvider : ISignatureProvider
    {
        #region [Field]
        /// <summary>
        /// The _private certificate
        /// </summary>
        private readonly X509Certificate2 _privateCertificate;
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureProvider"/> class.
        /// </summary>
        /// <param name="privateCertificate">The private certificate.</param>
        public SignatureProvider(X509Certificate2 privateCertificate)
        {
            _privateCertificate = privateCertificate;
        }
        #endregion


        #region [Method]
        /// <summary>
        /// Signs the XML file.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <param name="privateKey">if set to <c>true</c> [private key].</param>
        /// <returns>XmlDocument.</returns>
        /// <exception cref="System.ArgumentException">xmlDoc</exception>
        public XmlDocument SignXmlFile(XmlDocument xmlDoc, bool privateKey)
        {
            // Check arguments.
            if (xmlDoc == null)
            {
                throw new ArgumentException("xmlDoc");
            }

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = (RSA)_privateCertificate.PrivateKey;

            signedXml.KeyInfo = new KeyInfo();
            signedXml.KeyInfo.AddClause(new KeyInfoName(_privateCertificate.Thumbprint));

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            //reference.DigestMethod = digestMethod;            

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            return xmlDoc;
        }
        #endregion
    }
}
