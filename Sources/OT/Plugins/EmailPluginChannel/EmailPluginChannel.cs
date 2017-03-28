using BND.Services.MailMan.Entities;
using BND.Services.MailMan.Proxy.Implementations;
using BND.Services.MailMan.Proxy.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using BND.Services.Infrastructure.Common.Extensions;
using System.Text.RegularExpressions;
using BND.Services.MailMan.Enums;

namespace BND.Services.Security.OTP.Plugins
{
    [ChannelExport("EMAIL", "The email channel for sending the message via email.")]
    public class EmailPluginChannel : PluginChannelBase
    {
        #region [Properties]
        /// <summary>
        /// The filename of config value
        /// </summary>
        private const string ConfigFilename = @"PluginSettings.xml";

        /// <summary>
        /// The mailman api url
        /// </summary>
        private readonly string _mailmanApiUrl;

        /// <summary>
        /// The indicator to reflect if email was send already.
        /// </summary>
        private bool _isSuccess;

        /// <summary>
        /// The message from
        /// </summary>
        private readonly List<PluginChannelMessage> _messages = new List<PluginChannelMessage>();
        /// <summary>
        /// The plugin setting
        /// </summary>
        private readonly PluginSetting _setting;
        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailPluginChannel"/> class.
        /// </summary>
        public EmailPluginChannel()
        {
            _setting = new PluginSetting(Path.Combine(GetCurrentDirectory(), ConfigFilename), "EMAIL");
            _mailmanApiUrl = _setting.Parameters["MailManUrl"].Value;
        }
        #endregion

        private bool IsEmailAddress(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        #region [Public Method]
        /// <summary>
        /// This method intended to sends the otp via email
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>PluginChannelResult.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// parameters.Sender
        /// or
        /// parameters.Message
        /// or
        /// parameters.Address
        /// or
        /// parameters.RefCode
        /// or
        /// parameters.OtpCode
        /// </exception>
        public override PluginChannelResult Send(ChannelParams parameters)
        {
            // validate parameter are not null or empty
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            if (string.IsNullOrWhiteSpace(parameters.Sender))
            {
                throw new ArgumentException("parameters.Sender");
            }

            if (string.IsNullOrWhiteSpace(parameters.Message))
            {
                throw new ArgumentException("parameters.Message");
            }

            if (string.IsNullOrWhiteSpace(parameters.Address))
            {
                throw new ArgumentException("parameters.Address");
            }

            if (!IsEmailAddress(parameters.Address))
            {
                throw new ArgumentException("parameters.Address is not email.");
            }

            if (string.IsNullOrWhiteSpace(parameters.RefCode))
            {
                throw new ArgumentException("parameters.RefCode");
            }

            if (string.IsNullOrWhiteSpace(parameters.OtpCode))
            {
                throw new ArgumentException("parameters.OtpCode");
            }

            // set message by replace placeholder
            SetSendingMessage(parameters);

            // send email via MailManApi
            Message message = new Message
            {
                Subject = _setting.Parameters["EmailSubject"].Value,
                FromName = parameters.Sender,
                From = _setting.Parameters["EmailAccount"].Value,
                To = parameters.Address,
                Body = (String.Format("<pre>{0}</pre>", parameters.Message)).EncodeStringToBase64(),
                Mode = EnumSendingMode.Immediate,
            };
            Send(message);

            return new PluginChannelResult(_isSuccess, _messages);
        }
        #endregion

        #region [Private Method]

        /// <summary>
        /// Initials the main man API.
        /// </summary>
        /// <returns>IMailManApi.</returns>
        private IMailManApi InitialMainManApi()
        {
            IMailManApiConfig mailManApiConfig = new MailManApiConfig(_mailmanApiUrl);
            IMailManApi mailManProxy = new MailManApi(mailManApiConfig);
            return mailManProxy;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Task{System.Boolean}.</returns>
        private void Send(Message message)
        {
            bool isSuccess = false;
            IMailManApi mailManApi = InitialMainManApi();
            try
            {
                string accessToken = "off";
                int result = mailManApi.SendAsync(message, accessToken).Result;
                if (result != 0)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Empty;
                do
                {
                    errorMessage += ex.Message + Environment.NewLine;
                    ex = ex.InnerException;
                } while (ex != null);

                _messages.Add(new PluginChannelMessage
                {
                    Key = "MailManApi",
                    Message = errorMessage,
                });
            }
            _isSuccess = isSuccess;
        }

        #endregion
    }
}
