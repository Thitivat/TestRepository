namespace BND.Services.IbanStore.Models
{
    /// <summary>
    /// Class Bban.
    /// </summary>
    public class Bban
    {
        /// <summary>
        /// Gets or sets the import identifier.
        /// </summary>
        /// <value>The import identifier.</value>
        public int ImportId { get; set; }

        /// <summary>
        /// Gets or sets the b ban.
        /// </summary>
        /// <value>The bban.</value>
        public string BbanCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is imported.
        /// </summary>
        /// <value><c>true</c> if this instance is imported; otherwise, <c>false</c>.</value>
        public bool IsImported { get; set; }
    }
}