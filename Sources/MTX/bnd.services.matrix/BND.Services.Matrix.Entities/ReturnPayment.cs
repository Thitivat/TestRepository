
using Newtonsoft.Json;

namespace BND.Services.Matrix.Entities
{
    /// <summary>
    /// The return payment entity
    /// </summary>
    [JsonObject("ReturnPayment")]
    public class ReturnPayment
    {
        /// <summary>
        /// The movement id
        /// </summary>
        public int MovementId { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The return code
        /// </summary>
        public string SepaReturnCode { get; set; }
    }
}
