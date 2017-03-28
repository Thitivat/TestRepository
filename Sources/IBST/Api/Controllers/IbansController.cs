using BND.Services.IbanStore.Api.Helpers;
using BND.Services.IbanStore.Models;
using BND.Services.IbanStore.Service.Bll;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;

namespace BND.Services.IbanStore.Api.Controllers
{


    /// <summary>
    /// Class IbansController provides the function to manipulate with iban number. Client can use this api to retrieve iban data by
    /// reserve iban number and assign it to their own use.
    /// </summary>
    public class IbansController : ApiControllerBase
    {
        /// <summary>
        /// The _security
        /// </summary>
        private ISecurity _security;

        /// <summary>
        /// The _iban manager
        /// </summary>
        private IIbanManager _ibanManager;

        /// <summary>
        /// The url format for assign method.
        /// </summary>
        private const string ASSIGN_URL_FORMAT = "/{0}/Assign/{1}";
        /// <summary>
        /// The url format for get method.
        /// </summary>
        private const string GET_URL_FORMAT = "/{0}";

        /// <summary>
        /// Gets the iban manager.
        /// </summary>
        /// <value>The iban manager.</value>

        /// <summary>
        /// Initializes a new instance of the <see cref="IbansController" /> class.
        /// </summary>
        /// <param name="security">The security.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="ibanManagerFactory">The iban manager fctory.</param>
        public IbansController(ISecurity security)
        {
            // create instance
            _security = security;

            string token = System.Web.HttpContext.Current.Request.Headers["Authorization"];
            string uidPrefix = System.Web.HttpContext.Current.Request.Headers["Uid-Prefix"];

            //resolve instance inject parameter
            _ibanManager = WindsorConfig._container.Resolve<IIbanManager>(new
            {
                username = _security.GetUserCredential(token, uidPrefix).Token,
                context = ConfigurationManager.AppSettings["Context"],
                connectionString = ConfigurationManager.ConnectionStrings[Properties.Resources.DB_CONN_KEY].ConnectionString
            });
        }

        /// <summary>
        /// This method intended to reserves iban if user already reserved this will get iban data from NextAvailableIbanAlreadyReservedException.
        /// </summary>
        /// <param name="uid">The client uid.</param>
        [HttpPut]
        [Route("NextAvailable/{uid}")]
        public IHttpActionResult ReserveNextAvailable(string uid)
        {
            string baseUrl = String.Empty;


            var ibanResult = _ibanManager.Reserve(uid, UidPrefix);

            // get base url.
            baseUrl = GetBaseUrl() + String.Format(ASSIGN_URL_FORMAT, ibanResult.IbanId, uid);
            var response = Request.CreateResponse(HttpStatusCode.OK, new { IbanId = ibanResult.IbanId });
            response.Headers.Add("Location", baseUrl);


            return ResponseMessage(response);

        }

        /// <summary>
        /// This method intended to assigns the specified iban to client uid.
        /// </summary>
        /// <param name="ibanId">The iban identifier.</param>
        /// <param name="uid">The client uid.</param>
        [HttpPut]
        [Route("{ibanId:int}/Assign/{uid}")]
        public IHttpActionResult Assign(int ibanId, string uid)
        {
            string baseUrl = String.Empty;
            try
            {

                // call reserve method with uid and uid prefix and return status ok if assign success.
                Iban result = _ibanManager.Assign(ibanId, uid, UidPrefix);
                baseUrl = GetBaseUrl() + String.Format(GET_URL_FORMAT, result.Code);
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Location", baseUrl);
                return ResponseMessage(response);


            }
            catch (IbanAlreadyAssignedException ex)
            {
                baseUrl = GetBaseUrl() + String.Format(GET_URL_FORMAT, uid);
                // in case request again will return status notmodified with old data.
                var response = Request.CreateResponse(HttpStatusCode.NotModified);
                response.Headers.Add("Location", baseUrl);
                return ResponseMessage(response);

            }
        }

        /// <summary>
        /// This method intended to gets the specified iban by use iban code.
        /// </summary>
        /// <param name="ibanCode">The iban code.</param>
        /// <exception cref="System.IO.FileNotFoundException">Iban cannot be found</exception>
        /// <exception cref="System.ArgumentNullException">iban</exception>
        [Route("{uid}")]
        public IHttpActionResult Get(string uid)
        {
            // call get method with iban code.
            Iban iban = _ibanManager.Get(uid, UidPrefix);
            // check if cannot get iban data will return not found message
            if (iban == null)
            {
                // NL91ABNA0417164300
                throw new FileNotFoundException("Iban cannot be found");
            }

            return Ok(iban);
        }

       
        /// <summary>
        /// This method intended to retrieve base URL and return it back.
        /// </summary>
        private string GetBaseUrl()
        {
            return Url.Link(
                "DefaultApi",
                new Dictionary<string, object>
                {
                    { "controller", this.GetType().Name.Substring(0, this.GetType().Name.Length - "Controller".Length) }
                }
            );
        }
    }
}
