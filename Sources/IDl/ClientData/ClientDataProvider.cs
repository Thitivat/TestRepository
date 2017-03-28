using BND.Services.Payments.iDeal.ClientData.Dal.Interfaces;
using BND.Services.Payments.iDeal.Interfaces;
using System;

namespace BND.Services.Payments.iDeal.ClientData
{
    /// <summary>
    /// Class ClientDataProvider.
    /// </summary>
    public class ClientDataProvider : IClientDataProvider
    {
        #region [Fields]

        /// <summary>
        /// The _product bank account repository
        /// </summary>
        private IClientUserRepository _productBankAccountRepository;

        #endregion

        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDataProvider"/> class.
        /// </summary>
        /// <param name="productBankAccountRepository">The product bank account repository.</param>
        public ClientDataProvider(IClientUserRepository productBankAccountRepository)
        {
            _productBankAccountRepository = productBankAccountRepository;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Gets the client's name by IBAN.
        /// </summary>
        /// <param name="iban">The IBAN.</param>
        /// <returns>Client's name</returns>
        public string GetClientNameByIban(string iban)
        {
            if (String.IsNullOrEmpty(iban))
            {
                throw new ArgumentNullException("iban");
            }

            return _productBankAccountRepository.GetClientName(iban);
        }
        #endregion
    }
}
