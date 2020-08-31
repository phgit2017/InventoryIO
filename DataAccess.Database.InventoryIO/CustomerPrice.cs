namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CustomerPrices")]
    public partial class CustomerPrice
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CustomerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductID { get; set; }

        public decimal ProductPrice { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
