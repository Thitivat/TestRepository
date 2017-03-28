using System.Collections.Generic;

using BND.Services.MailMan.Enums;

using Newtonsoft.Json;

namespace BND.Services.MailMan.Entities
{
    /// <summary>
    /// The message entity.
    /// </summary>
    [JsonObject("Message entity")]
    public class Message
    {
        public Message()
        {
            ToList = new List<string>();
            Attachments = new List<Attachment>();
        }

        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        [JsonProperty("Body", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the list of attachments.
        /// </summary>
        [JsonProperty("Attachments", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the email sender.
        /// </summary>
        [JsonProperty("From", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the email senders name.
        /// </summary>
        [JsonProperty("FromName", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the email receiver.
        /// </summary>
        [JsonProperty("To", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the receivers list.
        /// </summary>
        [JsonProperty("ToList", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public List<string> ToList { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        [JsonProperty("Subject", NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Sending mode.
        /// </summary>
        [JsonProperty("Mode", DefaultValueHandling = DefaultValueHandling.Include)]
        public EnumSendingMode Mode { get; set; }
    }
}
