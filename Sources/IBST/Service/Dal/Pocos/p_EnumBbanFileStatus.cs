

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
using System.ComponentModel.DataAnnotations.Schema;
namespace BND.Services.IbanStore.Service.Dal.Pocos
{
    /// <summary>
    /// Class p_EnumBbanFileStatus is a poco entity representing ib.EnumBbanFileStatus table in the IBanStore database.
    /// </summary>
    public enum p_EnumBbanFileStatus
    {
        /// <summary>
        /// The BBAN file is added to database, they're pass checking.
        /// </summary>
        Uploaded  = 11,

        /// <summary>
        /// The BBAN file that uploaded need to cancel.
        /// </summary>
        UploadCanceled = 12,

        /// <summary>
        /// The BBAN file in status of verifying process.
        /// </summary>
        Verifying = 13,

        /// <summary>
        /// The BBAN file that passed the checking.
        /// </summary>
        Verified = 14,

        /// <summary>
        /// The BBAN file is passed by check file format, BBAN number format and 11 prove.
        /// </summary>
        VerifiedFailed = 15,

        /// <summary>
        /// The BBAN file ready to change to approve status by approver.
        /// </summary>
        WaitingForApproval = 16,

        /// <summary>
        /// The BBAN file is accepted by approver.
        /// </summary>
        Approved = 17,

        /// <summary>
        /// The BBAN file is deny by approver.
        /// </summary>
        ApprovalDenied = 18,

        /// <summary>
        /// The BBAN file is in process of generate IBAN.
        /// </summary>
        Importing = 19,

        /// <summary>
        /// The BBAN file that importing is successfully.
        /// </summary>
        Imported = 20,

        /// <summary>
        /// The BBAN file that got the problem on importing process.
        /// </summary>
        ImportFailed = 21
    }
}
