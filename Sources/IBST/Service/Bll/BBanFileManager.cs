using BND.Services.IbanStore.Repository.Interfaces;
using BND.Services.IbanStore.Repository.Models;
using BND.Services.IbanStore.Service.Bll.Interfaces;
using BND.Services.IbanStore.Service.Dal;
using BND.Services.IbanStore.Service.Dal.Pocos;
using Chilkat;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using Model = BND.Services.IbanStore.Models;

namespace BND.Services.IbanStore.Service.Bll
{
    /// <summary>
    /// Class BBanFile.
    /// </summary>
    public class BbanFileManager : IBbanFileManager
    {
        #region [Fields]
        /// <summary>
        /// The _unit of work
        /// </summary>
        private IEfUnitOfWork _unitOfWork = null;

        /// <summary>
        /// The _mail setting
        /// </summary>
        private EmailSetting _mailSetting;

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
        #endregion


        #region [Constructor]
        /// <summary>
        /// Initializes a new instance of the <see cref="BbanFileManager" /> class.
        /// </summary>
        /// <param name="username">The token.</param>
        /// <param name="context">The context.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="emailConfiguration">The XML configuration.</param>
        public BbanFileManager(string username, string context, string connectionString, string emailConfiguration)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            _username = username;
            _context = context;
            _unitOfWork = new IbanStoreUnitOfWork(connectionString);
            ConfigureEmailSetting(emailConfiguration);
        }

        #endregion


        #region [Public Method]

