using BND.Services.Payments.iDeal.ClientData.Dal.Interfaces;
using BND.Services.Payments.iDeal.ClientData.Dal.Pocos;
using System;
using System.Linq;

namespace BND.Services.Payments.iDeal.ClientData.Dal.Repositories
{
    public class ClientUserRepository : IClientUserRepository
    {
        #region [Fields]
        private IUnitOfWork _unitOfWork;
        #endregion

        #region [Constructor]
        public ClientUserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region [Methods]
        public string GetClientName(string iban)
        {
            IRepository<p_ClientUser> repository = _unitOfWork.GetRepository<p_ClientUser>();
            
            p_ClientUser clientUser = repository
                .GetQueryable(x =>  x.ProductClients.Any(a => a.Product.ProductBankAccounts.Any(b => b.MatrixIban == iban)))
                .FirstOrDefault();

            return clientUser != null ?
                String.Format("{0} {1} {2}", clientUser.FirstName, clientUser.LastNamePrefix, clientUser.LastName) :
                String.Empty;
        }
        #endregion
    }
}