using System;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

using BND.Services.Infrastructure.Common.Extensions;
using BND.Services.Infrastructure.Common.Json;
using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.WebAPI.Extensions;
using BND.Services.Matrix.Entities.Interfaces;

using Newtonsoft.Json;

namespace BND.Services.Matrix.Web.Binders
{
    /// <summary>
    /// Our base filter model binding class
    /// </summary>
    public abstract class BaseModelBinder
    {
        /// <summary>
        /// Gets the name of the current type
        /// </summary>
        protected string TypeName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModelBinder"/> class.
        /// </summary>
        protected BaseModelBinder()
        {
            TypeName = GetType().FullName;
        }

        /// <summary>
        /// Deserializes the querystring input into a filters model if applicable or returns
        /// <see langword="null"/>, by using the specified  action context and binding context
        /// </summary>
        /// <typeparam name="TQueryStringModel"> An <see cref="IQueryStringModel"/> model </typeparam>
        /// <param name="actionContext"> The <see cref="HttpActionContext"/>. </param>
        /// <param name="bindingContext"> The <see cref="ModelBindingContext"/> </param>
        /// <returns>
        /// The <typeparamref name="TQueryStringModel"/> model
        /// </returns>
        protected TQueryStringModel GetFiltersModel<TQueryStringModel>(HttpActionContext actionContext, ModelBindingContext bindingContext)
            where TQueryStringModel : class, IQueryStringModel
        {
            ThrowIfContextsAreNull<TQueryStringModel>(actionContext, bindingContext);

            try
            {
                // No model type specified
                if (string.IsNullOrWhiteSpace(actionContext.Request.RequestUri.Query))
                {
                    return null;
                }

                // Invalid model type specified
                if (!bindingContext.IsValidModelOfType(typeof(TQueryStringModel)))
                {
                    return null;
                }

                var queryStringDictionary = actionContext.Request.GetQueryStringDictionary();

                if (queryStringDictionary.ContainsKey(Constants.QuerystringFieldsParameter))
                {
                    queryStringDictionary.Remove(Constants.QuerystringFieldsParameter);
                }

                return JsonConvert.DeserializeObject<TQueryStringModel>(queryStringDictionary.FromDictionaryToJson(), new BaseJsonSerializerSettings());
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(
                    "BaseModelBinderGetFiltersModelErrorKey",
                    new ModelBindingException(
                        new Error()
                        {
                            Title = string.Format(Resources.Web.Common.ModelBindingTitleErrorMessage, typeof(ModelBindingException).FullName, typeof(TQueryStringModel).FullName),
                            Source = string.Format(Resources.Common.ErrorSourceInfo, MethodBase.GetCurrentMethod().Name, TypeName),
                            StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                            StatusCodeDescription = HttpStatusCode.BadRequest.GetDescription(),
                            Message = Resources.Web.Common.ModelValidationFilterAttributeErrorMessage,
                            Code = (int)EnumErrorCodes.ModelBindingLayerError
                        },
                        ex,
                        typeof(EnumErrorCodes)));

                return null;
            }
        }

        /// <summary>
        /// The get value provider result.
        /// </summary>
        /// <param name="actionContext"> The action context. </param>
        /// <param name="bindingContext"> The binding context. </param>
        /// <typeparam name="TQueryStringModel"> The type of querystring models </typeparam>
        /// <returns>
        /// The <see cref="ValueProviderResult"/>.
        /// </returns>
        protected ValueProviderResult GetValueProviderResult<TQueryStringModel>(HttpActionContext actionContext, ModelBindingContext bindingContext)
            where TQueryStringModel : class, IQueryStringModel
        {
            ThrowIfContextsAreNull<TQueryStringModel>(actionContext, bindingContext);

            try
            {
                return !bindingContext.IsValidModelOfType(typeof(TQueryStringModel)) ? null : bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(
                    "BaseModelBinderGetValueProviderResultErrorKey",
                    new ModelBindingException(
                        new Error
                        {
                            Title = string.Format(Resources.Web.Common.ModelBindingTitleErrorMessage, typeof(ModelBindingException).FullName, typeof(TQueryStringModel).FullName),
                            Source = string.Format(Resources.Common.ErrorSourceInfo, MethodBase.GetCurrentMethod().Name, TypeName),
                            StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                            StatusCodeDescription = HttpStatusCode.BadRequest.GetDescription(),
                            Message = Resources.Web.Common.ModelValidationFilterAttributeErrorMessage,
                            Code = (int)EnumErrorCodes.ModelBindingLayerError,

                        },
                        ex,
                        typeof(EnumErrorCodes)));

                return null;
            }
        }

        /// <summary>
        /// The throw if contexts are null.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <param name="bindingContext">
        /// The binding context.
        /// </param>
        /// <typeparam name="TQueryStringModel">
        /// </typeparam>
        /// <exception cref="ModelBindingException">
        /// </exception>
        protected void ThrowIfContextsAreNull<TQueryStringModel>(HttpActionContext actionContext, ModelBindingContext bindingContext)
            where TQueryStringModel : class, IQueryStringModel
        {
            if (actionContext == null)
            {
                throw new ModelBindingException(
                    new Error()
                    {
                        Title = string.Format(Resources.Web.Common.ModelBindingTitleErrorMessage, typeof(ModelBindingException).FullName, typeof(TQueryStringModel).FullName),
                        Source = string.Format(Resources.Common.ErrorSourceInfo, MethodBase.GetCurrentMethod().Name, TypeName),
                        StatusCode = ((int)HttpStatusCode.InternalServerError).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.InternalServerError.GetDescription(),
                        Message = Resources.Web.Common.ActionContextIsNullErrorMessage,
                        Code = (int)EnumErrorCodes.ModelBindingLayerError
                    },
                    typeof(EnumErrorCodes));
            }

            if (bindingContext == null)
            {
                throw new ModelBindingException(
                    new Error()
                    {
                        Title = string.Format(Resources.Web.Common.ModelBindingTitleErrorMessage, typeof(ModelBindingException).FullName, typeof(TQueryStringModel).FullName),
                        Source = string.Format(Resources.Common.ErrorSourceInfo, MethodBase.GetCurrentMethod().Name, TypeName),
                        StatusCode = ((int)HttpStatusCode.InternalServerError).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.InternalServerError.GetDescription(),
                        Message = Resources.Web.Common.BindingContextIsNullErrorMessage,
                        Code = (int)EnumErrorCodes.ModelBindingLayerError
                    },
                    typeof(EnumErrorCodes));
            }
        }

        /// <summary>
        /// The add model error.
        /// </summary>
        /// <param name="bindingContext">
        /// The binding context.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <typeparam name="TQueryStringModel">
        /// </typeparam>
        protected void AddModelError<TQueryStringModel>(ModelBindingContext bindingContext, string key, string message, Exception innerException = null)
            where TQueryStringModel : class, IQueryStringModel
        {
            bindingContext.ModelState.AddModelError(
                key,
                new ModelBindingException(
                    new Error()
                    {
                        Title = string.Format(Resources.Web.Common.ModelBindingTitleErrorMessage, typeof(ModelBindingException).FullName, typeof(TQueryStringModel).FullName),
                        Source = string.Format(Resources.Common.ErrorSourceInfo, MethodBase.GetCurrentMethod().Name, TypeName),
                        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = HttpStatusCode.BadRequest.GetDescription(),
                        Message = message,
                        Code = (int)EnumErrorCodes.ModelBindingLayerError
                    },
                    innerException,
                    typeof(EnumErrorCodes)));
        }
    }
}