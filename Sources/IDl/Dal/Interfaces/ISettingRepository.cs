
namespace BND.Services.Payments.iDeal.Dal
{
    /// <summary>
    /// Interface ISettingRepository
    /// </summary>
    public interface ISettingRepository
    {
        /// <summary>
        /// Gets the value by key.
        /// </summary>
        /// <param name="key">The key for retrieve data.</param>
        /// <returns>System.String.</returns>
        string GetValueByKey(string key);
    }
}
