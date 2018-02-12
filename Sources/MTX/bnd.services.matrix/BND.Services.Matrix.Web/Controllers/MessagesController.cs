using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;

using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Infrastructure.WebAPI.Controllers;
using BND.Services.Matrix.Business.Interfaces;
using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Enums;
using BND.Services.Matrix.Web.BinderProviders;

namespace BND.Services.Matrix.Web.Controllers
{
    /// <summary>
    /// The <see cref="MessagesController"/> class
    /// </summary>
    [RoutePrefix("v1/messages")]
    public class MessagesController : BaseApiController<EnumErrorCodes>
    {
        /// <summary>
        /// The <see cref="IMatrixManager"/>
        /// </summary>
        private readonly IMatrixManager _matrixManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="manager"> The <see cref="IMatrixManager"/>. </param>
        public MessagesController(IMatrixManager manager)
        {
            _matrixManager = manager;
        }

        /// <summary>
        /// Gets a list of messages from Matrix.
        /// </summary>
        /// <param name="filters"> The <see cref="MessageFilters"/>. </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <remarks>
        /// Get a <see cref="List{T}"/> where T is of type <see cref="Message"/> from Matrix.
        /// On success the <see cref="List{T}"/> and a response status code of 200 (OK) is returned.
        /// On failure, it can return a number of different status codes depending on the type error, e.g 400 (Bad request), 404 (Not Found), 500 (Internal server error)
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(List<Entities.Message>))]
        public IHttpActionResult GetMessages([ModelBinder(typeof(MessageFiltersModelBinderProvider))]MessageFilters filters = null)
        {
            try
            {
                var result = _matrixManager.GetMessages(filters);
                return Ok(result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }

        /// <summary>
        /// Gets a message details from Matrix for specified <paramref name="id"/>
        /// </summary>
        /// <param name="id"> The message id. </param>
        /// <param name="messageType"> The <see cref="EnumMessageTypes"/> </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        /// <remarks>
        /// Get a <see cref="Message"/> from Matrix.
        /// On success the <see cref="Message"/> and a response status code of 200 (OK) is returned.
        /// On failure, it can return a number of different status codes depending on the type error, e.g 400 (Bad request), 404 (Not Found), 500 (Internal server error)
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessageDetails(int id, EnumMessageTypes messageType)
        {
            try
            {
                var result = _matrixManager.GetMessageDetails(id, messageType);
                return Ok(result);
            }
            catch (BaseException ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
            catch (Exception ex)
            {
                throw CreateWrapperException(ex, EnumErrorCodes.ServiceLayerError, GetHelpLink());
            }
        }
    }
}