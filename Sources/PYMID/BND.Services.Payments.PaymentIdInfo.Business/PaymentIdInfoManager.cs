using AutoMapper;
using BND.Services.Payments.PaymentIdInfo.Business.Helper;
using BND.Services.Payments.PaymentIdInfo.Data.Interfaces;
using BND.Services.Payments.PaymentIdInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Payments.PaymentIdInfo.Business
{
    /// <summary>
    /// Class PaymentIdInfoManager.
    /// </summary>
    public class PaymentIdInfoManager : IPaymentIdInfoManager
    {
        #region Fields & Properties
        /// <summary>
        /// The _mapper
        /// </summary>
        IMapper _mapper;
        /// <summary>
        /// The _ideal repository
        /// </summary>
        IiDealRepository _idealRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentIdInfoManager"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="idealRepository">The ideal repository.</param>
        public PaymentIdInfoManager(
            IMapper mapper,
            IiDealRepository idealRepository)
        {
            _mapper = mapper;
            _idealRepository = idealRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the filter types.
        /// </summary>
        /// <returns>IEnumerable{EnumFilterType}.</returns>
        public IEnumerable<EnumFilterType> GetFilterTypes()
        {
            return Enum.GetValues(typeof(EnumFilterType)).Cast<EnumFilterType>();
        }

        /// <summary>
        /// Gets the payment identifier by BND iban.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable{PaymentIdInfoModel}.</returns>
        /// <exception cref="System.ArgumentNullException">bndIban</exception>
        /// <exception cref="System.ArgumentException">bndIban is invalid.</exception>
        /// <exception cref="System.Exception"></exception>
        public IEnumerable<PaymentIdInfoModel> GetPaymentIdByBndIban(string bndIban, List<EnumFilterType> filterTypes)
        {
            if (String.IsNullOrEmpty(bndIban))
            {
                throw new ArgumentNullException("bndIban");
            }

            if (!IbanValidator.Validate(bndIban))
            {
                throw new ArgumentException("bndIban is invalid.");
            }

            IEnumerable<EnumFilterType> enumFilterTypes = filterTypes == null || filterTypes.Any(c => c == EnumFilterType.Unknown) ?
                GetFilterTypes().Where(c => c != EnumFilterType.Unknown).ToList() : filterTypes.AsEnumerable();

            IEnumerable<PaymentIdInfoModel> results = new List<PaymentIdInfoModel>();
            foreach (var filterType in enumFilterTypes)
            {
                IEnumerable<iDealTransaction> payments;

                switch (filterType)
                {
                    case EnumFilterType.iDeal:
                        payments = _idealRepository.GetByBndIban(bndIban).ToList();
                        results = results.Union(_mapper.Map<IList<PaymentIdInfoModel>>(payments));
                        break;
                    case EnumFilterType.eMandates:
                        break;
                    case EnumFilterType.CreditTransfer:
                        break;
                    default:
                        throw new Exception(String.Format("GetPaymentIdByBndIban have not supported for {0} yet.", filterType));
                }
            }

            return results;
        }

        /// <summary>
        /// Gets the payment identifier by source iban.
        /// </summary>
        /// <param name="sourceIban">The source iban.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable{PaymentIdInfoModel}.</returns>
        /// <exception cref="System.ArgumentNullException">sourceIban</exception>
        /// <exception cref="System.ArgumentException">sourceIban is invalid.</exception>
        /// <exception cref="System.Exception"></exception>
        public IEnumerable<PaymentIdInfoModel> GetPaymentIdBySourceIban(string sourceIban, List<EnumFilterType> filterTypes)
        {
            if (String.IsNullOrEmpty(sourceIban))
            {
                throw new ArgumentNullException("sourceIban");
            }

            if (!IbanValidator.Validate(sourceIban))
            {
                throw new ArgumentException("sourceIban is invalid.");
            }

            IEnumerable<EnumFilterType> enumFilterTypes = filterTypes == null || filterTypes.Any(c => c == EnumFilterType.Unknown) ?
                GetFilterTypes().Where(c => c != EnumFilterType.Unknown).ToList() : filterTypes.AsEnumerable();

            IEnumerable<PaymentIdInfoModel> results = new List<PaymentIdInfoModel>();
            foreach (var filterType in enumFilterTypes)
            {
                IEnumerable<iDealTransaction> payments;

                switch (filterType)
                {
                    case EnumFilterType.iDeal:
                        payments = _idealRepository.GetBySourceIban(sourceIban);
                        results = results.Union(_mapper.Map<IEnumerable<PaymentIdInfoModel>>(payments));
                        break;
                    case EnumFilterType.eMandates:
                        break;
                    case EnumFilterType.CreditTransfer:
                        break;
                    default:
                        throw new Exception(String.Format("GetPaymentIdBySourceIban have not supported for {0} yet.", filterType));
                }
            }

            return results;
        }

        /// <summary>
        /// Gets the payment identifier by transaction.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns>IEnumerable{PaymentIdInfoModel}.</returns>
        /// <exception cref="System.ArgumentNullException">transactionId</exception>
        /// <exception cref="System.Exception"></exception>
        public IEnumerable<PaymentIdInfoModel> GetPaymentIdByTransaction(string transactionId, List<EnumFilterType> filterTypes)
        {
            if (String.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentNullException("transactionId");
            }

            IEnumerable<EnumFilterType> enumFilterTypes = filterTypes == null || filterTypes.Any(c => c == EnumFilterType.Unknown) ?
                GetFilterTypes().Where(c => c != EnumFilterType.Unknown).ToList() : filterTypes.AsEnumerable();

            IEnumerable<PaymentIdInfoModel> results = new List<PaymentIdInfoModel>();
            foreach (var filterType in enumFilterTypes)
            {
                IEnumerable<iDealTransaction> payments;

                switch (filterType)
                {
                    case EnumFilterType.iDeal:
                        payments = _idealRepository.GetByTransactionId(transactionId);
                        results = results.Union(_mapper.Map<IEnumerable<PaymentIdInfoModel>>(payments));
                        break;
                    case EnumFilterType.eMandates:
                        break;
                    case EnumFilterType.CreditTransfer:
                        break;
                    default:
                        throw new Exception(String.Format("GetPaymentIdByTransaction have not supported for {0} yet.", filterType));
                }
            }

            return results;
        }
        #endregion
    }
}
