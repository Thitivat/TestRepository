using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace BND.Services.Security.OTP.ClientProxy
{
    /// <summary>
    /// Class ResourceBase.
    /// </summary>
    public abstract class ResourceBase
    {
        #region [Fields]

        /// <summary>
        /// The api key field to store api key which retrieves from constructor.
        /// </summary>
        protected string ApiKey;
        /// <summary>
        /// The account identifier of each user which retrieves from constructor.
        /// </summary>
        protected string AccountId;

        /// <summary>
        /// The base address for calls api.
        /// </summary>
        private readonly string _baseAddress;

        #endregion


        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBase"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address of api.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="accountId">The account identifier.</param>
        protected ResourceBase(string baseAddress, string apiKey, string accountId)
        {
            // Sets all fields.
            ApiKey = apiKey;
            AccountId = accountId;
            _baseAddress = baseAddress;
        }

        #endregion

        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <returns>HttpClient.</returns>
        protected virtual HttpClient GetHttpClient()
        {
            // Prepares http client.
            AssemblyName assemblyName = GetType().Assembly.GetName();

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(new Uri(_baseAddress.EndsWith("/")
                    ? _baseAddress
                    : _baseAddress + "/"),
                    Properties.Resources.URL_RES_BASE)
            };

            // Sets base url
            // Sets headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiKey);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(Properties.Resources.HEADER_ACC_ID, AccountId);
            // Sets user agent.
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue(new ProductHeaderValue(assemblyName.Name, assemblyName.Version.ToString()))
            );

            return httpClient;
        }

        #region [Methods]

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
        /// Maps the content of the model to raw data on body as json format.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model which you want to map.</param>
        /// <returns>A http content as StringContent.</returns>
        protected StringContent MapModelToRawContent<TModel>(TModel model)
        {
            return new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, Properties.Resources.CONTENT_TYPE_JSON);
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
                                   ? ((object[])prop.GetValue(model)).ToList()
                                   : (IList)prop.GetValue(model);
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
                else if (prop.GetValue(model) is IDictionary)
                {
                    // Gets all items one by one to add to result variable.
                    foreach (dynamic kv in (IDictionary)prop.GetValue(model))
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
                // For class or struct object.
                else if (IsClassOrStruct(prop.PropertyType) &&
                         prop.PropertyType != typeof(object)) // Checks boxing value.
                {
                    // Recursive calls method to extracts all childs of all properties.
                    result.AddRange(
                        MapModelToKeyValuePairs(prop.GetValue(model), JoinStringOmitEmpty(Properties.Resources.SEPARATOR,
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
                        IList values = (prop.PropertyType.IsArray) ? ((object[])prop.GetValue(model)).ToList()
                                                                   : (IList)prop.GetValue(model);
                        // Gets all items one by one to add to result variable.
                        result.AddRange(from object value in values select new KeyValuePair<string, string>(JoinStringOmitEmpty(Properties.Resources.SEPARATOR, parentPropertyName, prop.Name), value.ToString()));
                    }
                    // For static value.
                    else
                    {
                        // Adds data to result variable.
                        result.Add(
                            new KeyValuePair<string, string>(JoinStringOmitEmpty(Properties.Resources.SEPARATOR, parentPropertyName, prop.Name),
                                                             (prop.GetValue(model) == null) ? null : prop.GetValue(model).ToString())
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
    }
}
