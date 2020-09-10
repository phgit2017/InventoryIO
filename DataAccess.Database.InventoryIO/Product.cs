namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Products")]
    public partial class Product
    {
        public Product()
        {
            CustomerPrices = new HashSet<CustomerPrice>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            ProductHistory = new HashSet<ProductHistory>();
        }

        public long ProductID { get; set; }

        [Required]
        [StringLength(16)]
        public string ProductCode { get; set; }

        [StringLength(256)]
        public string ProductDescription { get; set; }

        [StringLength(32)]
        public string ProductExtension { get; set; }

        public decimal? Quantity { get; set; }

        public bool IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual ICollection<CustomerPrice> CustomerPrices { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public virtual ICollection<ProductHistory> ProductHistory { get; set; }
    }
}
