namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductHistory")]
    public partial class ProductHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductHistoryID { get; set; }

        public long ProductID { get; set; }

        public decimal? QuantityAmmend { get; set; }

        public decimal? QuantityPrevious { get; set; }

        public decimal? QuantityCurrent { get; set; }

        public int OrderTransactionTypeID { get; set; }

        [StringLength(256)]
        public string OrderRemarks { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("OrderTransactionTypeID")]
        public virtual OrderTransactionType OrderTransactionType { get; set; }
        
    }
}
