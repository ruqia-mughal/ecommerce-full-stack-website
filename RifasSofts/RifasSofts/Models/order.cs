namespace RifasSofts.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            orderDetails = new HashSet<orderDetail>();
        }

        [Key]
        public int ORDER_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ORDER_TYPE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ORDER_DATE { get; set; }

        [Required]
        [StringLength(50)]
        public string ORDER_STATUS { get; set; }

        [Required]
        [StringLength(50)]
        public string ORDER_NAME { get; set; }

        [StringLength(50)]
        public string ORDER_CONTACT { get; set; }

        [StringLength(50)]
        public string ORDER_EMAIL { get; set; }

        [StringLength(150)]
        public string ORDER_ADDRESS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderDetail> orderDetails { get; set; }
    }
}
