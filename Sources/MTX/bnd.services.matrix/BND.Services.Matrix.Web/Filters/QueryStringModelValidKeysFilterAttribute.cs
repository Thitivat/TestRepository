using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.WebAPI.Extensions;
using BND.Services.Matrix.Entities.Interfaces;

namespace BND.Services.Matrix.Web.Filters
{
    /// <summary>
    /// Checks for empty querystring values
    /// </summary>
    public class QueryStringModelValidKeysFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var actionParamBindings = actionContext.ActionDescriptor.ActionBinding.ParameterBindings;
            var actionParamNames = actionParamBindings.Select(prop => prop.Descriptor.ParameterName).ToList();
            var validNames = new List<string>();

            foreach (var actionParamName in actionParamNames)
            {
                var paramType = actionParamBindings.First(x => x.Descriptor.ParameterName.Equals(actionParamName)).Descriptor.ParameterType;

                // currently supported types in querystring other than implementations of IQuerystringModel
                // add/comment out as needed!
                var paramTypeSupported =
                    paramType == typeof(string) ||
                    paramType == typeof(decimal) ||
                    paramType == typeof(decimal?) ||
                    paramType == typeof(int) ||
                    paramType == typeof(int?) ||
                    paramType == typeof(bool) ||
                    paramType == typeof(bool?) ||
                    paramType == typeof(DateTime) ||
                    paramType == typeof(DateTime?) ||
                    paramType.IsEnum ||
                    paramType.IsNullableEnum();

                if (typeof(IQueryStringModel).IsAssignableFrom(paramType))
                {
                    validNames.AddRange(paramType.GetProperties().Select(x => x.Name));
                }
                else if (paramTypeSupported)
                {
                    validNames.Add(actionParamName);
                }
            }

            var invalidKeys =
                actionContext.Request
                    .GetQueryStringDictionary()
                    .Select(x => x.Key)
                    .Where(x => !validNames.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                    .ToList();

            if (invalidKeys.Count > 0)
            {
                throw new ServiceLayerException(
                          new Error()
                          {
                              Title = typeof(ServiceLayerException).FullName,
                              Source = GetType().FullName,
                              Message = string.Format(Resources.Web.Common.QueryStringModelValidKeysFilterAttributeErrorMessage, string.Join(", ", invalidKeys)),
                              StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                              StatusCodeDescription = HttpStatusCode.BadRequest.GetDescription(),
                              Code = (int)EnumErrorCodes.ModelBindingLayerError
                          },
                          typeof(EnumErrorCodes));
            }


        }
    }
}