using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Matrix.Business.CashAccount;

namespace BND.Services.Matrix.Business.Extensions
{
    /// <summary>
    /// The enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// The to entity.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="IdentificationStandards"/>.
        /// </returns>
        /// <exception cref="BusinessLayerException">
        /// </exception>
        public static Enums.IdentificationStandards ToEnum(this CashAccount.IdentificationStandards item)
        {
            switch (item)
            {
                case IdentificationStandards.IBAN:
                    return Enums.IdentificationStandards.IBAN;
                case IdentificationStandards.BBAN:
                    return Enums.IdentificationStandards.BBAN;
                case IdentificationStandards.Internal:
                    return Enums.IdentificationStandards.Internal;
                case IdentificationStandards.SWIFT:
                    return Enums.IdentificationStandards.SWIFT;
                case IdentificationStandards.TARGET:
                    return Enums.IdentificationStandards.TARGET;
                default: throw CreateBusinessLayerException(
                         HttpStatusCode.BadRequest,
                         "Invalid Enum",
                         EnumErrorCodes.BusinessLayerError);

            }
        }

        /// <summary>
        /// Creates a custom business layer exception
        /// </summary>
        /// <param name="httpStatusCode"> The <see cref="HttpStatusCode"/> of the error </param>
        /// <param name="message"> The error message </param>
        /// <param name="code"> The error code. </param>
        /// <returns> The <see cref="BusinessLayerException"/> </returns>
        private static BusinessLayerException CreateBusinessLayerException(HttpStatusCode httpStatusCode, string message, EnumErrorCodes code)
        {
            var stackTrace = new StackTrace();

            // Get calling method name
            var callingMethod = stackTrace.GetFrame(1).GetMethod().Name;

            return new BusinessLayerException(
                new Error()
                {
                    Code = (int)code,
                    Title = typeof(BusinessLayerException).FullName,
                    Source = string.Format(Resources.Common.ErrorSourceInfo, callingMethod, MethodBase.GetCurrentMethod().DeclaringType.Name),
                    StatusCode = ((int)httpStatusCode).ToString(CultureInfo.InvariantCulture),
                    StatusCodeDescription = httpStatusCode.GetDescription(),
                    Message = message
                },
                typeof(EnumErrorCodes));
        }
    }
}
