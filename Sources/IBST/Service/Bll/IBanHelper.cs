using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BND.Services.IbanStore.Repository.Interfaces;
using BND.Services.IbanStore.Service.Dal.Pocos;
using Model = BND.Services.IbanStore.Models;

namespace BND.Services.IbanStore.Service.Bll
{
    public static class IbanHelper
    {
        #region [Fields]
        /// <summary>
        /// The _unit of work
        /// </summary>
        private static IUnitOfWork _unitOfWork = null;
        #endregion

        /// <summary>
        /// Sets the i ban status.
        /// </summary>
        /// <param name="unitOfWork">The i unit of work.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        /// <param name="remark">The remark.</param>
        /// <param name="username">The username.</param>
        public static void SetIBanStatus(IUnitOfWork unitOfWork, int id, p_EnumIbanStatus status, string remark, string username,string context)
        {
            // Assign value to valiable
            _unitOfWork = unitOfWork;

            // Get currentStatusHistoryId from Iban table
            p_Iban ibanPoco = _unitOfWork.GetRepository<p_Iban>().GetById(id);
            int? currentHistoryId = ibanPoco.CurrentStatusHistoryId;

            // If currentStatusHistoryId is not null, take id to get Iban Status from IbanHistory table
            p_EnumIbanStatus? currentHistoryEnum = null;
            if (currentHistoryId != null)
            {
                currentHistoryEnum = _unitOfWork.GetRepository<p_IbanHistory>().GetById(currentHistoryId.Value).IbanStatusId;
            }

            // Check valid status that need to change
            if (CheckIBanStatus(currentHistoryEnum, status))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    // Create IbanHistory poco to insert into IbanHistory table
                    p_IbanHistory newIbanHistory = new p_IbanHistory
                    {
                        Context = context,
                        IbanId = id,
                        IbanStatusId = status,
                        Remark = remark,
                        ChangedDate = DateTime.Now,
                        ChangedBy = username
                    };

                    // Insert iban history into IbanHistory table
                    _unitOfWork.GetRepository<p_IbanHistory>().Insert(newIbanHistory);
                    _unitOfWork.CommitChanges();

                    // Update Iban table with HistoryId
                    ibanPoco.CurrentStatusHistoryId = newIbanHistory.HistoryId;
                    _unitOfWork.GetRepository<p_Iban>().Update(ibanPoco);
                    _unitOfWork.CommitChanges();

                    scope.Complete();
                }
            }
        }

        /// <summary>
        /// Checks the i ban status.
        /// </summary>
        /// <param name="currentStatus">The current status.</param>
        /// <param name="newStatus">The new status.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckIBanStatus(p_EnumIbanStatus? currentStatus, p_EnumIbanStatus newStatus)
        {
            // Case IBan Available
            if (newStatus == p_EnumIbanStatus.Available && (currentStatus == null || currentStatus == p_EnumIbanStatus.Available))
            {
                return true;
            }

            // Case IBan Assign
            if (newStatus == p_EnumIbanStatus.Assigned && currentStatus == p_EnumIbanStatus.Available)
            {
                return true;
            }

            // Case IBan Cancel
            if (newStatus == p_EnumIbanStatus.Canceled && currentStatus == p_EnumIbanStatus.Assigned)
            {
                return true;
            }

            // Case IBan Active
            if (newStatus == p_EnumIbanStatus.Active && currentStatus == p_EnumIbanStatus.Assigned)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// is the ban histories.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="ibanPoco">The iban poco.</param>
        /// <returns>IEnumerable&lt;Models.IBan&gt;.</returns>
        public static IEnumerable<Models.Iban> IBanHistories(IUnitOfWork unitOfWork, IQueryable<p_Iban> ibanPoco)
        {
            // Assign value to valiable
            _unitOfWork = unitOfWork;
            
            // Retrun list of Iban
            return (from item in ibanPoco
                    join ibanhis in _unitOfWork.GetRepository<p_IbanHistory>().GetQueryable() on item.CurrentStatusHistoryId equals ibanhis.HistoryId
                    select new {item,ibanhis}).ToList().Select(s=>
                     new Model.Iban(
                        s.item.CountryCode,
                        s.item.BankCode,
                        s.item.Bban,
                        s.item.CheckSum)
                    {
                        IbanId = s.item.IbanId,
                        Uid = s.item.Uid,
                        UidPrefix = s.item.UidPrefix,
                        ReservedTime = s.item.ReservedTime,
                        BbanFileName = s.item.BbanFile.Name,
                        CurrentIbanHistory = new Model.IbanHistory
                        {
                            Id = s.ibanhis.HistoryId,
                            Status = Enum.GetName(typeof(Model.EnumIbanStatus), s.ibanhis.IbanStatusId),
                            Remark = s.ibanhis.Remark,
                            Context = s.ibanhis.Context,
                            ChangedBy = s.ibanhis.ChangedBy,
                            ChangedDate = s.ibanhis.ChangedDate
                        }
                    }).ToList();
        }
    }
}
