namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SalesOrders")]
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        public long SalesOrderID { get; set; }

        public int OrderTypeID { get; set; }

        public decimal TotalQuantity { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(64)]
        public string SalesNo { get; set; }

        [StringLength(128)]
        public string Messenger { get; set; }

        [StringLength(128)]
        public string PaymentTerms { get; set; }

        public long CustomerID { get; set; }

        public bool IsOrderQueue { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [ForeignKey("OrderTypeID")]
        public virtual OrderType OrderType { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
