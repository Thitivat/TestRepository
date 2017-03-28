using AutoMapper;
using BND.Services.Payments.PaymentIdInfo.Data.Interfaces;
using BND.Services.Payments.PaymentIdInfo.Entities;
using BND.Services.Payments.PaymentIdInfo.Models;
using System.Collections.Generic;
using System.Linq;

namespace BND.Services.Payments.PaymentIdInfo.Data
{
    /// <summary>
    /// Class iDealRepository.
    /// </summary>
    public class iDealRepository : IiDealRepository
    {
        #region [Fields]
        /// <summary>
        /// The _mapper
        /// </summary>
        IMapper _mapper;
        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork;
        #endregion


        #region [Constructor]
        public iDealRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion


        #region [Methods]
        /// <summary>
        /// Gets iDealTransaction data the by BND iban.
        /// </summary>
        /// <param name="bndIban">The BND iban.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<iDealTransaction> GetByBndIban(string bndIban)
        {
            IRepository<p_iDealTransaction> repository = _unitOfWork.GetRepository<p_iDealTransaction>();
            return _mapper.Map<IEnumerable<iDealTransaction>>(repository.Get().Where(x => x.BNDIBAN == bndIban).AsEnumerable());
        }

        /// <summary>
        /// Gets iDealTransaction data by source iban.
        /// </summary>
        /// <param name="sourceIban">The source iban.</param>
        /// <returns>IEnumerable&lt;Libs.iDealTransaction&gt;.</returns>
        public IEnumerable<iDealTransaction> GetBySourceIban(string sourceIban)
        {
            IRepository<p_iDealTransaction> repository = _unitOfWork.GetRepository<p_iDealTransaction>();
            return _mapper.Map<IEnumerable<iDealTransaction>>(repository.Get().Where(x => x.ConsumerIBAN == sourceIban).AsEnumerable());
        }

        /// <summary>
        /// Gets iDealTransaction data by transaction identifier.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>IEnumerable&lt;Libs.iDealTransaction&gt;.</returns>
        public IEnumerable<iDealTransaction> GetByTransactionId(string transactionId)
        {
            IRepository<p_iDealTransaction> repository = _unitOfWork.GetRepository<p_iDealTransaction>();
            return _mapper.Map<IEnumerable<iDealTransaction>>(repository.Get().Where(x => x.TransactionID == transactionId).AsEnumerable());
        }
        #endregion
    }
}
