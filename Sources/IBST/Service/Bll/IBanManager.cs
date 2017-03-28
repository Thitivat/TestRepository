using BND.Services.IbanStore.Repository.Interfaces;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using BND.Services.IbanStore.Service.Dal;
using BND.Services.IbanStore.Service.Dal.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using Model = BND.Services.IbanStore.Models;

namespace BND.Services.IbanStore.Service.Bll
{
    public class IbanManager : IIbanManager
    {
        #region [Fields]

        /// <summary>
        /// The forma of date time to show in log.
        /// </summary>
        private const string FORMAT_DATE_TIME = "dd/MM/yyyy HH:mm";

        /// <summary>
        /// The length of iban number.
        /// </summary>
        private const int IBAN_LENGTH = 18;

        /// <summary>
        /// The _unit of work
        /// </summary>
        private IEfUnitOfWork _unitOfWork = null;

        /// <summary>
        /// The disposed flag.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// The _token
        /// </summary>
        private readonly string _username;

        /// <summary>
        /// The _context
        /// </summary>
        private readonly string _context;

        private readonly string _connectionstring;

        /// <summary>
        /// The time period to check expiry time for reserved iban (in second).
        /// </summary>
        private readonly int _reservedExpiry;

        static readonly object _object = new object();
        #endregion


        #region [Constructor]

        public IbanManager(string username, string context, string connectionString)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(username);
            }

            _username = username;
            _context = context;
            lock(_object){
            _unitOfWork = new IbanStoreUnitOfWork(connectionString);
            }
            _connectionstring = connectionString;
            // Read reserved time period from configuration.
            _reservedExpiry = Convert.ToInt32(ConfigurationManager.AppSettings["ReservedExpiry"]);
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// Gets all ibans.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="total">The total.</param>
        /// <param name="status">The status.</param>
        /// <param name="iban">The iban.</param>
        /// <returns>IEnumerable&lt;IBan&gt;.</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="IbanOperationException"></exception>
        public IEnumerable<Model.Iban> Get(int? offset, int? limit, out int total, string status = null, string iban = null)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            var query = _unitOfWork.GetRepository<p_Iban>().GetQueryable();
            // Generate status filter.
            if (!String.IsNullOrEmpty(status))
            {
                // Check parameter status already exist in enum status class
                if (!Enum.IsDefined(typeof(Model.EnumIbanStatus), status))
                {
                    throw new ArgumentException(string.Format(MessageLibs.MSG_INVALID.Message, "IBAN Status"), "status");
                }

                // Get enum status by parameter status
                p_EnumIbanStatus statusName = (p_EnumIbanStatus)Enum.Parse(typeof(p_EnumIbanStatus), status);
                query =
                    query.Where(
                        f =>
                            f.IbanHistory.Any(
                                a => a.IbanStatusId == statusName && a.HistoryId == f.CurrentStatusHistoryId));
            }

            // Generate iban filter.
            if (!String.IsNullOrEmpty(iban))
            {
                if (iban.Length > IBAN_LENGTH)
                {
                    throw new ArgumentException("iban");
                }
                // Filter by concat all fild of IBAN and then check is contains.
                query = query.Where(f => String.Concat(f.CountryCode, f.CheckSum, f.BankCode, "0", f.Bban).Contains(iban));
            }

            // Get record count follow the same condition.
            total = query.Count();

            if (offset.HasValue && limit.HasValue)
            {
                //query with paging
                query = query.OrderBy(s => s.IbanId).Skip(offset.Value).Take(limit.Value);

            }

