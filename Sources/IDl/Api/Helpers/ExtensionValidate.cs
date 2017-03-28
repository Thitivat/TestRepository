using System;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace BND.Services.Payments.iDeal.Api.Helpers
{
    /// <summary>
    /// Class ExtensionValidate is a extension class for validate modelstate.
    /// </summary>
    public static class ExtensionValidate
    {
        /// <summary>
        /// Validate model extension.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="System.ArgumentException">invalid model</exception>
        public static void Validate(this ModelStateDictionary modelState, string paramName)
        {
            if (!modelState.IsValid)
            {
                var message = string.Join(Environment.NewLine, 
                                          modelState.Values.SelectMany(m => m.Errors.Select(e => e.ErrorMessage)));

                throw new ArgumentException(message, paramName);
            }
        }
    }
}