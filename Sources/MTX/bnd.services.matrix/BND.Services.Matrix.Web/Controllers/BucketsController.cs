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
using BND.Services.Matrix.Web.Binders;

namespace BND.Services.Matrix.Web.Controllers
{
    /// <summary>
    /// The <see cref="AccountsController"/> class
    /// </summary>
    [RoutePrefix("v1/buckets")]
    public class BucketsController : BaseApiController<EnumErrorCodes>
    {
        /// <summary>
        /// The <see cref="IMatrixManager"/>
        /// </summary>
        private readonly IMatrixManager _matrixManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BucketsController"/> class.
        /// </summary>
        /// <param name="manager"> The <see cref="IMatrixManager"/>. </param>
        public BucketsController(IMatrixManager manager)
        {
            _matrixManager = manager;
        }

        /// <summary>
        /// The get buckets.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(List<BucketItem>))]
        public IHttpActionResult GetBuckets(
            [ModelBinder(typeof(BucketFilterModelBinder))] BucketItemFilters filter,
            [ModelBinder(typeof(BucketExtraFieldsModelBinder))] BucketExtraFields fields = null)
        {
            try
            {
                var result = _matrixManager.GetBuckets(filter, fields);
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
        /// The get buckets.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(BucketItem))]
        public IHttpActionResult GetBucket(string id)
        {
            try
            {
                var result = _matrixManager.GetBucket(id);
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