            //var model = AutoMapper.Mapper.Map<IEnumerable<p_Iban>, IEnumerable<Model.Iban>>(query.ToList());
            // Return Iban and Iban history
            return IbanHelper.IBanHistories(_unitOfWork, query.Where(w => w.IbanHistory.Any(a => a.HistoryId == w.CurrentStatusHistoryId)));
        }

        /// <summary>
        /// Reserves the next available IBAN, client wil send a Unuqueid plus Unuque Id prefix.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <param name="uidPrefix">The context.</param> 
        /// <returns>Iban.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// uid
        /// or
        /// uidPrefix
        /// </exception>
        /// <exception cref="NextAvailableIbanAlreadyReservedException"></exception>
        /// <exception cref="IbanOperationException"></exception>
        public Model.Iban Reserve(string uid, string uidPrefix)
        {


            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentNullException("uid");
            }

            if (string.IsNullOrEmpty(uidPrefix))
            {
                throw new ArgumentNullException("uidPrefix");
            }


            //set old iban to null
              var ibanNotReserv = _unitOfWork.GetRepository<p_Iban>().GetQueryable(g => ((DbFunctions.DiffSeconds(g.ReservedTime, DateTime.Now) > _reservedExpiry) && g.Uid == uid && g.UidPrefix == uidPrefix&&g.IbanHistory.Any(x=>x.HistoryId==g.CurrentStatusHistoryId&&x.IbanStatusId== p_EnumIbanStatus.Available))).ToList();
              foreach (var piban in ibanNotReserv)
              {
                  piban.Uid = null;
                  piban.UidPrefix = null;
                  piban.ReservedTime = null;
                  _unitOfWork.GetRepository<p_Iban>().Update(piban);
                  p_IbanHistory his =
                      _unitOfWork.GetRepository<p_IbanHistory>()
                          .GetQueryable(s => s.HistoryId == piban.CurrentStatusHistoryId).FirstOrDefault();
                  his.IbanStatusId= p_EnumIbanStatus.Available;
                  his.Remark = null;
                  _unitOfWork.GetRepository<p_IbanHistory>().Update(his);

                  _unitOfWork.CommitChanges();
              }

            // Check already reserved 
            p_Iban iban = _unitOfWork.GetRepository<p_Iban>().GetQueryable(
                                                g => g.IbanHistory.Any(
                                                        a => a.IbanStatusId == p_EnumIbanStatus.Available && a.HistoryId == g.CurrentStatusHistoryId) &&
                                                    g.Uid == uid &&
                                                    g.UidPrefix == uidPrefix &&
                                                    DbFunctions.DiffSeconds(g.ReservedTime, DateTime.Now) < _reservedExpiry
                                                ).OrderBy(s => s.IbanId).FirstOrDefault();

            // Throw exception for not modify status.
            if (iban != null)
            {
                // throw exception with i band in case of data is already reserved.
                return (AutoMapper.Mapper.Map<Model.Iban>(iban));
            }

            // Check case the unique id try to reserve the one that already assign.
            iban = _unitOfWork.GetRepository<p_Iban>().GetQueryable(
                                                g => g.IbanHistory.Any(
                                                        a => a.IbanStatusId != p_EnumIbanStatus.Available && a.HistoryId == g.CurrentStatusHistoryId) &&
                                                    g.Uid == uid &&
                                                    g.UidPrefix == uidPrefix
                                                ).FirstOrDefault();
            if (iban != null)
            {
                // throw exception with i band in case of data is already assigned.
                throw new IbanOperationException(MessageLibs.MSG_IBAN_ALREADY_ASSIGNED.Code,
                                                 MessageLibs.MSG_IBAN_ALREADY_ASSIGNED.Message);
            }

            try
            {
                using (var scope = new TransactionScope())
                {
                    //get next available
                    iban = _unitOfWork.GetRepository<p_Iban>().GetQueryable(
                                                    g => g.IbanHistory.Any(
                                                            a => a.IbanStatusId == p_EnumIbanStatus.Available && a.HistoryId == g.CurrentStatusHistoryId) &&
                                                        (DbFunctions.DiffSeconds(g.ReservedTime, DateTime.Now) > _reservedExpiry || !g.ReservedTime.HasValue)
                                                    ).OrderBy(s => s.IbanId).FirstOrDefault();

                    if (iban == null)
                    {
                        // No IBAN for reserve.
                        throw new IbanOperationException(MessageLibs.MSG_NOT_AVAILABLE.Code,
                          string.Format(MessageLibs.MSG_NOT_AVAILABLE.Message, "IBAN"));
                    }
                    // Sets  reserve IBAN.
                    iban.Uid = uid;
                    iban.UidPrefix = uidPrefix;
                    iban.ReservedTime = DateTime.Now;
                    _unitOfWork.GetRepository<p_Iban>().Update(iban);
                    _unitOfWork.CommitChanges();

                    // adds history for reserve
                    string remark = String.Format(MessageLibs.MSG_RESERVED_HISTORY.Message,uidPrefix +"."+uid, DateTime.Now.ToString(FORMAT_DATE_TIME));
                    IbanHelper.SetIBanStatus(_unitOfWork, iban.IbanId, p_EnumIbanStatus.Available, remark, _username,_context);

                    scope.Complete();
                }
                // return iban that has set reserve.
                return AutoMapper.Mapper.Map<Model.Iban>(iban);
            }
            catch (Exception e)
            {
                // IBAN for reserve exception.
                throw new IbanOperationException(MessageLibs.MSG_NOT_AVAILABLE.Code,
                  string.Format(MessageLibs.MSG_NOT_AVAILABLE.Message, "IBAN"));
            }

        }

        /// <summary>
        /// Assigns the specified uid.
        /// </summary>
        /// <param name="ibanId">The iban identifier.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="uidPrefix">The context.</param>
        /// <returns>Iban.</returns>
        public Model.Iban Assign(int ibanId, string uid, string uidPrefix)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (ibanId <= 0)
            {
                throw new ArgumentException("ibanId could not be found.");
            }

            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentNullException("uid could not be found.");
            }

            if (string.IsNullOrEmpty(uidPrefix))
            {
                throw new ArgumentNullException("uidPrefix could not be found.");
            }

            // Get already assigned.
            p_Iban ibanPoco =
                _unitOfWork.GetRepository<p_Iban>()
                    .GetQueryable(
                        g =>
                            g.IbanId == ibanId && g.Uid == uid && g.UidPrefix == uidPrefix &&
                            g.IbanHistory.Any(a => a.IbanStatusId == p_EnumIbanStatus.Assigned && a.HistoryId == g.CurrentStatusHistoryId))
                    .FirstOrDefault();

            // Chceks already asssigned.
            if (ibanPoco != null)
            {
                // throw exception with i band in case of data is already assigned.
                throw new IbanAlreadyAssignedException(AutoMapper.Mapper.Map<Model.Iban>(ibanPoco));
            }

            // Get available IBAN to assign.
            ibanPoco =
                _unitOfWork.GetRepository<p_Iban>()
                    .GetQueryable(
                        g =>
                            g.IbanId == ibanId && g.Uid == uid && g.UidPrefix == uidPrefix &&
                            g.IbanHistory.Any(a => a.IbanStatusId == p_EnumIbanStatus.Available && a.HistoryId == g.CurrentStatusHistoryId))
                    .FirstOrDefault();
            if (ibanPoco == null)
            {
                throw new IbanOperationException(MessageLibs.MSG_COULD_NOT_BE_FOUND.Code,
                    string.Format(MessageLibs.MSG_COULD_NOT_BE_FOUND.Message, "IBan"));
            }

            if (!ibanPoco.ReservedTime.HasValue)
            {
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_BE_NULL.Code,
                    string.Format(MessageLibs.MSG_CANNOT_BE_NULL.Message, "Reserved Time"));
            }

            if ((DateTime.Now.Subtract(ibanPoco.ReservedTime.Value)).Minutes > _reservedExpiry)
            {
                throw new IbanOperationException(MessageLibs.MSG_EXPIRE_DATE.Code,
                    string.Format(MessageLibs.MSG_EXPIRE_DATE.Message, "Reserved Time"));
            }
            string remark = String.Format(MessageLibs.MSG_ASSIGNED_IBAN.Message, uidPrefix + "." + uid, DateTime.Now.ToString(FORMAT_DATE_TIME));
            // Update status iban with "Assigned"
            IbanHelper.SetIBanStatus(_unitOfWork, ibanId, p_EnumIbanStatus.Assigned, remark, _username, _context);

            // Return iban object
            return new Model.Iban(ibanPoco.CountryCode, ibanPoco.BankCode, ibanPoco.Bban, ibanPoco.CheckSum)
            {
                Uid = ibanPoco.Uid,
                UidPrefix = ibanPoco.UidPrefix,
                ReservedTime = ibanPoco.ReservedTime
            };
        }

        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <param name="uidPrefix">The uid prefix.</param>
        /// <returns>Iban.</returns>
        /// <exception cref="IbanOperationException">
        /// </exception>
        public Model.Iban Get(string uid, string uidPrefix)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentNullException("uid");
            }

            if (string.IsNullOrEmpty(uidPrefix))
            {
                throw new ArgumentNullException("uidPrefix");
            }

            IQueryable<p_Iban> ibanPoco =
                _unitOfWork.GetRepository<p_Iban>()
                    .GetQueryable(g => g.Uid == uid
                                       && g.UidPrefix == uidPrefix
                                       && g.IbanHistory.Any(
                                           a =>
                                               a.IbanStatusId == p_EnumIbanStatus.Assigned &&
                                               a.HistoryId == g.CurrentStatusHistoryId));
            if (ibanPoco.Count() == 0)
            {
                throw new IbanOperationException(MessageLibs.MSG_COULD_NOT_BE_FOUND.Code,
                    string.Format(MessageLibs.MSG_COULD_NOT_BE_FOUND.Message, "IBan", "have this uid and context."));
            }

            return IbanHelper.IBanHistories(_unitOfWork, ibanPoco).First();
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Iban.</returns>
        public Model.Iban Get(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("id");
            }

            IQueryable<p_Iban> ibanPoco =
                _unitOfWork.GetRepository<p_Iban>()
                    .GetQueryable(
                        g =>
                            g.IbanId == id &&
                            g.IbanHistory.Any(
                                a =>
                                    a.IbanStatusId == p_EnumIbanStatus.Assigned &&
                                    a.HistoryId == g.CurrentStatusHistoryId));
            if (ibanPoco.Count() == 0)
            {
                throw new IbanOperationException(MessageLibs.MSG_COULD_NOT_BE_FOUND.Code,
                    string.Format(MessageLibs.MSG_COULD_NOT_BE_FOUND.Message, "IBan"));
            }

            return IbanHelper.IBanHistories(_unitOfWork, ibanPoco).FirstOrDefault();
        }

        /// <summary>
        /// Gets the specified iban.
        /// </summary>
        /// <param name="iban">The iban.</param>
        /// <returns>Iban.</returns>
        public Model.Iban Get(string iban)
        {
            if (string.IsNullOrEmpty(iban))
            {
                throw new ArgumentNullException("iban");
            }

            // create and validate iban model by send iban code to constructure. If iban code is invalid will throw ArgumentException.
            Model.Iban ibanModel = new Model.Iban(iban);
            IQueryable<p_Iban> ibanPoco =
                _unitOfWork.GetRepository<p_Iban>()
                    .GetQueryable(
                        g =>
                            g.CountryCode == ibanModel.CountryCode && g.CheckSum == ibanModel.CheckSum &&
                            g.BankCode == ibanModel.BankCode && g.Bban == ibanModel.Bban &&
                            g.IbanHistory.Any(
                                a =>
                                    a.IbanStatusId == p_EnumIbanStatus.Assigned &&
                                    a.HistoryId == g.CurrentStatusHistoryId));

            return IbanHelper.IBanHistories(_unitOfWork, ibanPoco).FirstOrDefault();
        } 

        public IEnumerable<Model.IbanHistory> GetHistory(int id)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            List<p_IbanHistory> ibanHistories =
                _unitOfWork.GetRepository<p_IbanHistory>().GetQueryable(g => g.IbanId == id).ToList();

            return ibanHistories.Select(item => new Model.IbanHistory()
            {
                Id = item.HistoryId,
                Status = Enum.GetName(typeof(Model.EnumIbanStatus), item.IbanStatusId),
                Remark = item.Remark,
                ChangedBy = item.ChangedBy,
                ChangedDate = item.ChangedDate,
                Context = item.Context
            }).ToList();
        }

        #endregion

        #region [Dispose]
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // Clears garbage collector.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Releases all resources.
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                    _unitOfWork = null;
                }

                // Sets dispose flag.
                _disposed = true;
            }
        }

        #endregion
    }

}
