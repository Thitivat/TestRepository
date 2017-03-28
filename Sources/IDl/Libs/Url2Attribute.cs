using System;
using System.ComponentModel.DataAnnotations;

namespace BND.Services.Payments.iDeal
{
    /// <summary>
    /// Class Url2Attribute is a custom validation attribute for validating url.
    /// <remarks>We cannot use official Url attribute because it is not allow 'localhost'.</remarks>
    /// </summary>
    public class Url2Attribute : DataTypeAttribute
    {
        #region [Fields]

        /// <summary>
        /// The data type name.
        /// </summary>
        private const string DATA_TYPE_NAME = "Url2";

        #endregion


        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="Url2Attribute"/> class.
        /// </summary>
        public Url2Attribute()
            : base(DATA_TYPE_NAME)
        { }

        #endregion


        #region [Methods]

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.FormatException">It has to be string.</exception>
        public override bool IsValid(object value)
        {
            if (value == null || (value is string && String.IsNullOrEmpty((string)value)))
            {
                return true;
            }

            if (!(value is string))
            {
                throw new FormatException("It has to be string.");
            }

            Uri result;
            return Uri.TryCreate((string)value, UriKind.Absolute, out result);
        }

        /// <summary>
        /// Gets the name of the data type.
        /// </summary>
        /// <returns>The data type name.</returns>
        public override string GetDataTypeName()
        {
            return DATA_TYPE_NAME;
        }

        #endregion
    }
}
