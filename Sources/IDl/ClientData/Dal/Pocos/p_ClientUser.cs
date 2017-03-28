using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BND.Services.Payments.iDeal.ClientData.Dal.Pocos
{
    [Table("dbo.ClientUsers")]
    public partial class p_ClientUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public p_ClientUser()
        {
            ProductClients = new HashSet<p_ProductClient>();
        }

        [Key]
        public int ID { get; set; }

        public int ClientID { get; set; }

        [StringLength(256)]
        public string FirstName { get; set; }

        [StringLength(64)]
        public string Initials { get; set; }

        [StringLength(256)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string LastNamePrefix { get; set; }

        [StringLength(8)]
        public string Sex { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public decimal? Burgerservicenummer { get; set; }

        public DateTime Inserted { get; set; }

        [Required]
        [StringLength(128)]
        public string InsertedBy { get; set; }

        public DateTime Updated { get; set; }

        [Required]
        [StringLength(128)]
        public string UpdatedBy { get; set; }

        [StringLength(3)]
        public string Nationality { get; set; }

        public DateTime? DateOfDecease { get; set; }

        public int? AddressID { get; set; }

        public int? FactuaID { get; set; }

        public byte[] Avatar { get; set; }

        public int? OldClientID { get; set; }

        public DateTime? ClientSplitDate { get; set; }

        public bool? USPerson { get; set; }

        [StringLength(3)]
        public string TaxCountry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_ProductClient> ProductClients { get; set; }
    }
}
