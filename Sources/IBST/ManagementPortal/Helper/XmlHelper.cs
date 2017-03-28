using System;
using System.IO;
using System.Xml;

namespace BND.Services.IbanStore.ManagementPortal.Helper
{
    /// <summary>
    /// Class XmlHelper.
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// Reads the XML to string.
        /// </summary>
        /// <param name="path">The xml path.</param>
        /// <returns>System.String.</returns>
        public static string ReadXmlToString(string path)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            XmlDocument document = new XmlDocument();
            document.Load(filePath);
            return document.AsString();
        }
    }
}
