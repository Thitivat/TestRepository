namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    /// <summary>
    /// Represents types of actions that are logged in the system.
    /// </summary>
    public class ActionType
    {
        /// <summary>
        /// Action Identifier.
        /// </summary>
        public int ActionTypeId { get; set; }

        /// <summary>
        /// Name of the action.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Action description.
        /// </summary>
        public string Description { get; set; }

    }
}
