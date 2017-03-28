using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Common
{
    /// <summary>
    /// The CustomXmlReader class, 
    //// The RSS of Eu sanction list they create wrong format for pubDate.
    /// RSS formatter have the rule for pubDate must be follow this format "ddd MMM dd HH:mm:ss Z yyyy".
    /// In Eu they create pubDate in this format "yyyy-mm-dd"
    /// </summary>
    public class CustomXmlReader : XmlTextReader
    {
        #region [Fields]

        /// <summary>
        /// The flag to check status is reading.
        /// </summary>
        private bool _readingTag = false;

        /// <summary>
        /// The current tag name that reading.
        /// </summary>
        private string _readingTagName;

        /// <summary>
        /// The custom datetime fomat fields.
        /// </summary>
        private CustomDateFormat _customFormat;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomXmlReader"/> class.
        /// </summary>
        /// <param name="inputUri">The input URI.</param>
        /// <param name="customFormat">The settings.</param>
        public CustomXmlReader(string inputUri, CustomDateFormat customFormat)
            : base(inputUri)
        {
            _customFormat = customFormat;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomXmlReader"/> class.
        /// </summary>
        /// <param name="srream">The s.</param>
        public CustomXmlReader(Stream srream) : base(srream) { }

        /// <summary>
        /// Checks that the current node is an element and advances the reader to the next node.
        /// </summary>
        public override void ReadStartElement()
        {
            if (String.IsNullOrEmpty(base.NamespaceURI) && IsCustomTag(base.LocalName))
            {
                _readingTag = true;
                _readingTagName = base.LocalName;
            }
            base.ReadStartElement();
        }

        /// <summary>
        /// Determines whether [is custom tag] [the specified tag name].
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns><c>true</c> if [is custom tag] [the specified tag name]; otherwise, <c>false</c>.</returns>
        private bool IsCustomTag(string tagName)
        {
            if (_customFormat == null)
            {
                return false;
            }
            // Check tagName is exist in _customSetting or not.
            if (String.IsNullOrEmpty(_customFormat.GetCustomFormat(tagName)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks that the current content node is an end tag and advances the reader to the next node.
        /// </summary>
        public override void ReadEndElement()
        {
            if (_readingTag)
            {
                _readingTag = false;
            }
            base.ReadEndElement();
        }

        /// <summary>
        /// Reads the contents of an element or a text node as a string. for custom 
        /// </summary>
        /// <returns>The contents of the element or text node. This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.Note: The text node can be either an element or an attribute text node.</returns>
        public override string ReadString()
        {
            if (_readingTag)
            {
                string dateString = base.ReadString();
                DateTime dt;
                if (!DateTime.TryParse(dateString, out dt))
                {
                    dt = DateTime.ParseExact(dateString, _customFormat.GetCustomFormat(_readingTagName), CultureInfo.InvariantCulture);

                }
                // return date time in string format is "ddd MMM dd HH:mm:ss Z yyyy".
                //The "R" or "r" format is defined by the DateTimeFormatInfo.RFC1123Pattern property
                //The custom format string is "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'".
                return dt.ToUniversalTime().ToString("R", CultureInfo.InvariantCulture);
            }
            else
            {
                return base.ReadString();
            }
        }
    }
}
