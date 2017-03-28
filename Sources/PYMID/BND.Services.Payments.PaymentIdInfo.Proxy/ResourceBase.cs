using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Payments.PaymentIdInfo.Entities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace BND.Services.Payments.PaymentIdInfo.Proxy
{
    /// <summary>
    /// Class ResourceBase is a abstract class for all proxy call that will get the resource from rest api
    /// this class provides the fields and methods that all proxy have to use to communicate with rest api.
    /// </summary>
    public abstract class ResourceBase : IDisposable
    {
        #region [Fields]
        /// <summary>
        /// The disposed flag.
        /// </summary>
        protected bool _disposed;

        /// <summary>
        /// The token string that implement the authentication key.
        /// </summary>
        protected string _token;

        /// <summary>
        /// The HTTP client to communicates with api.
        /// </summary>
        protected HttpClient _httpClient;
        #endregion

        #region [Constructor]

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="token">The token string that implement the authentication key .</param>
        public ResourceBase(string baseAddress, string token)
            : this(baseAddress, token, new HttpClient())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="token">The token string that implement the authentication key .</param>
        /// <param name="httpClient">The HTTP client.</param>
        public ResourceBase(string baseAddress, string token, HttpClient httpClient)
        {
            // Sets all fields.
            _token = token;

            // Prepares http client.
            AssemblyName assemblyName = this.GetType().Assembly.GetName();
            _httpClient = httpClient;
            // Sets base url
            _httpClient.BaseAddress = new Uri(new Uri(baseAddress.EndsWith("/") ? baseAddress
                                                                                : baseAddress + "/"),
                                              Properties.Resources.URL_RES_BASE);
            // Sets headers
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);

            // Sets user agent.
            _httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue(new ProductHeaderValue(assemblyName.Name, assemblyName.Version.ToString()))
            );
        }
        #endregion

        #region [Public Methods]


        /// <summary>
        /// Sets the result for returning back. This method will re-use in any public method.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="responseMessage">The response message.</param>
        /// <returns>``0.</returns>
        /// <exception cref="System.ArgumentException">is used when rest api return bad request</exception>
        /// <exception cref="System.Security.SecurityException">is used when rest api return forbidden</exception>
        /// <exception cref="System.UnauthorizedAccessException">is used when rest api return unauthorized</exception>
        /// <exception cref="System.Exception">is used when rest api return InternalServerError or except from 3 above</exception>
        protected TResult SetResult<TResult>(HttpResponseMessage responseMessage)
            where TResult : class
        {
            // Gets result from api.
            string resultContent = responseMessage.Content.ReadAsStringAsync().Result;

            // Checks result.
            if (responseMessage.IsSuccessStatusCode)
            {
                // Sets to success when http status code is 200.
                return JsonConvert.DeserializeObject<TResult>(resultContent);
            }
            else
            {
                string Message, ErrorCode = String.Empty;
                try
                {
                    ApiErrorModel apiModel = JsonConvert.DeserializeObject<ApiErrorModel>(resultContent);
                    Message = apiModel.Message;
                    ErrorCode = apiModel.ErrorCode;
                }
                catch
                {
                    // this case will occur when api doesn't return ApiErrorModel and make DeserializeObject error.
                    Message = resultContent;
                }
                // for all fail will throw exception separate via status code.
                throw new ProxyException(
                    new Error()
                    {
                        Title = typeof(ProxyException).FullName,
                        Source = string.Format("Method {0} at {1}!", MethodBase.GetCurrentMethod().Name, GetType().FullName),
                        StatusCode = ((int)responseMessage.StatusCode).ToString(CultureInfo.InvariantCulture),
                        StatusCodeDescription = (responseMessage.StatusCode).ToString(),
                        Message = Message,
                        Code = (int)EnumErrorCodes.DataLayerError
                    }, typeof(EnumErrorCodes)
                );
            }
        }

        /// <summary>
        /// Maps the content of the model to form-urlencoded content.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model which you want to map.</param>
        /// <param name="parentPropertyName">Name of the parent property.</param>
        /// <returns>A http content as FormUrlEncodedContent.</returns>
        protected FormUrlEncodedContent MapModelToFormUrlEncodedContent<TModel>(TModel model, string parentPropertyName = null)
            where TModel : class
        {
            return new FormUrlEncodedContent(MapModelToKeyValuePairs(model, parentPropertyName));
        }

        /// <summary>
        /// Maps the model to key value pairs for using with MapModelToFormUrlEncodedContent method.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model which you want to map.</param>
        /// <param name="parentPropertyName">Name of the parent property.</param>
        /// <returns>A collection of key value pairs.</returns>
        private IEnumerable<KeyValuePair<string, string>> MapModelToKeyValuePairs<TModel>(TModel model, string parentPropertyName = null)
            where TModel : class
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            // Extracts all properties of model to add to result variable.
            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                // For collection of objects.
                if (prop.PropertyType.IsArray && IsClassOrStruct(prop.PropertyType.GetElementType()) ||
                    IsGenericCollection(prop.PropertyType) && IsClassOrStruct(prop.PropertyType.GetGenericArguments().First()))
                {
                    // Sets value to list object.
                    IList values = prop.PropertyType.IsArray && IsClassOrStruct(prop.PropertyType.GetElementType())
                                   ? ((object[])prop.GetValue(model, null)).ToList()
                                   : (IList)prop.GetValue(model, null);
                    // Gets all items one by one to add to result variable.
                    foreach (object value in values)
                    {
                        // Recursive calls method to extracts all childs of all properties.
                        result.AddRange(
                            MapModelToKeyValuePairs(value, JoinStringOmitEmpty(Properties.Resources.SEPARATOR, parentPropertyName, prop.Name))
                        );
                    }
                }
                // For dictionary object.
                else if (prop.GetValue(model, null) is IDictionary)
                {
                    // Gets all items one by one to add to result variable.
                    foreach (dynamic kv in (IDictionary)prop.GetValue(model, null))
                    {
                        // Adds key as a keyvaluepair object. Is has to be string only.
                        result.Add(
                            new KeyValuePair<string, string>(JoinStringOmitEmpty(Properties.Resources.SEPARATOR, parentPropertyName, prop.Name,
                                                                                 Properties.Resources.KEY),
                                                             kv.Key.ToString())
                        );
                        // Adds value by recursive calling.
                        result.AddRange(
                            MapModelToKeyValuePairs(kv.Value, JoinStringOmitEmpty(Properties.Resources.SEPARATOR, parentPropertyName, prop.Name,
                                                                                  Properties.Resources.VALUE))
                        );
                    }
                }
                // For Uri object.
                else if (prop.PropertyType == typeof(Uri))
                {
                    // Adds data to result variable.
                    result.Add(
                        new KeyValuePair<string, string>(JoinStringOmitEmpty(Properties.Resources.SEPARATOR, parentPropertyName, prop.Name),
                                                         (prop.GetValue(model, null) == null) ? null : prop.GetValue(model, null).ToString())
                    );
                }
                // For class or struct object.
                else if (IsClassOrStruct(prop.PropertyType) &&
                         prop.PropertyType != typeof(object)) // Checks boxing value.
                {
                    // Recursive calls method to extracts all childs of all properties.
                    result.AddRange(
                        MapModelToKeyValuePairs(prop.GetValue(model, null), JoinStringOmitEmpty(Properties.Resources.SEPARATOR,
                                                                                          parentPropertyName, prop.Name))
                    );
                }
                // For value.
                else
                {
                    // For collection value.
                    if (prop.PropertyType.IsArray || IsGenericCollection(prop.PropertyType))
                    {
                        // Sets value to list object.
                        IList values = (prop.PropertyType.IsArray) ? ((object[])prop.GetValue(model, null)).ToList()
                                                                   : (IList)prop.GetValue(model, null);
                        // Gets all items one by one to add to result variable.
                        foreach (object value in values)
                        {
                            // Adds data to result variable.
                            result.Add(
                                new KeyValuePair<string, string>(JoinStringOmitEmpty(Properties.Resources.SEPARATOR, parentPropertyName,
                                                                                     prop.Name),
                                                                 value.ToString())
                            );
                        }
                    }
                    // For static value.
                    else
                    {
                        // Adds data to result variable.
                        result.Add(
                            new KeyValuePair<string, string>(JoinStringOmitEmpty(Properties.Resources.SEPARATOR, parentPropertyName, prop.Name),
                                                             (prop.GetValue(model, null) == null) ? null : prop.GetValue(model, null).ToString())
                        );
                    }
                }
            }

            // Returns result as IEnumerable.
            return result;
        }

        /// <summary>
        /// Determines whether [is generic collection] [the specified property type].
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns><c>true</c> if [is generic collection] [the specified property type]; otherwise, <c>false</c>.</returns>
        private bool IsGenericCollection(Type propertyType)
        {
            return propertyType.IsGenericType &&
                   propertyType.GetGenericArguments().Length == 1;
        }

        /// <summary>
        /// Determines whether [is class or structure] [the specified property type].
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns><c>true</c> if [is class or structure] [the specified property type]; otherwise, <c>false</c>.</returns>
        private bool IsClassOrStruct(Type propertyType)
        {
            return !propertyType.IsArray &&
                   !IsGenericCollection(propertyType) &&
                   ((propertyType.IsClass && propertyType != typeof(string)) || // Class.
                    (propertyType.IsValueType && !propertyType.IsPrimitive && // Struct.
                     propertyType != typeof(decimal) && propertyType != typeof(DateTime)));
        }

        /// <summary>
        /// Joins the all strings with specified separator but omit empty string.
        /// </summary>
        /// <param name="separator">The separator.</param>
        /// <param name="value">The all strings which you want to join.</param>
        /// <returns>A joined string.</returns>
        private string JoinStringOmitEmpty(string separator, params string[] value)
        {
            return String.Join(separator, value.Where(v => !String.IsNullOrEmpty(v)));
        }

        #endregion

        #region [Dispose]
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // Clears garbage collector.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Releases all resources.
                if (_httpClient != null)
                {
                    _httpClient.Dispose();
                    _httpClient = null;
                }

                // Sets dispose flag.
                _disposed = true;
            }
        }
        #endregion
    }
}
