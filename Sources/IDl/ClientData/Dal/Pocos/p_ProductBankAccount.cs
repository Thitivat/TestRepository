using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Payments.iDeal.ClientData.Dal.Pocos
{
    [Table("dbo.ProductBankAccounts")]
    public partial class p_ProductBankAccount
    {
        [Key]
        public int ID { get; set; }

        public int ProductID { get; set; }

        [StringLength(64)]
        public string Label { get; set; }

        [StringLength(50)]
        public string MatrixIban { get; set; }

        public int? MatrixProductID { get; set; }

        [StringLength(64)]
        public string MatrixProductName { get; set; }

        public DateTime Inserted { get; set; }

        [Required]
        [StringLength(128)]
        public string InsertedBy { get; set; }

        public DateTime Updated { get; set; }

        [Required]
        [StringLength(256)]
        public string UpdatedBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? PaymentRecurringAmount { get; set; }

        [StringLength(50)]
        public string PaymentRecurringInterval { get; set; }

        [StringLength(50)]
        public string PaymentRecurringMethod { get; set; }

        public int? PaymentSingleAmount { get; set; }

        [StringLength(50)]
        public string PaymentSingleMethod { get; set; }

        public virtual p_Product Product { get; set; }
    }
}
