using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Security.OTP.RepositoriesTest
{
    internal class Poco2
    {
        [Key]
        public string Email { get; set; }

        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual Poco1 Poco1 { get; set; }
    }
}
