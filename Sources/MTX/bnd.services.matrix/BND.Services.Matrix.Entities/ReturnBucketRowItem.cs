
using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The return bucket row item
    /// </summary>
    [JsonObject("ReturnBucketRowItem")]
    public class ReturnBucketRowItem
    {
        /// <summary>
        /// The row id
        /// </summary>
        [JsonProperty("RowId", DefaultValueHandling = DefaultValueHandling.Include)]
        public int RowId { get; set; }

        /// <summary>
        /// The Sepa Return Code
        /// </summary>
        [JsonProperty("SepaReturnCode", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string SepaReturnCode { get; set; }
    }
}
