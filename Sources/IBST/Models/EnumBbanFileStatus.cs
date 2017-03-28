namespace BND.Services.IbanStore.Models
{
    public enum EnumBbanFileStatus
    {
        /// <summary>
        /// The uploaded status when Uploadfile success
        /// </summary>
        Uploaded = 11,

        /// <summary>
        /// The UploadedCancelled status when cancel by user
        /// </summary>
        UploadCanceled = 12,

        /// <summary>
        /// The Verifying status when bban file verifying after uploaded
        /// </summary>
        Verifying = 13,

        /// <summary>
        /// The Verified status when verify success
        /// </summary>
        Verified = 14,

        /// <summary>
        /// The verified status when virifield error response message error
        /// </summary>
        VerifiedFailed = 15,

        /// <summary>
        /// verify success and offer to approver
        /// </summary>
        WaitingForApproval = 16,

        /// <summary>
        /// Approved by user 
        /// </summary>
        Approved = 17,

        /// <summary>
        /// deny by user
        /// </summary>
        ApprovalDenied = 18,

        /// <summary>
        /// Importing BBAN and verify exist in database
        /// </summary>
        Importing = 19,

        /// <summary>
        /// after Importing and verify Bban update status to Imported
        /// </summary>
        Imported = 20,

        /// <summary>
        /// when Iban exist in database return  ImportedFailed
        /// </summary>
        ImportFailed = 21
    }
}