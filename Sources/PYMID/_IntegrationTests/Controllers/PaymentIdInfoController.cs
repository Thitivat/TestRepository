using BND.Services.Infrastructure.ErrorHandling;
using BND.Services.Payments.PaymentIdInfo.Entities.Helpers;
using BND.Services.Payments.PaymentIdInfo.IntegrationTests.Models;
using BND.Services.Payments.PaymentIdInfo.Proxy.NET4.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BND.Services.Payments.PaymentIdInfo.IntegrationTests.Controllers
{
    public class PaymentIdInfoController : ApiController
    {
        private IPaymentIdInfoProxy _paymentIdInfo;

        public PaymentIdInfoController(IPaymentIdInfoProxy paymentIdInfo)
        {
            _paymentIdInfo = paymentIdInfo;
        }

        // GET api/PaymentIdInfo/GetEnumFilterType
        public HttpResponseMessage GetEnumFilterType()
        {
            try
            {
                var result = _paymentIdInfo.GetFilterTypes().Select(x => new FilterTypeModel { name = x.ToString() });
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (ProxyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Error.Message);
            }
        }

        // GET api/PaymentIdInfo/GetByBndIban/{id}?filterTypes={filterTypes}
        public HttpResponseMessage GetByBndIban(string id, string filterTypes = "")
        {
            try
            {
                var filterList = EnumFilterTypeHelper.ConvertToEnumFilterTypes(filterTypes);
                var result = _paymentIdInfo.GetByBndIban(id, filterList);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (ProxyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Error.Message);
            }
        }

        // GET api/PaymentIdInfo/GetBySourceIban/{id}?filterTypes={filterTypes}
        public HttpResponseMessage GetBySourceIban(string id, string filterTypes = "")
        {
            try
            {
                var filterList = EnumFilterTypeHelper.ConvertToEnumFilterTypes(filterTypes);
                var result = _paymentIdInfo.GetBySourceIban(id, filterList);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (ProxyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Error.Message);
            }
        }

        // GET api/PaymentIdInfo/GetByTransactionId/{id}?filterTypes={filterTypes}
        public HttpResponseMessage GetByTransactionId(string id, string filterTypes = "")
        {
            try
            {
                var filterList = EnumFilterTypeHelper.ConvertToEnumFilterTypes(filterTypes);
                var result = _paymentIdInfo.GetByTransactionId(id, filterList);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (ProxyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Error.Message);
            }
        }

    }
}