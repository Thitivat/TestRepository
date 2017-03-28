using BND.Services.Security.OTP.Dal;
using BND.Services.Security.OTP.Dal.Pocos;
using BND.Services.Security.OTP.Interfaces;
using BND.Services.Security.OTP.Models;
using BND.Services.Security.OTP.Plugins;
using BND.Services.Security.OTP.Prng;
using BND.Services.Security.OTP.Repositories.Interfaces;
using BND.Services.Security.OTP.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BND.Services.Security.OTP.Api.Utils
{
    /// <summary>
    /// Class ApiManager is a business logic layer of one time password api, it povides everything what any action needs.
    /// </summary>
    public class ApiManager : IApiManager
    {
        #region [Fields]

        /// <summary>
        /// The disposed flag.
        /// </summary>
        public bool Disposed;
        /// <summary>
        /// The api settings to store all settings of api.
        /// </summary>
        private ApiSettings _apiSettings;
        /// <summary>
        /// The _otpuow field to store one time password unit of work for manipulating data in database.
        /// </summary>
        private IUnitOfWork _otpUow;
        /// <summary>
        /// The _channelFactory field to store channel factory for providing any channel to send
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code
        /// </summary>
        private IChannelFactory _channelFactory;
        /// <summary>
        /// Enum OtpStatus is a enum representing EnumStatus table in database.
        /// </summary>
        private enum OtpStatus
        {
            Canceled,
            Deleted,
            Expired,
            Locked,
            Pending,
            Verified,
            Reserved // For unit test
        }

        #endregion


        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="BND.Services.Security.OTP.Api.Utils.ApiManager" /> class.
        /// </summary>
        /// <param name="apiSettings">The api settings.</param>
        /// <exception cref="System.ArgumentNullException"> settings or ApiKey or AccountId or ConnectionString or
        /// DataDirectory or OtpIdLength or OtpCodeLength or RefCodeLength </exception>
        public ApiManager(ApiSettings apiSettings)
        {
            if (apiSettings == null)
            {
                throw new ArgumentNullException("apiSettings", String.Format(Properties.Resources.ErrorArgNull, "apiSettings"));
            }
            // Checks required parameters.
            if (String.IsNullOrEmpty(apiSettings.ApiKey))
            {
                throw new ArgumentException("apiSettings.ApiKey", String.Format(Properties.Resources.ErrorArgNull, "apiSettings.ApiKey"));
            }
            if (String.IsNullOrEmpty(apiSettings.AccountId))
            {
                throw new ArgumentException("apiSettings.AccountId",
                                                String.Format(Properties.Resources.ErrorArgNull, "apiSettings.AccountId"));
            }
            if (String.IsNullOrEmpty(apiSettings.ConnectionString))
            {
                throw new ArgumentException("apiSettings.ConnectionString",
                                                String.Format(Properties.Resources.ErrorArgNull, "apiSettings.ConnectionString"));
            }
            if (String.IsNullOrEmpty(apiSettings.ChannelPluginsPath))
            {
                throw new ArgumentException("apiSettings.ChannelPluginsPath",
                                                String.Format(Properties.Resources.ErrorArgNull, "apiSettings.ChannelPluginsPath"));
            }
            if (apiSettings.OtpIdLength == default(int))
            {
                throw new ArgumentException("apiSettings.OtpIdLength",
                                                String.Format(Properties.Resources.ErrorArgNull, "apiSettings.OtpIdLength"));
            }
            if (apiSettings.OtpCodeLength == default(int))
            {
                throw new ArgumentException("apiSettings.OtpCodeLength",
                                                String.Format(Properties.Resources.ErrorArgNull, "apiSettings.OtpCodeLength"));
            }
            if (apiSettings.RefCodeLength == default(int))
            {
                throw new ArgumentException("apiSettings.RefCodeLength",
                                                String.Format(Properties.Resources.ErrorArgNull, "apiSettings.RefCodeLength"));
            }

            // Sets all fields
            _apiSettings = apiSettings;

            // Creates instance of all fields.
            _otpUow = new OtpUnitOfWork(apiSettings.ConnectionString);
            _channelFactory = new ChannelFactory(apiSettings.ChannelPluginsPath);
        }

        #endregion


        #region [Methods]

        /// <summary>
        /// Verifies an account by using api key and account id.
        /// </summary>
        /// <exception cref="System.UnauthorizedAccessException">
        /// When api key and/or account id are wrong
        /// or
        /// When account has been locked.
        /// </exception>
        public void VerifyAccount()
        {
            // Gets account by account id.
            Account account = _otpUow.GetRepository<Account>().GetById(new Guid(_apiSettings.AccountId));
            // Checks account and api key have to matched with aki key in database.
            if (account == null || Hash(_apiSettings.ApiKey, account.Salt) != account.ApiKey)
            {
                throw new UnauthorizedAccessException(Properties.Resources.ErrrorUnauthorized);
            }
            // Checks account status.
            if (!account.IsActive)
            {
                throw new UnauthorizedAccessException(Properties.Resources.ErrorAccountIsLocked);
            }
        }

        /// <summary>
        /// Adds a new <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code to database.
        /// </summary>
        /// <param name="otpRequest">The <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> request.</param>
        /// <returns>
        /// An <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a>
        /// object representing output of api which response back to client.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// otpRequest
        /// or
        /// otpRequest.Channel.Type
        /// or
        /// otpRequest.Channel.Sender
        /// or
        /// otpRequest.Channel.Address
        /// or
        /// otpRequest.Channel.Message
        /// or
        /// otpRequest.Suid
        /// </exception>
        /// <exception cref="ChannelOperationException">Could not send OTP code, Error Code: 4</exception>
        public OtpResultModel AddOtp(OtpRequestModel otpRequest)
        {
            // Checks required parameters.
            if (otpRequest == null)
            {
                throw new ArgumentNullException("otpRequest", String.Format(Properties.Resources.ErrorArgNull, "otpRequest"));
            }
            if (String.IsNullOrEmpty(otpRequest.Suid))
            {
                throw new ArgumentNullException("otpRequest.Suid", String.Format(Properties.Resources.ErrorArgNull, "otpRequest.Suid"));
            }
            if (otpRequest.Channel == null)
            {
                throw new ArgumentNullException("otpRequest.Channel", String.Format(Properties.Resources.ErrorArgNull, "otpRequest.Channel"));
            }
            if (String.IsNullOrEmpty(otpRequest.Channel.Type))
            {
                throw new ArgumentNullException("otpRequest.Channel.Type",
                                                String.Format(Properties.Resources.ErrorArgNull, "otpRequest.Channel.Type"));
            }
            if (String.IsNullOrEmpty(otpRequest.Channel.Sender))
            {
                throw new ArgumentNullException("otpRequest.Channel.Sender",
                                                String.Format(Properties.Resources.ErrorArgNull, "otpRequest.Channel.Sender"));
            }
            if (String.IsNullOrEmpty(otpRequest.Channel.Address))
            {
                throw new ArgumentNullException("otpRequest.Channel.Address",
                                                String.Format(Properties.Resources.ErrorArgNull, "otpRequest.Channel.Address"));
            }
            if (String.IsNullOrEmpty(otpRequest.Channel.Message))
            {
                throw new ArgumentNullException("otpRequest.Channel.Message",
                                                String.Format(Properties.Resources.ErrorArgNull, "otpRequest.Channel.Message"));
            }

            // Checks duplicate suid with same account id.
            OneTimePassword existingOtp =
                _otpUow.GetRepository<OneTimePassword>().GetQueryable(o => o.Suid == otpRequest.Suid &&
                                                                           o.AccountId == new Guid(_apiSettings.AccountId))
                                                        .FirstOrDefault();
            if (existingOtp != null)
            {
                // Throws an exception when suid already exists in database.
                throw new ArgumentException(Properties.Resources.ErrorDuplicateSuid, "otpRequest.Suid");
            }

            // Maps otp request object to OneTimePassword poco.
            OneTimePassword otp = AutoMapper.Mapper.Map<OneTimePassword>(otpRequest);
            // Gets expiry period from setting table.
            double expiryPeriod = Convert.ToDouble(_otpUow.GetRepository<Setting>().GetById("Expiration").Value);
            // Sets otp id with random key
            otp.OtpId = KeyGenerator.RandomKey(_apiSettings.OtpIdLength);
            otp.AccountId = new Guid(_apiSettings.AccountId);
            // Sets expiry date by using expiration value in settings table of database.
            otp.ExpiryDate = DateTime.Now.AddSeconds(expiryPeriod);
            // Sets ref code with random characters.
            otp.RefCode = KeyGenerator.RandomChars(_apiSettings.RefCodeLength).ToUpper();
            // Sets otp code with random numbers.
            otp.Code = KeyGenerator.RandomDigits(_apiSettings.OtpCodeLength);
            // Sets status to pending.
            otp.Status = OtpStatus.Pending.ToString();

            // Adds to database.
            _otpUow.GetRepository<OneTimePassword>().Insert(otp);
            _otpUow.Execute();

            // Prepares inputs for sending otp code via channel factory.
            ChannelParams channelParams = AutoMapper.Mapper.Map<ChannelParams>(otp);
            channelParams.ExpiryPeriod = expiryPeriod;

            // Sends otp code by using channel factory.
            PluginChannelResult result = _channelFactory.GetChannel(otpRequest.Channel.Type).Send(channelParams);
            // Checks result.
            if (!result.Success)
            {
                // Throws an exception.
                throw new ChannelOperationException(4, JsonConvert.SerializeObject(result.Messages));
            }

            // Returns otp object.
            return AutoMapper.Mapper.Map<OtpResultModel>(otp);
        }

        /// <summary>
        /// Verifies the <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code.
        /// </summary>
        /// <param name="otpId">The <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> identifier.</param>
        /// <param name="enteredOtpCode">
        /// The entered <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a>
        /// code as JObject which retrieved from client.
        /// </param>
        /// <returns>An <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> object as OtpModel.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// otpId is null
        /// or
        /// otpId could not be found.
        /// </exception>
        /// <exception cref="System.UnauthorizedAccessException">
        /// <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> code is invalid because of some reason.
        /// </exception>
        /// <exception cref="System.NotImplementedException">
        /// There is a <a href="https://en.wikipedia.org/wiki/One-time_password" target="_blank">OTP</a> status which not implement yet in code.
        /// </exception>
        public OtpModel VerifyOtp(string otpId, JObject enteredOtpCode)
        {
            // Checks required parameters.
            if (String.IsNullOrEmpty(otpId))
            {
                throw new ArgumentNullException("otpId", String.Format(Properties.Resources.ErrorArgNull, "otpId"));
            }
            JToken otpCodeObject;
            if (enteredOtpCode == null || (otpCodeObject = enteredOtpCode.GetValue("otpCode")) == null)
            {
                throw new ArgumentNullException("enteredOtpCode", String.Format(Properties.Resources.ErrorArgNull, "enteredOtpCode"));
            }

            // Checks otp id exists.
            OneTimePassword otpPoco = _otpUow.GetRepository<OneTimePassword>().GetById(otpId);
            if (otpPoco == null)
            {
                throw new ArgumentNullException("otpId", String.Format(Properties.Resources.ErrorNotFound, "otpId"));
            }

            // Checks otp status, it must be pending, if not, throws exception with good reason.
            if (otpPoco.Status != OtpStatus.Pending.ToString())
            {
                // Checks status first.
                OtpStatus otpStatus;
                if (Enum.TryParse<OtpStatus>(otpPoco.Status, true, out otpStatus))
                {
                    switch ((OtpStatus)Enum.Parse(typeof(OtpStatus), otpPoco.Status))
                    {
                        case OtpStatus.Canceled:
                        case OtpStatus.Deleted:
                        case OtpStatus.Locked:
                        case OtpStatus.Verified:
                            throw new UnauthorizedAccessException(String.Format(Properties.Resources.ErrorOtpInvalidStatusPassive,
                                                                                otpPoco.Status.ToLower()));
                        case OtpStatus.Expired:
                            throw new UnauthorizedAccessException(String.Format(Properties.Resources.ErrorOtpInvalidStatusActive,
                                                                                otpPoco.Status.ToLower()));
                        default:
                            // In case, new status comes and did not implement yet.
                            throw new NotImplementedException(String.Format(Properties.Resources.ErrorOtpInvalidStatusNotImplement,
                                                                            otpPoco.Status));
                    }
                }
                else
                {
                    // In case, OtpStatus enum cannot support new status.
                    throw new NotSupportedException(String.Format(Properties.Resources.ErrorOtpInvalidStatusNotSupport, otpPoco.Status));
                }
            }

            // Declares variable to keep error message when otp code is invalid.
            string errorMessage = null;

            // Verifies otp code by compare entered code with otp code in database.
            if (otpPoco.Code != enteredOtpCode.GetValue("otpCode").ToObject<string>())
            {
                // Adds data to attempt table.
                _otpUow.GetRepository<Attempt>().Insert(new Attempt
                {
                    OtpId = otpPoco.OtpId,
                    Date = DateTime.Now,
                    IpAddress = _apiSettings.ClientIp,
                    UserAgent = _apiSettings.UserAgent
                });

                // Gets attempt amount and compare with retry count which got from setting table.
                int maxRetryCount = Convert.ToInt32(_otpUow.GetRepository<Setting>().GetById("RetryCount").Value);

                // If attempt amount equals or exceeds retry count.
                if (_otpUow.GetRepository<Attempt>().GetCount(a => a.OtpId == otpPoco.OtpId) + 1 >= maxRetryCount)
                {
                    // Updates otp status to be Locked.
                    otpPoco.Status = OtpStatus.Locked.ToString();
                }

                // Sets error message.
                errorMessage = Properties.Resources.ErrorOtpInvalid;
            }
            // Check expiry date when otp code is valid.
            else if (otpPoco.ExpiryDate <= DateTime.Now)
            {
                // Updates status to be expired.
                otpPoco.Status = OtpStatus.Expired.ToString();
                // Sets error message.
                errorMessage = String.Format(Properties.Resources.ErrorOtpInvalidStatusActive, otpPoco.Status.ToLower());
            }
            else
            {
                // Updates otp status to be Verified when otp code is valid.
                otpPoco.Status = OtpStatus.Verified.ToString();
            }

            // Updates all in database.
            _otpUow.Execute();

            // Throws an exception if otp code is invalid.
            if (errorMessage != null)
            {
                throw new UnauthorizedAccessException(errorMessage);
            }

            // Returns otp model if valid.
            return AutoMapper.Mapper.Map<OtpModel>(otpPoco);
        }

        /// <summary>
        /// Gets all channel names.
        /// </summary>
        /// <returns>A collection of channel names.</returns>
        public IEnumerable<string> GetChannelNames()
        {
            return _channelFactory.GetAllChannelTypeNames();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            Dispose(true);
            // Clears garbage collector.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Releases all resources.
                if (_otpUow != null)
                {
                    _otpUow.Dispose();
                    _otpUow = null;
                }
                if (_channelFactory != null)
                {
                    _channelFactory.Dispose();
                    _channelFactory = null;
                }

                // Sets dispose flag.
                Disposed = true;
            }
        }

        /// <summary>
        /// Hashes the specified api key.
        /// </summary>
        /// <param name="plainText">The api key.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>A hashed api key.</returns>
        private string Hash(string plainText, string salt)
        {
            // Hashes by using SHA-512 with salt.
            using (SHA512CryptoServiceProvider hash = new SHA512CryptoServiceProvider())
            {
                return Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(plainText + salt)));
            }
        }

        #endregion
    }
}