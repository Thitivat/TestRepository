

/// <summary>
/// The Bndb.IBanStore.Dal.Pocos namespace contains all poco entities about IBanStore.
/// </summary>
using System.ComponentModel.DataAnnotations.Schema;
namespace BND.Services.IbanStore.Service.Dal.Pocos
{
    /// <summary>
    /// Class p_EnumIbanStatus is a poco entity representing ib.EnumIBanStatus table in the database.
    /// </summary>
    public enum p_EnumIbanStatus
    {
        /// <summary>
        /// The IBAN ready to use.
        /// </summary>
        Available = 11,

        /// <summary>
        /// The IBAN already use by assigned to someone.
        /// </summary>
        Assigned = 12,

        /// <summary>
        /// The IBAN that assigned need to cancel with some reason.
        /// </summary>
        Canceled = 13,

        /// <summary>
        /// The IBAN that assigned pass the state of verify KYC procedures.
        /// </summary>
        Active = 14
    }
}
