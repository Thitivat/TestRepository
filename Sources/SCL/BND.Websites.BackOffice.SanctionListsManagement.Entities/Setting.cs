namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents single setting.
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Setting Id.
        /// </summary>
        public int SettingId { get; set; }

        /// <summary>
        /// Setting key used to identify the setting.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Setting value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// <see cref="Entities.ListType"/> that setting belongs to.
        /// </summary>
        public ListType ListType { get; set; }
    }
}
