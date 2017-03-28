using BND.Services.Payments.iDeal.Dal.Pocos;
using System;
using System.Data.Entity;

namespace BND.Services.Payments.iDeal.Dal
{
    /// <summary>
    /// Class SettingRepository performing setting table in database.
    /// </summary>
    public class SettingRepository : ISettingRepository
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public SettingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the value by key.
        /// </summary>
        /// <param name="key">The key for retrieve data.</param>
        /// <returns>System.String.</returns>
        public string GetValueByKey(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            IRepository<p_Setting> repository = _unitOfWork.GetRepository<p_Setting>();
            p_Setting setting = repository.GetById(key);
            if (setting == null)
            {
                throw new ArgumentException("Key not found.", "key");
            }

            return repository.GetById(key).Value;
        }
    }
}