        /// <summary>
        /// Gets the bban files response all BbanFiles
        /// </summary>
        /// <param name="offset">The offset start paging query</param>
        /// <param name="limit">The limit end paging query</param>
        /// <param name="total">The total.</param>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns>IEnumerable Model.BbanFile</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentException">BBAN file status is invalid.;status</exception>
        /// <exception cref="IbanOperationException"></exception>
        /// <exception cref="System.AggregateException"></exception>
        public IEnumerable<Model.BbanFile> Get(int offset, int limit, out int total, int? bbanFileId = null, string status = null)
        {
            total = 0;
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            try
            {
                // Create object page to get data collection
                Page<p_BbanFile> page = new Page<p_BbanFile> { Limit = limit, Offset = offset, OrderBy = o => o.OrderBy(b => b.BbanFileId) };

                // Check parameter is null
                Expression<Func<p_BbanFile, bool>> filter = null;
                p_EnumBbanFileStatus statusName;

                if (bbanFileId.HasValue && !string.IsNullOrEmpty(status))
                {
                    // Check parameter status already exist in enum status class
                    if (!Enum.IsDefined(typeof(Model.EnumBbanFileStatus), status))
                    {
                        throw new ArgumentException(string.Format(MessageLibs.MSG_INVALID.Message, "BBAN File Status"), "status");
                    }

                    // Get enum status by parameter status
                    statusName = (p_EnumBbanFileStatus)Enum.Parse(typeof(p_EnumBbanFileStatus), status);
                    filter = f => f.BbanFileId == bbanFileId && f.BbanFileHistory.Any(
                                a => a.BbanFileStatusId == statusName && a.HistoryId == f.CurrentStatusHistoryId);

                    total = _unitOfWork.GetRepository<p_BbanFile>().GetCount(g =>
                                    g.BbanFileId == bbanFileId &&
                                    g.BbanFileHistory.Any(
                                        a => a.BbanFileStatusId == statusName && a.HistoryId == g.CurrentStatusHistoryId));
                }
                else if (!bbanFileId.HasValue && !string.IsNullOrEmpty(status))
                {
                    // Check parameter status already exist in enum status class
                    if (!Enum.IsDefined(typeof(Model.EnumBbanFileStatus), status))
                    {
                        throw new ArgumentException(string.Format(MessageLibs.MSG_INVALID.Message, "BBAN File Status"), "status");
                    }

                    // Get enum status by parameter status
                    statusName = (p_EnumBbanFileStatus)Enum.Parse(typeof(p_EnumBbanFileStatus), status);
                    filter = f => f.BbanFileHistory.Any(
                                a => a.BbanFileStatusId == statusName && a.HistoryId == f.CurrentStatusHistoryId);

                    total = _unitOfWork.GetRepository<p_BbanFile>().GetCount(g =>
                                    g.BbanFileHistory.Any(
                                        a => a.BbanFileStatusId == statusName && a.HistoryId == g.CurrentStatusHistoryId));
                }
                else if (bbanFileId.HasValue && string.IsNullOrEmpty(status))
                {
                    filter = f => f.BbanFileId == bbanFileId;
                    total = _unitOfWork.GetRepository<p_BbanFile>().GetCount(g => g.BbanFileId == bbanFileId);
                }
                else
                {
                    total = _unitOfWork.GetRepository<p_BbanFile>().GetCount();
                }

                // query paging 
                IEnumerable<p_BbanFile> query = _unitOfWork.GetRepository<p_BbanFile>().GetQueryable(filter: filter, page: page);

                return ConvertToBbanFile(query);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Code,
                    string.Format(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Message, "BBAN or BBAN File", "get"), ex);
            }
        }

        /// <summary>
        /// Gets the bbans response Bban in file
        /// </summary>
        /// <param name="bbanFileId">bbanFileId in database</param>
        /// <returns>IEnumerable&lt;Model.Bban&gt;.</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentNullException">bbanFileId</exception>
        /// <exception cref="System.ArgumentException">Cannot be found BBAN.</exception>
        public IEnumerable<Model.Bban> GetBbans(int offset, int limit, out int total, int bbanFileId)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
            Page<p_BbanImport> page = new Page<p_BbanImport> { Limit = limit, Offset = offset, OrderBy = s => s.OrderBy(x => x.BbanFileId) };
            total = _unitOfWork.GetRepository<p_BbanImport>().GetQueryable(g => g.BbanFileId.Equals(bbanFileId)).Count();
            // Check BBanFileId is existing in the database
            List<p_BbanImport> bbanImport = _unitOfWork.GetRepository<p_BbanImport>().GetQueryable(g => g.BbanFileId.Equals(bbanFileId), page).ToList();

            // If exist then return value in type of enumerable object bban
            return bbanImport.Select(item => new Model.Bban
            {
                ImportId = item.BbanImportId,
                BbanCode = item.Bban,
                IsImported = item.IsImported
            }).ToList();
        }

        /// <summary>
        /// Gets the bban file status. response all statuses
        /// </summary>
        /// <returns>all statuses</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public IEnumerable<string> GetStatus()
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            // Return enum status in type of list string
            return Enum.GetNames(typeof(Model.EnumBbanFileStatus)).ToList();
        }

        /// <summary>
        /// Adds the bban file upload file Bban and save to database then set BbanFile status Uploaded into Table BbanFileHistory
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="bbanFile">file streaming</param>
        /// <returns>BbanfileId</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentNullException">fileName</exception>
        /// <exception cref="IbanOperationException">1001;This file is already uploaded to the system.
        /// or
        /// 1002;Cannot BBAN file or BBAN to database.</exception>
        public int Save(string fileName, byte[] bbanFile)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            // Checks required parameters.
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            // Checks BBAN file format.
            int[] bbans = Verify.CheckFileFormat(bbanFile).ToArray();

            // Checks BBAN file is exist in db via hash data.
            string hasedFile = Verify.CreateHash(bbanFile);
            if (_unitOfWork.GetRepository<p_BbanFile>().GetQueryable(g => g.Hash.Equals(hasedFile)).Any())
            {
                throw new IbanOperationException(MessageLibs.MSG_ALREADY_EXIST_SYSTEM.Code,
                    string.Format(MessageLibs.MSG_ALREADY_EXIST_SYSTEM.Message, "BBAN File"));
            }

            var dupBban = bbans.GroupBy(s => s).Where(s => s.Count() > 1).FirstOrDefault();
            if (dupBban != null)
            {
                throw new IbanOperationException(MessageLibs.MSG_BBAN_FILE_DUPLICATE.Code, String.Format(MessageLibs.MSG_BBAN_FILE_DUPLICATE.Message, dupBban.Key));
            }
            // Checks BBAN valid.
            foreach (int bban in bbans)
            {
                Verify.CheckBBan11Proof(bban);
            }

            try
            {
                int bbfileId;

                // Used TrasactionScope for confirm all transaction when upload BBAN file should save to db completely.
                using (var scope = new TransactionScope())
                {
                    #region BBAN file
                    // Adds BBAN file.
                    p_BbanFile pBbanFile = new p_BbanFile
                    {
                        Hash = hasedFile,
                        Name = fileName,
                        RawFile = bbanFile,
                    };

                    _unitOfWork.GetRepository<p_BbanFile>().Insert(pBbanFile);
                    _unitOfWork.CommitChanges();
                    #endregion

                    // Add new status history and update the current status.
                    SetBBanFileStatus(pBbanFile.BbanFileId, Model.EnumBbanFileStatus.Uploaded);

                    #region BBAN import
                    // Adds BBAN number.
                    List<p_BbanImport> bbansImport = bbans.Select(bban => new p_BbanImport
                    {
                        Bban = bban.ToString(),
                        BbanFileId = pBbanFile.BbanFileId,
                        IsImported = false
                    }).ToList();

                    // check duplicate in database
                    List<string> bbandup = bbansImport.Select(x => x.Bban).ToList();
                    List<string> duplicate =
                        _unitOfWork.GetRepository<p_BbanImport>()
                            .GetQueryable(s => bbandup.Contains(s.Bban))
                            .Select(x => x.Bban)
                            .ToList();

                    if (duplicate.Count > 0)
                    {
                        throw new IbanOperationException(MessageLibs.MSG_ALREADY_EXIST_SYSTEM.Code,
                        string.Format(MessageLibs.MSG_ALREADY_EXIST_SYSTEM.Message, "BBAN Number:" + string.Join(",", duplicate.ToArray())));
                    }
                    _unitOfWork.GetRepository<p_BbanImport>().Insert(bbansImport);
                    _unitOfWork.CommitChanges();

                    #endregion

                    // Set bbanfileid to variable
                    bbfileId = pBbanFile.BbanFileId;

                    scope.Complete();
                }

                // Return bbanfile id
                return bbfileId;
            }
            catch (DbUpdateException dbException)
            {
                // Handle dabase exception.
                throw DbExceptionHelper.ThrowException(dbException, "BBAN", "insert");
            }
            catch (IbanOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Code,
                    string.Format(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Message, "BBAN or BBAN File", "insert"), ex);
            }
        }

        /// <summary>
        /// Updates the bban file status. send new Status and  insert into Table BbanFileHistory
        /// </summary>
        /// <param name="bbanFileId">BbanfileId after uploaded</param>
        /// <param name="remark">The remark.</param>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentException">BBanFile Id is invalid.;bbanFileId
        /// or
        /// Ivalid BBAN status;status
        /// or
        /// BBAN File cannot be found data in the database.</exception>
        /// <exception cref="IbanOperationException">1000;Cannot import BBAN file to the database
        /// or
        /// 1000;Cannot BBAN file or BBAN to databse.</exception>
        public void Approve(int bbanFileId, string remark = null)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            // Checks required parameters.
            if (bbanFileId == 0)
            {
                throw new ArgumentException("BBAn File could be found.", "bbanFileId");
            }

            // Check token user should be same as uploader.
            if (!CheckApprover(bbanFileId))
            {
                throw new IbanOperationException(MessageLibs.MSG_UPLOADER_CANNOT_APPROVE.Code,
                    MessageLibs.MSG_UPLOADER_CANNOT_APPROVE.Message);
            }

            try
            {
                // Update status from to "Approved".
                SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.Approved, remark);

                // Update status to "Importing".
                SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.Importing);

                try
                {
                    // Get BBAN from BbanImport table by BbanFileId and Is import equal to false
                    List<p_BbanImport> bbanImportpocoList = _unitOfWork.GetRepository<p_BbanImport>()

                    .GetQueryable(g => g.BbanFileId == bbanFileId && !g.IsImported)
                    .ToList();
                    List<p_Iban> ibns =
                     bbanImportpocoList.Select(s => new Model.Iban("NL", "BNDA", s.Bban))
                     .ToList()
                     .Select(s => new p_Iban()
                     {
                         BbanFileId = bbanFileId,
                         CountryCode = s.CountryCode,
                         BankCode = s.BankCode,
                         CheckSum = string.Format("{0:D2}", Convert.ToInt32(s.CheckSum)),
                         Bban = s.Bban,


                     }).ToList();

                    if (bbanImportpocoList.Count == 0)
                    {
                        throw new InvalidOperationException("BBAN File cannot be found data in the database.");
                    }

                    using (var scope = new TransactionScope())
                    {
                        // insert bluk into database
                        _unitOfWork.GetRepository<p_Iban>().Insert(ibns);

                        //get ibns before insert
                        ibns =
                            _unitOfWork.GetRepository<p_Iban>().GetQueryable(s => s.BbanFileId == bbanFileId).ToList();


                        //create IbanHitory record
                        List<p_IbanHistory> newIbanHistory = ibns.Select(s => new p_IbanHistory
                        {
                            IbanId = s.IbanId,
                            IbanStatusId = p_EnumIbanStatus.Available,
                            Remark = remark,
                            ChangedDate = DateTime.Now,
                            Context = _context,
                            ChangedBy = _username
                        }).ToList();

                        _unitOfWork.GetRepository<p_IbanHistory>().Insert(newIbanHistory);


                        //update Status
                        StringBuilder sb = new StringBuilder();
                        sb.Append("UPDATE ib.IBan SET CurrentStatusHistoryId= i.HistoryId ");
                        sb.Append("FROM (SELECT HistoryId,IBanId FROM ib.IbanHistory) i ");
                        sb.Append(" WHERE i.IBanId = ib.IBan.IBanId AND ib.IBan.BbanFileId=@BbanFileId ");

                        var sqlUpdateIBanHistory = sb.ToString();

                        var param = new Dictionary<string, object>();
                        param.Add("BbanFileId", bbanFileId);
                        _unitOfWork.ExecuteNonQuery(sqlUpdateIBanHistory, param);
                        scope.Complete();
                    }

                    using (var scope = new TransactionScope())
                    {
                        // Delete all bban by bbanFileId that already imported
                        string deleteSQL = "DELETE ib.BbanImport WHERE BbanFileId=@BbanFileId";
                        var param = new Dictionary<string, object>();
                        param.Add("BbanFileId", bbanFileId);

                        _unitOfWork.ExecuteNonQuery(deleteSQL, param);

                        // Update status to "Imported"
                        SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.Imported);

                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        // Delete all bban by bbanFileId that already imported
                        string deleteSQL = "DELETE ib.BbanImport WHERE BbanFileId=@BbanFileId";
                        var param = new Dictionary<string, object>();
                        param.Add("BbanFileId", bbanFileId);

                        _unitOfWork.ExecuteNonQuery(deleteSQL, param);

                        // If any problem then update status to "ImportFailed"
                        SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.ImportFailed, ex.Message);
                        scope.Complete();
                    }
                    throw new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Code,
                            String.Format(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Message, "BBAN or BBAN File",
                                "Approve"), ex);
                }
            }
            catch (IbanOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Code,
                        String.Format(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Message, "BBAN or BBAN File", "Approve"), ex);
            }
        }

        /// <summary>
        /// Denies the bban file vai set the status to "ApprovalDenied".
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="remark">The remark.</param>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentNullException">remark</exception>
        /// <exception cref="System.ArgumentException">BBAn File could be found.;bbanFileId</exception>
        /// <exception cref="IbanOperationException">1004;BBAN file cannot be Denine.</exception>
        public void Deny(int bbanFileId, string remark)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            // Checks required parameters.
            if (String.IsNullOrEmpty(remark))
            {
                throw new ArgumentNullException("remark");
            }
            // Checks required parameters.
            if (bbanFileId == 0)
            {
                throw new ArgumentException("BBAn File could be found.", "bbanFileId");
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    // Update status in the database to waiting for approval
                    SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.ApprovalDenied, remark);

                    // Remove all bban in BbanImport by bban file id
                    List<p_BbanImport> bbanImport =
                    _unitOfWork.GetRepository<p_BbanImport>().GetQueryable(g => g.BbanFileId == bbanFileId).ToList();
                    if (bbanImport.Count > 0)
                    {
                        string deleteSQL = "DELETE ib.BbanImport WHERE BbanFileId=@BbanFileId";
                        var param = new Dictionary<string, object>();
                        param.Add("BbanFileId", bbanFileId);

                        _unitOfWork.ExecuteNonQuery(deleteSQL, param);
                    }

                    scope.Complete();
                }
            }
            catch (IbanOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Code,
                        String.Format(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Message, "BBAN or BBAN File", "Denial"), ex);
            }
        }

        /// <summary>
        /// Gets the bban file history.  query Bban History in database table BbanHistroy
        /// </summary>
        /// <param name="bbanFileId">Bban FileId for query in table</param>
        /// <returns>all status history</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentNullException">bbanFileId</exception>
        public IEnumerable<Model.BbanFileHistory> GetHistory(int bbanFileId)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            List<p_BbanFileHistory> bbanFileHistoryPoco =
                _unitOfWork.GetRepository<p_BbanFileHistory>().GetQueryable(g => g.BbanFileId == bbanFileId).ToList();

            return bbanFileHistoryPoco.Select(item => new Model.BbanFileHistory()
            {
                Id = item.HistoryId,
                Status = Enum.GetName(typeof(Model.EnumBbanFileStatus),
                item.BbanFileStatusId),
                Remark = item.Remark,
                ChangedBy = item.ChangedBy,
                ChangedDate = item.ChangedDate,
                Context = item.Context
            }).ToList();
        }

        /// <summary>
        /// Verifies the bban existing in the Iban table database.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentException">BBAn File could be found.;bbanFileId</exception>
        /// <exception cref="IbanOperationException">1000;Cannot verify bban file.</exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public void VerifyBbanExist(int bbanFileId)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            // Checks required parameters.
            if (bbanFileId == 0)
            {
                throw new ArgumentException("BBAn File could be found.", "bbanFileId");
            }

            try
            {
                // Set BBanFile status to "Verifying"
                SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.Verifying);

                // Get all Bban number.
                List<string> bbanList = _unitOfWork.GetRepository<p_BbanImport>()
                    .GetQueryable(g => g.BbanFileId == bbanFileId && g.IsImported == false)
                    .Select(s => s.Bban)
                    .ToList();

                List<string> duplicate =
                    _unitOfWork.GetRepository<p_Iban>()
                        .GetQueryable(s => bbanList.Contains(s.Bban))
                        .Select(x => x.Bban)
                        .ToList();

                // Verify data by check existing in IBan Table
                if (duplicate.Count > 0)
                {
                    using (var scope = new TransactionScope())
                    {
                        // Delete all bban by bbanFileId.
                        string deleteSQL = "DELETE ib.BbanImport WHERE BbanFileId=@BbanFileId";
                        var param = new Dictionary<string, object>();
                        param.Add("BbanFileId", bbanFileId);

                        _unitOfWork.ExecuteNonQuery(deleteSQL, param);

                        // Set BBanFile status to "VerifiedFailed"
                        SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.VerifiedFailed,
                            string.Format(MessageLibs.MSG_ALREADY_EXIST_DATABASE.Message, "BBAN Number:" + string.Join(",", duplicate.ToList())));

                        scope.Complete();
                    }

                    throw new IbanOperationException(MessageLibs.MSG_ALREADY_EXIST_DATABASE.Code,
                                      string.Format(MessageLibs.MSG_ALREADY_EXIST_DATABASE.Message, "BBAN Number:" + string.Join(",", duplicate.ToList())));
                }

                // Set BBanFile status to "Verified"
                SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.Verified);
            }
            catch (IbanOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                // Case cannot be found bban file history that have verified status then throw
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Code,
                        string.Format(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Message, "The system", "Process to verify BBAN file."));
            }
        }

        /// <summary>
        /// Sends to message to Approver by email to notify Bban are ready to approve.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="emailReceiver">The email receiver.</param>
        /// <param name="emailMessage">The email message.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentException">Cannot be found Bban file that ready to approve.
        /// or
        /// BBAN file on processing to verify, cannot sent email for this time.</exception>
        /// <exception cref="System.ArgumentNullException">bbanFileId</exception>
        /// <exception cref="IbanOperationException">1000;Cannot sent mail to approver</exception>
        public void SendToApprove(int bbanFileId, string emailReceiver, string emailMessage)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            // Checks required parameters.
            if (bbanFileId == 0)
            {
                throw new ArgumentException("BBAn File could be found.", "bbanFileId");
            }

            if (string.IsNullOrEmpty(emailReceiver))
            {
                throw new ArgumentNullException("emailReceiver");
            }

            if (string.IsNullOrEmpty(emailMessage))
            {
                throw new ArgumentNullException("emailMessage");
            }

            // Get latest bban file history by bban file id
            p_BbanFileHistory bbanFileHistory = _unitOfWork.GetRepository<p_BbanFileHistory>()
                .GetQueryable(g => g.BbanFileId == bbanFileId)
                .OrderByDescending(o => o.ChangedDate)
                .FirstOrDefault();

            // Check latest history should not null and equal to Verified status
            if (bbanFileHistory != null &&
                bbanFileHistory.BbanFileStatusId == (p_EnumBbanFileStatus)Model.EnumBbanFileStatus.Verified)
            {
                // Get bbanfile with bbanfileid and historyid that have verified status
                p_BbanFile bbanFile = _unitOfWork.GetRepository<p_BbanFile>()
                    .GetQueryable(
                        g => g.BbanFileId == bbanFileId && g.CurrentStatusHistoryId == bbanFileHistory.HistoryId)
                    .FirstOrDefault();

                // Check bbanfile is not null
                if (bbanFile == null)
                {
                    // Case cannot be found bban file that have verified status then throw
                    throw new IbanOperationException(MessageLibs.MSG_CANNOT_FOUND.Code,
                        string.Format(MessageLibs.MSG_CANNOT_FOUND.Message, "BBAN File", "ready to send for approve."));
                }

                // Set Email Subject
                string subject = "BBAN waiting for approval";

                // Sent email to notify BBAN approve
                SendEmail(emailReceiver, emailMessage, subject);

                // Update status in the database to waiting for approval
                SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.WaitingForApproval);
            }
            else
            {
                // Case cannot be found bban file history that have verified status then throw
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_FOUND.Code,
                        string.Format(MessageLibs.MSG_CANNOT_FOUND.Message, "BBAN File", "have status verified."));
            }
        }

        /// <summary>
        /// Cancels the bban file.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns>System.Boolean.</returns>
        public void Cancel(int bbanFileId)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            // Checks required parameters.
            if (bbanFileId == 0)
            {
                throw new ArgumentException("BBAn File could be found.", "bbanFileId");
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    // Remove all bban by bban file id
                    List<p_BbanImport> bbanImport =
                    _unitOfWork.GetRepository<p_BbanImport>().GetQueryable(g => g.BbanFileId == bbanFileId).ToList();
                    if (bbanImport.Count > 0)
                    {
                        // Delete all bban by bbanFileId that already imported
                        string deleteSQL = "DELETE ib.BbanImport WHERE BbanFileId=@BbanFileId";
                        var param = new Dictionary<string, object>();
                        param.Add("BbanFileId", bbanFileId);

                        _unitOfWork.ExecuteNonQuery(deleteSQL, param);

                    }

                    // Update status in the database to waiting for approval
                    SetBBanFileStatus(bbanFileId, Model.EnumBbanFileStatus.UploadCanceled);

                    scope.Complete();
                }
            }
            catch (IbanOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Any error then throw
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Code,
                    string.Format(MessageLibs.MSG_CANNOT_PROCESS_FUNCTION.Message, "The system",
                        "process function to cancel upload BBAN file."), ex);
            }
        }

        /// <summary>
        /// Gets the ibans by bban file identifier.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="total">The total.</param>
        /// <returns>IEnumerable&lt;Models.IBan&gt;.</returns>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="IbanOperationException"></exception>
        public IEnumerable<Models.Iban> GetIbans(int bbanFileId, int? offset, int? limit,
            out int total)
        {
            // Checks dispose.
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            try
            {
                // Create object page to get data collection
                Page<p_Iban> page = new Page<p_Iban> { Limit = limit, Offset = offset, OrderBy = s => s.OrderBy(o => o.IbanId) };

                // Check parameter is null
                Expression<Func<p_Iban, bool>> filter = f => f.BbanFileId == bbanFileId && f.IbanHistory.Any(a => a.HistoryId == f.CurrentStatusHistoryId);

                // get record count follow the same condition.
                total = _unitOfWork.GetRepository<p_Iban>().GetCount(g => g.IbanHistory.Any(a => a.HistoryId == g.CurrentStatusHistoryId && g.BbanFileId == bbanFileId));

                // Retrun list of Iban
                return IbanHelper.IBanHistories(_unitOfWork, _unitOfWork.GetRepository<p_Iban>().GetQueryable(filter: filter, page: page));
            }
            catch (Exception ex)
            {
                throw new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Code,
                   string.Format(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Message, "IBAN", "get"), ex);
            }
        }

        #endregion


        #region [Private Method]

        /// <summary>
        /// Sets the b ban file status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        /// <param name="remark">The remark.</param>
        private void SetBBanFileStatus(int id, Model.EnumBbanFileStatus status, string remark = null)
        {
            // Get history id from BbanFile Table
            p_BbanFile bbanFiledata = _unitOfWork.GetRepository<p_BbanFile>().GetById(id);
            if (bbanFiledata == null)
            {
                throw new IbanOperationException(MessageLibs.MSG_COULD_NOT_BE_FOUND.Code, string.Format(MessageLibs.MSG_COULD_NOT_BE_FOUND.Message, "The BBAN File"));
            }

            int? currentHistoryId = bbanFiledata.CurrentStatusHistoryId;

            // Get current status from BbanFileHistory
            p_EnumBbanFileStatus? currentHistoryStatus = null;
            if (currentHistoryId != null)
            {
                currentHistoryStatus = _unitOfWork.GetRepository<p_BbanFileHistory>().GetById(currentHistoryId.Value).BbanFileStatusId;
            }

            // Verify Status before insert to BbanFileHistory
            if (CheckBbanFileStatus(currentHistoryStatus, (p_EnumBbanFileStatus)status))
            {
                // Count BBAN and stamp on remark field
                if ((status == Model.EnumBbanFileStatus.Verified || status == Model.EnumBbanFileStatus.VerifiedFailed
                    || status == Model.EnumBbanFileStatus.Approved || status == Model.EnumBbanFileStatus.ApprovalDenied)
                    )
                {
                    // Count Bban File records
                    int recCnt = _unitOfWork.GetRepository<p_BbanImport>().GetQueryable(g => g.BbanFileId == id).Count();
                    remark = String.IsNullOrEmpty(remark)
                        ? String.Format(MessageLibs.MSG_RECORDS.Message, status, recCnt)
                        : String.Concat(remark, " ", String.Format(MessageLibs.MSG_RECORDS.Message, status, recCnt));
                }

                if (status == Model.EnumBbanFileStatus.Imported)
                {
                    // Count Bban File records
                    int recCnt = _unitOfWork.GetRepository<p_Iban>().GetQueryable(g => g.BbanFileId == id).Count();
                    remark = String.IsNullOrEmpty(remark)
                        ? remark + string.Format(MessageLibs.MSG_RECORDS.Message, status, recCnt)
                        : remark;
                }

                // Database processing
                using (TransactionScope scope = new TransactionScope())
                {
                    // Insert new status to BbanFileHistory table
                    p_BbanFileHistory bbanFileHistory = new p_BbanFileHistory
                    {
                        BbanFileId = id,
                        BbanFileStatusId = (p_EnumBbanFileStatus)status,
                        Remark = remark,
                        Context = _context,
                        ChangedDate = DateTime.Now,
                        ChangedBy = _username
                    };

                    _unitOfWork.GetRepository<p_BbanFileHistory>().Insert(bbanFileHistory);
                    _unitOfWork.CommitChanges();

                    // Update current status to BbanFile table
                    bbanFiledata.CurrentStatusHistoryId = bbanFileHistory.HistoryId;
                    _unitOfWork.GetRepository<p_BbanFile>().Update(bbanFiledata);
                    _unitOfWork.CommitChanges();

                    scope.Complete();
                }
            }
            else
            {
                throw new ArgumentException(string.Format(MessageLibs.MSG_INVALID.Message, "BBAN File Status"), "status");
            }
        }

        /// <summary>
        /// Checks the status.
        /// </summary>
        /// <param name="currentStatus">The current status.</param>
        /// <param name="newStatus">The new status.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckBbanFileStatus(p_EnumBbanFileStatus? currentStatus, p_EnumBbanFileStatus newStatus)
        {
            // Case Upload
            if (newStatus == p_EnumBbanFileStatus.Uploaded && currentStatus == null)
            {
                return true;
            }

            // Case UploadCanceled
            if (newStatus == p_EnumBbanFileStatus.UploadCanceled && currentStatus == p_EnumBbanFileStatus.Verified)
            {
                return true;
            }

            // Case Verifying
            if (newStatus == p_EnumBbanFileStatus.Verifying && currentStatus == p_EnumBbanFileStatus.Uploaded)
            {
                return true;
            }

            // Case Verified
            if (newStatus == p_EnumBbanFileStatus.Verified && currentStatus == p_EnumBbanFileStatus.Verifying)
            {
                return true;
            }

            // Case VerifiedFailed
            if (newStatus == p_EnumBbanFileStatus.VerifiedFailed && currentStatus == p_EnumBbanFileStatus.Verifying)
            {
                return true;
            }

            // Case WaitingForApproval
            if (newStatus == p_EnumBbanFileStatus.WaitingForApproval && currentStatus == p_EnumBbanFileStatus.Verified)
            {
                return true;
            }

            // Case Approved
            if (newStatus == p_EnumBbanFileStatus.Approved && currentStatus == p_EnumBbanFileStatus.WaitingForApproval)
            {
                return true;
            }

            // Case ApprovalDenied
            if (newStatus == p_EnumBbanFileStatus.ApprovalDenied && currentStatus == p_EnumBbanFileStatus.WaitingForApproval)
            {
                return true;
            }

            // Case Importing
            if (newStatus == p_EnumBbanFileStatus.Importing && currentStatus == p_EnumBbanFileStatus.Approved)
            {
                return true;
            }

            // Case Imported
            if (newStatus == p_EnumBbanFileStatus.Imported && currentStatus == p_EnumBbanFileStatus.Importing)
            {
                return true;
            }

            // Case ImportedFailed
            if (newStatus == p_EnumBbanFileStatus.ImportFailed && currentStatus == p_EnumBbanFileStatus.Importing)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Bbans the file.
        /// </summary>
        /// <param name="bbanFilePoco">The bban file poco.</param>
        /// <returns>IEnumerable&lt;Model.BbanFile&gt;.</returns>
        private IEnumerable<Model.BbanFile> ConvertToBbanFile(IEnumerable<p_BbanFile> bbanFilePoco)
        {
            return bbanFilePoco.Where(x => x.CurrentStatusHistoryId.HasValue).Select(x => new
            {
                BbanFile = x,
                BbanFileHistory = x.BbanFileHistory.FirstOrDefault(a => x.CurrentStatusHistoryId.Value == a.HistoryId),
            })
            .Select(s => new Model.BbanFile()
            {
                Id = s.BbanFile.BbanFileId,
                Name = s.BbanFile.Name,
                Hash = s.BbanFile.Hash,
                CurrentStatus = new Model.BbanFileHistory
                {
                    Id = s.BbanFileHistory.HistoryId,
                    Status = Enum.GetName(typeof(Model.EnumBbanFileStatus), s.BbanFileHistory.BbanFileStatusId),
                    Remark = s.BbanFileHistory.Remark,
                    ChangedDate = s.BbanFileHistory.ChangedDate,
                    ChangedBy = s.BbanFileHistory.ChangedBy,
                    Context = s.BbanFileHistory.Context
                }
            });
        }

        /// <summary>
        /// Checks the approver.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckApprover(int bbanFileId)
        {
            // Get bban file history with bbanfile id and 
            p_BbanFileHistory bbanFileHistory =
                _unitOfWork.GetRepository<p_BbanFileHistory>()
                    .GetQueryable(
                        g =>
                            g.BbanFileId == bbanFileId &&
                            g.BbanFileStatusId == p_EnumBbanFileStatus.Uploaded &&
                            g.ChangedBy == _username).OrderByDescending(o => o.ChangedDate)
                    .FirstOrDefault();

            if (bbanFileHistory == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method intended to sends the email message via chilkat component.
        /// </summary>
        /// <param name="emailReceiver">The email receiver.</param>
        /// <param name="emailMessage">The email message body.</param>
        /// <param name="subjectMessage">The subject message.</param>
        /// <returns><c>true</c> if send email success, <c>false</c> if send email fail.</returns>
        private void SendEmail(string emailReceiver, string emailMessage, string subjectMessage)
        {
            try
            {
                using (Email email = new Email())
                {
                    email.SetHtmlBody(emailMessage);
                    email.Subject = subjectMessage;
                    // split email address.
                    foreach (string mail in emailReceiver.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(m => m.Trim()))
                    {
                        email.AddTo(mail, mail);
                    }
                    email.FromName = _mailSetting.EmailFromName; // check where sender come from.
                    email.FromAddress = _mailSetting.EmailAccount;
                    email.UnSpamify();

                    Send(email);
                }
            }
            catch (Exception ex)
            {
                throw new IbanOperationException(MessageLibs.ERR_SEND_EMAIL.Code, MessageLibs.ERR_SEND_EMAIL.Message, ex);
            }
        }

        /// <summary>
        /// This method intended to initialize the mail man object and return it as a result.
        /// </summary>
        /// <returns>MailMan.</returns>
        private MailMan InitialMailMan()
        {
            // initialize mailman with email setting.
            MailMan mailman = new MailMan()
            {
                SmtpHost = _mailSetting.EmailServer,
                SmtpPort = _mailSetting.EmailSmtpPort,
                SmtpSsl = true,  // set enable ssl
                SmtpUsername = _mailSetting.EmailSmtpAccount,
                SmtpPassword = _mailSetting.EmailPassword
            };

            // unlock Chilkat with license code
            mailman.UnlockComponent(_mailSetting.MailmanComponentCode);

            return mailman;
        }

        /// <summary>
        /// This method intended to configures the email setting by load xml configuration file and parse the value to email setting.
        /// </summary>
        /// <param name="xmlConfiguration">The email configuration in type of xml string.</param>
        /// <exception cref="System.ArgumentNullException">xmlConfiguration</exception>
        private void ConfigureEmailSetting(string xmlConfiguration)
        {
            // check if xml configuration value is null or whitespace will throw ArgumentNullException.
            if (String.IsNullOrWhiteSpace(xmlConfiguration))
            {
                throw new ArgumentNullException("xmlConfiguration");
            }

            // create xDocument by parsing configuration.
            XDocument xdoc = XDocument.Parse(xmlConfiguration);
            XElement element = xdoc.Element("Setting").Element("EmailSetting");
            if (element != null)
            {
                _mailSetting = new EmailSetting
                {
                    MailmanComponentCode = String.IsNullOrWhiteSpace(element.Element("MailManComponentCode").Value)
                        ? null
                        : element.Element("MailManComponentCode").Value,
                    EmailServer = String.IsNullOrWhiteSpace(element.Element("EmailServer").Value)
                        ? null
                        : element.Element("EmailServer").Value,
                    EmailSmtpPort = String.IsNullOrWhiteSpace(element.Element("EmailSmtpPort").Value)
                        ? 0
                        : Convert.ToInt32(element.Element("EmailSmtpPort").Value),
                    EmailAccount = String.IsNullOrWhiteSpace(element.Element("EmailAccount").Value)
                        ? null
                        : element.Element("EmailAccount").Value,
                    EmailPassword = String.IsNullOrWhiteSpace(element.Element("EmailPassword").Value)
                        ? null
                        : element.Element("EmailPassword").Value,
                    EmailFromName = String.IsNullOrWhiteSpace(element.Element("EmailFromName").Value)
                        ? null
                        : element.Element("EmailFromName").Value,
                    EmailSmtpAccount = String.IsNullOrWhiteSpace(element.Element("EmailSmtpAccount").Value)
                        ? null
                        : element.Element("EmailSmtpAccount").Value,
                };
            }

            // call for validate email setting.
            ValidateEmailSetting();
        }

        /// <summary>
        /// This method intended to validates the email setting.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">_mailSetting
        /// or invalid property</exception>
        private void ValidateEmailSetting()
        {
            // check if mail setting is null will throw exception.
            if (_mailSetting == null)
            {
                throw new ArgumentNullException("_mailSetting");
            }

            // loop check all property of _mailSetting.
            foreach (PropertyInfo propertyInfo in _mailSetting.GetType().GetProperties())
            {
                // if property is string will check if null or white space if it's true will throw ArgumentNullException
                if (propertyInfo.PropertyType == typeof(string))
                {
                    string value = (string)propertyInfo.GetValue(_mailSetting);
                    if (String.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentNullException(propertyInfo.Name);
                    }
                }
                // if property is integer will check if value is 0 if it's true will throw ArgumentNullException
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    int value = (int)propertyInfo.GetValue(_mailSetting);
                    if (value == 0)
                    {
                        throw new ArgumentNullException(propertyInfo.Name);
                    }
                }
            }
        }

        /// <summary>
        /// This method intended to sends the specified email.
        /// </summary>
        /// <param name="email">The email object for send.</param>
        /// <returns><c>true</c> if send email success, <c>false</c> if send email fail.</returns>
        private void Send(Email email)
        {
            // set result to true.
            MailMan mailman = InitialMailMan();
            try
            {
                // sending email if failed change status to false
                if (!mailman.SendEmail(email))
                {
                    throw new InvalidOperationException(mailman.LastErrorText);
                }
            }
            finally
            {
                // close connection and clear resource
                mailman.CloseSmtpConnection();
                mailman.Dispose();
            }
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

                if (_mailSetting != null)
                {
                    _mailSetting = null;
                }

                // Sets dispose flag.
                _disposed = true;
            }
        }

        #endregion
    }
}
