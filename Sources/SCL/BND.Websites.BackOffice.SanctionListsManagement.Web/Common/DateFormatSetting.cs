using System;
using System.Collections.Generic;
using System.Globalization;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Common
{
    /// <summary>
    /// Class CustomDateFormat to read specific date time string and parsing that datetime.
    /// </summary>
    public class CustomDateFormat
    {
        /// <summary>
        /// The dictionary of datetime format, key is the name and value is string format for that name.
        /// </summary>
        private Dictionary<string, string> _dateFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDateFormat"/> class.
        /// </summary>
        public CustomDateFormat()
        {
            _dateFormat = new Dictionary<string, string>();
        }

        /// <summary>
        /// Adds the custom datetime format.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="format">The format.</param>
        public void AddCustomFormat(string name, string format)
        {
            _dateFormat.Add(name, format);
        }

        /// <summary>
        /// Gets the custom format by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        public string GetCustomFormat(string name)
        {
            string value = "";
            _dateFormat.TryGetValue(name, out value);
            return value;
        }

        /// <summary>
        /// Parses the datetime string by sepecific format to DateTime object.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dateString">The date string.</param>
        /// <returns>DateTime.</returns>
        public DateTime ParseDate(string name, string dateString)
        {
            return DateTime.ParseExact(dateString, GetCustomFormat(name), CultureInfo.InstalledUICulture);
        }
    }
}
