using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BND.Services.Security.OTP.Plugins
{
    [ChannelExport("SMS", "The sms channel for send sms message via message bird api.")]
    public class SmsPluginChannel : PluginChannelBase
    {
        #region [Fields]
        /// <summary>
        /// The message bird client for manipulate with message bird api that message bird provides.
        /// </summary>
        private readonly Client _messageBirdClient;
        /// <summary>
        /// The config file name.
        /// </summary>
        private const string ConfigFilename = @"PluginSettings.xml";
        /// <summary>
        /// The sending status.
        /// </summary>
        private bool _isSuccess;
        /// <summary>
        /// The message result from plugin.
        /// </summary>
        private readonly List<PluginChannelMessage> _messages = new List<PluginChannelMessage>();

        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsPluginChannel"/> class.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">accessKey is empty</exception>
        public SmsPluginChannel()
        {
            var setting = new PluginSetting(Path.Combine(GetCurrentDirectory(), ConfigFilename), "SMS");
            _messageBirdClient = Client.CreateDefault(setting.Parameters["SmsAccessKey"].Value);
        }

        #endregion

        #region [Public Method]
        /// <summary>
        /// This method intended to sends the sms via message .
        /// </summary>
        /// <param name="parameters">The <see cref="BND.Services.Security.OTP.Plugins.ChannelParams" /> object.</param>
        /// <exception cref="System.ArgumentNullException">TypeInvalid or SenderEmpty or MessageEmpty or AddressEmpty</exception>
        public override PluginChannelResult Send(ChannelParams parameters)
        {
            // validate parameters are not null or empty
            if (String.IsNullOrWhiteSpace(parameters.Sender))
            {
                throw new ArgumentException("Sender");
            }

            if (String.IsNullOrWhiteSpace(parameters.Message))
            {
                throw new ArgumentException("Message");
            }

            if (String.IsNullOrWhiteSpace(parameters.Address))
            {
                throw new ArgumentException("Address");
            }

            if (string.IsNullOrWhiteSpace(parameters.RefCode))
            {
                throw new ArgumentException("parameters.RefCode");
            }

            if (string.IsNullOrWhiteSpace(parameters.OtpCode))
            {
                throw new ArgumentException("parameters.OtpCode");
            }

            try
            {
                SetSendingMessage(parameters);

                // send message.
                Message result = _messageBirdClient.SendMessage(parameters.Sender, parameters.Message, ConvertMobileNumber(parameters.Address));

                foreach (var item in result.Recipients.Items)
                {
                    // don't know how to produce this case.
                    if (item.Status != Recipient.RecipientStatus.Sent)
                    {
                        _messages.Add(new PluginChannelMessage { Message = string.Format("The status of message is {0}", item.Status), Type = 1 });
                    }
                }
                // set succes field to true if status contain success.
                if (result.Recipients.Items.All(x => x.Status == Recipient.RecipientStatus.Sent))
                {
                    _isSuccess = true;
                }
            }
            catch (ErrorException ex)
            {
                _isSuccess = false;
                if (ex.Errors != null)
                {
                    foreach (var error in ex.Errors)
                    {
                        _messages.Add(new PluginChannelMessage { Message = error.Description, Type = 0, Key = error.Parameter });
                    }
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                string key = "unknown";
                if (ex.Message == Properties.Resources.INVALID_MOBILE_FORMAT)
                {
                    key = "MobileNumber";
                }
                _messages.Add(new PluginChannelMessage { Message = ex.Message, Type = 0, Key = key });
            }

            return new PluginChannelResult(_isSuccess, _messages);
        }
        #endregion

        #region [Private Method]
        /// <summary>
        /// Converts the mobile numbers to long array for send message.
        /// </summary>
        /// <param name="phoneNumbers">The phone numbers in text seperate with comma.</param>
        /// <exception cref="System.ArgumentException">Input phone number is/are invalid format.</exception>
        private long[] ConvertMobileNumber(string phoneNumbers)
        {
            try
            {
                // accept only one number per time.
                long[] phoneList = new long[] { Convert.ToInt64(phoneNumbers) };
                return phoneList;
            }
            catch (Exception)
            {
                throw new ArgumentException(Properties.Resources.INVALID_MOBILE_FORMAT);
            }
        }
        #endregion
    }
}
