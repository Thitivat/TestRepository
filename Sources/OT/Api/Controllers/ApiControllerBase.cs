using BND.Services.Security.OTP.Api.Models;
using BND.Services.Security.OTP.Api.Utils;
using BND.Services.Security.OTP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace BND.Services.Security.OTP.Api.Controllers
{
    /// <summary>
    /// Abstract class ApiControllerBase is a template class for all controllers of one time password api.
    /// It provides some necessary properties such as api key and account id, some methods for reuse in every controllers.
    /// </summary>
    public abstract class ApiControllerBase : ApiController
    {
        #region [Fields]

        /// <summary>
        /// The api manager field providing all controllers to use.
        /// </summary>
        protected IApiManager ApiManager;

        #endregion


        #region [Properties]

        /// <summary>
        /// Gets the API key from header.
        /// </summary>
        /// <value>The API key.</value>
        /// <exception cref="System.ArgumentException">When there is no api key on header.</exception>
        protected string ApiKey
        {
            get
            {
                // Checks authorization header.
                if (Request.Headers.Authorization == null)
                {
                    throw new ArgumentException(String.Format(Properties.Resources.ErrorHeaderRequired, Properties.Resources.HEADER_AUTH));
                }

                // Returns authorization header as api key.
                return Request.Headers.Authorization.ToString();
            }
        }

        /// <summary>
        /// Gets the account identifier from header.
        /// </summary>
        /// <value>The account identifier.</value>
        /// <exception cref="System.ArgumentException">When there is no account id on header.</exception>
        protected string AccountId
        {
            get
            {
                // Checks account id custom header.
                if (!Request.Headers.Contains(Properties.Resources.HEADER_ACC_ID))
                {
                    throw new ArgumentException(String.Format(Properties.Resources.ErrorHeaderRequired, Properties.Resources.HEADER_ACC_ID));
                }

                // Returns account id.
                return Request.Headers.GetValues(Properties.Resources.HEADER_ACC_ID).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the client ip.
        /// </summary>
        /// <value>The client ip.</value>
        protected string ClientIp
        {
            get
            {
                // Returns client ip.
                return ((HttpContextBase)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
        }

        /// <summary>
        /// Gets the client user agent.
        /// </summary>
        /// <value>The client user agent.</value>
        protected string ClientUserAgent
        {
            get
            {
                // Returns user agent.
                return Request.Headers.UserAgent.ToString();
            }
        }

        #endregion


        #region [Methods]

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Releases all resources.
                if (ApiManager != null)
                {
                    ApiManager.Dispose();
                    ApiManager = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// This is a method providing work template for all actions of all controllers, thier actions do not need to try..catch,
        /// log, or whatever that is the same processes so we can reuse anything here for all of them.
        /// </summary>
        /// <param name="work">The work.</param>
        /// <returns>IHttpActionResult.</returns>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        protected IHttpActionResult Process(Func<IHttpActionResult> work)
        {
            try
            {
                // Creates instance of ApiManager class with settings.
                if (ApiManager == null)
                {
                    ApiManager = new ApiManager(new ApiSettings
                    {
                        ApiKey = ApiKey,
                        AccountId = AccountId,
                        ClientIp = ClientIp,
                        UserAgent = ClientUserAgent,
                        ConnectionString = ConfigurationManager.ConnectionStrings[Properties.Resources.DB_CONN_KEY].ConnectionString,
                        ChannelPluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Resources.PLUGIN_FOLDER),
                        RefCodeLength = Convert.ToInt32(ConfigurationManager.AppSettings["RefCodeLength"]),
                        OtpIdLength = Convert.ToInt32(ConfigurationManager.AppSettings["OtpIdLength"]),
                        OtpCodeLength = Convert.ToInt32(ConfigurationManager.AppSettings["OtpCodeLength"])
                    });
                }

                // Verifies account first.
                ApiManager.VerifyAccount();

                // Performs specified work of each action of each controller.
                return work();
            }
            catch (Exception ex)
            {
                // If something goes wrong, call Error method to response back to client properly.
                return Error(ex);
            }
        }

        /// <summary>
        /// The method for responding error with proper http status code and error object.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>An ErrorResult object.</returns>
        protected ErrorResult Error(Exception exception)
        {
            // Creates error model for sending to client.
            ApiErrorModel errorModel = new ApiErrorModel { StatusCode = HttpStatusCode.InternalServerError };
            // Creates collection of errors to store error messages.
            List<ErrorMessageModel> errorMessage = new List<ErrorMessageModel>();

            // Analysis the exception for setting proper http status code and message to error model.
            if (exception is UnauthorizedAccessException)
            {
                errorModel.StatusCode = HttpStatusCode.Unauthorized;
                errorMessage.Add(new ErrorMessageModel { Message = exception.Message });
            }
            else if (exception is ArgumentException)
            {
                errorModel.StatusCode = HttpStatusCode.BadRequest;
                // Sets parameter name to key.
                errorMessage.Add(new ErrorMessageModel { Key = ((ArgumentException)exception).ParamName, Message = exception.Message });
            }
            else if (exception is ChannelOperationException)
            {
                errorModel.ErrorCode = ((ChannelOperationException)exception).ErrorCode;
                // Checks error code, if it is an error which comes from api manager, sets collection of errors to there.
                if (errorModel.ErrorCode == 4)
                {
                    errorMessage.AddRange(JsonConvert.DeserializeObject<List<ErrorMessageModel>>(exception.Message));
                }
                else
                {
                    errorMessage.Add(new ErrorMessageModel { Message = exception.Message });
                }
            }
            else if (exception is DbEntityValidationException)
            {
                // Adds error messages from EF to error model.
                foreach (DbEntityValidationResult validation in ((DbEntityValidationException)exception).EntityValidationErrors)
                {
                    errorMessage.AddRange(
                        validation.ValidationErrors.Select(e => new ErrorMessageModel { Key = e.PropertyName, Message = e.ErrorMessage })
                    );
                }
            }
            else
            {
                errorMessage.Add(new ErrorMessageModel { Message = exception.Message });
            }

            // Updates error messages to model.
            errorModel.Messages = errorMessage.ToArray();

            // Returns error model.
            return new ErrorResult(Request, errorModel);
        }

        #endregion
    }
}
