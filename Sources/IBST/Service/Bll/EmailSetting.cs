using System.ComponentModel.DataAnnotations;

namespace BND.Services.IbanStore.Service.Bll
{
    /// <summary>
    /// Class EmailSetting is an internal class for store email smtp configuration.
    /// </summary>
    internal class EmailSetting
    {
        /// <summary>
        /// The mailman component code. This is a chilkat license code for use with chilkat library.
        /// </summary>
        public string MailmanComponentCode { get; set; }
        /// <summary>
        /// The email server address.
        /// </summary>
        public string EmailServer { get; set; }
        /// <summary>
        /// The email SMTP port.
        /// </summary>
        public int EmailSmtpPort { get; set; }

        /// <summary>
        /// The email SMTP Account
        /// </summary>
        public string EmailSmtpAccount { get; set; }

        /// <summary>
        /// The email account of email sender.
        /// </summary>
        public string EmailAccount { get; set; }
        /// <summary>
        /// The email password of email sender.
        /// </summary>
        public string EmailPassword { get; set; }

        /// <summary>
        /// The email From name.
        /// </summary>
        public string EmailFromName { get; set; }
    }
}
