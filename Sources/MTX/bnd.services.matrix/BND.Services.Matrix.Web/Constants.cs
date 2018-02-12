namespace BND.Services.Matrix.Web
{
    /// <summary>
    /// Constants used throughout the application
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Error code base for service
        /// </summary>
        public static readonly string ErrorCodeBase = "ErrorCodeBase";

        /// <summary>
        /// Swagger base url
        /// </summary>
        public static readonly string SwaggerBaseUrl = "swagger/ui/index#!";

        /// <summary>
        /// The default decimal precision.
        /// </summary>
        public static readonly string DefaultDecimalPrecision = "DefaultDecimalPrecision";

        /// <summary>
        /// App settings security activation key
        /// </summary>
        public static readonly string AuthSecurityEnabled = "AuthSecurityEnabled";

        /// <summary>
        /// The authorization server url.
        /// </summary>
        public static readonly string AuthServerUrl = "AuthServerUrl";

        /// <summary>
        /// The products scope
        /// </summary>
        public static readonly string AuthMatrixScope = "AuthMatrixScope";

        /// <summary>
        /// The fields parameter in query string
        /// </summary>
        public static readonly string QuerystringFieldsParameter = "fields";
    }
}