using System.IO;

using Newtonsoft.Json;

using RestSharp.Serializers;

using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace BND.Services.Matrix.Proxy.NET4.Serializers
{
    /// <summary>
    /// The rest sharp json net serializer.
    /// </summary>
    public class RestSharpJsonNetSerializer : ISerializer
    {
        private readonly JsonSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestSharpJsonNetSerializer"/> class.
        /// </summary>
        public RestSharpJsonNetSerializer()
        {
            ContentType = "application/json";

            // TODO: use the settings of BetterJsonMediaTypeFormatter
            _serializer = new JsonSerializer()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                DateParseHandling = DateParseHandling.DateTime,
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestSharpJsonNetSerializer"/> class.
        /// </summary>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        public RestSharpJsonNetSerializer(JsonSerializer serializer)
        {
            ContentType = "application/json";
            _serializer = serializer;
        }

        /// <summary>
        /// Serialize the object into a string.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    this._serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Gets or sets the root element.
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        public string ContentType { get; set; }
    }
}
