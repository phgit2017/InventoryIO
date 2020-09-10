namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderTransactionTypes")]
    public partial class OrderTransactionType
    {
        public OrderTransactionType()
        {
            ProductHistory = new HashSet<ProductHistory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderTransactionTypeID { get; set; }

        [StringLength(32)]
        public string OrderTransactionTypeName { get; set; }

        public virtual ICollection<ProductHistory> ProductHistory { get; set; }
    }
}
