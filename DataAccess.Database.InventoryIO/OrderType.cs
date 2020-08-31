namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderTypes")]
    public partial class OrderType
    {
        public OrderType()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderTypeID { get; set; }

        [StringLength(16)]
        public string OrderTypeName { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
