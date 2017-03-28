using System.IO;
using System.Xml;

namespace BND.Services.IbanStore.ManagementPortal.Helper
{
    /// <summary>
    /// Class Extension.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Convert xml document to string.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <returns>System.String.</returns>
        public static string AsString(this XmlDocument xmlDoc)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter tx = new XmlTextWriter(sw))
                {
                    xmlDoc.WriteTo(tx);
                    string strXmlText = sw.ToString();
                    return strXmlText;
                }
            }
        }
    }
}