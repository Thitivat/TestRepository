
using System;
using System.Collections.Generic;

namespace BND.Services.IbanStore.Service.Bll.Interfaces
{
    /// <summary>
    /// Interface IBBanFile
    /// </summary>
    public interface  IBbanFileManager : IDisposable
    {
        /// <summary>
        /// Gets the bban files.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="total">The total.</param>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns>IEnumerable&lt;Models.BbanFile&gt;.</returns>
        IEnumerable<Models.BbanFile> Get(int offset, int limit, out int total, int? bbanFileId = null, string status = null);

        /// <summary>
        /// Gets the bbans.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns>IEnumerable&lt;Models.Bban&gt;.</returns>
        IEnumerable<Models.Bban> GetBbans(int offset, int limit, out int total, int bbanFileId);

        /// <summary>
        /// Gets the bban file status.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> GetStatus();

        /// <summary>
        /// Save the bban file to system.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="bbanFile">The bban file.</param>
        /// <returns>System.Int32.</returns>
        int Save(string fileName, byte[] bbanFile);

        /// <summary>
        /// Updates the bban file status to Approved and generate the IBAN file from BBAN number.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="remark">The remark.</param>
        void Approve(int bbanFileId, string remark = null);

        /// <summary>
        /// Denies the bban file vai set the status to "ApprovalDenied".
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="remark">The remark.</param>
        void Deny(int bbanFileId, string remark);

        /// <summary>
        /// Gets the bban file history.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns>IEnumerable&lt;Models.BbanFileHistory&gt;.</returns>
        IEnumerable<Models.BbanFileHistory> GetHistory(int bbanFileId);

        /// <summary>
        /// Verifies the bban existing in the database.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        void VerifyBbanExist(int bbanFileId);

        /// <summary>
        /// Sends to message to Approver by email to notify Bban are ready to approve.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="emailReceiver">The email receiver.</param>
        /// <param name="emailMessage">The email message.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        void SendToApprove(int bbanFileId, string emailReceiver, string emailMessage);

        /// <summary>
        /// Cancels the bban file.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        void Cancel(int bbanFileId);

        /// <summary>
        /// Gets the ibans by bban file identifier.
        /// </summary>
        /// <param name="bbanFileId">The bban file identifier.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="total">The total.</param>
        /// <returns>IEnumerable&lt;Models.IBan&gt;.</returns>
        IEnumerable<Models.Iban> GetIbans(int bbanFileId, int? offset, int? limit, out int total);
    }
}
