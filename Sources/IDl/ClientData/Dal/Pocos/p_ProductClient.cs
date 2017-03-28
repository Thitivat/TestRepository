using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Payments.iDeal.ClientData.Dal.Pocos
{
    [Table("dbo.ProductClients")]
    public partial class p_ProductClient
    {
        [Key]
        public int Id { get; set; }

        public int ClientUserID { get; set; }

        [Required]
        [StringLength(25)]
        public string ClientUserType { get; set; }

        public int ProductID { get; set; }

        public virtual p_ClientUser ClientUser { get; set; }

        public virtual p_Product Product { get; set; }
    }
}
