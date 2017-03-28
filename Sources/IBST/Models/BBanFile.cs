
namespace BND.Services.IbanStore.Models
{
    /// <summary>
    /// Class BBanFile.
    /// </summary>
    public class BbanFile
    {
        /// <summary>
        /// BbanfileId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// name of  Bban file ex. file_1.xslx
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///  hash value from file after uploaded  
        /// </summary>
        /// <value>The hash.</value>
        public string Hash { get; set; }

        /// <summary>
        /// latest status of Bbanfile
        /// </summary>
        public BbanFileHistory CurrentStatus { get; set; }
    }
}
