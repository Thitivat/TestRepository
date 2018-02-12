using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;

using BND.Services.Matrix.Entities.Interfaces;

namespace BND.Services.Matrix.Entities.Extensions
{
    /// <summary>
    /// The filters extensions.
    /// </summary>
    public static class FiltersExtensions
    {
        /// <summary>
        /// Create the name value collection.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="obj"> The object </param>
        /// <returns>
        /// The <see cref="NameValueCollection"/>.
        /// </returns>
        public static NameValueCollection ToNameValueCollection<T>(this T obj) where T : class, IQueryStringModel
        {
            var collection = new NameValueCollection();

            var dictionary = obj.GetType().GetProperties().ToDictionary(pi => pi.Name, pi => pi.GetValue(obj, null));
            foreach (var item in dictionary.Where(item => item.Value != null))
            {
                var list = item.Value as IList;
                if (list == null)
                {
                    var enumValue = item.Value as Enum;

                    if (enumValue == null)
                    {
                        if (item.Value != null)
                        {
                            collection.Add(item.Key.ToLowerInvariant(), item.Value.ToString().ToLowerInvariant());
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(enumValue) > 0)
                        {
                            collection.Add(item.Key.ToLowerInvariant(), enumValue.ToString().ToLowerInvariant());
                        }
                    }
                }
                else
                {
                    var result = list.Cast<object>().Select(i => i.ToString().ToLowerInvariant()).ToList();
                    if (result.Count > 0)
                    {
                        var key = item.Key.ToLowerInvariant();
                        collection.Add(key, string.Join(",", result));
                    }
                }
            }

            return collection;
        }
    }
}
