using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BND.Services.Payments.PaymentIdInfo.Entities.Helpers
{
    public class EnumFilterTypeHelper
    {
        /// <summary>
        /// Converts to enum filter types.
        /// </summary>
        /// <param name="enumList">The enum list.</param>
        /// <returns>IEnumerable{EnumFilterType}.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static IEnumerable<EnumFilterType> ConvertToEnumFilterTypes(string enumList)
        {
            IList<EnumFilterType> result = new List<EnumFilterType>();
            string[] enums = enumList.Split(',');
            for (int i = 0; i < enums.Length; i++)
            {
                EnumFilterType enumFilterType;
                if (Enum.TryParse(enums[i], out enumFilterType))
                {
                    result.Add(enumFilterType);
                }
                else
                {
                    throw new ArgumentException(String.Format("List of EnumFilterType ({0}) is invalid", enumList));
                }
            }
            return result;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="enumList">The enum list.</param>
        /// <returns>System.String.</returns>
        public static string ConvertToString(IEnumerable<EnumFilterType> enumList)
        {
            return String.Join(", ", enumList.ToArray());
        }
    }
}
