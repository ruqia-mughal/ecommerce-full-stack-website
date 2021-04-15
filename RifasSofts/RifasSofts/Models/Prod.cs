namespace RifasSofts.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prod")]
    public partial class Prod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prod()
        {
            orderDetails = new HashSet<orderDetail>();
        }

        [Key]
        public int PROD_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string PROD_NAME { get; set; }

        [StringLength(150)]
        public string PROD_DESCRIPTION { get; set; }

        [StringLength(150)]
        public string PROD_PICTURE { get; set; }

        [NotMapped]
        public HttpPostedFileBase PRO_PIC { get; set; }

        [NotMapped]
        public int PRO_QUANTITY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal PROD_PURCHASE_PRICE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal PROD_SALE_PRICE { get; set; }

        public int CATEGORY_FID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderDetail> orderDetails { get; set; }
    }
}
