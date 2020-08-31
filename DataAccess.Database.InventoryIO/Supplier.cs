namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Suppliers")]
    public partial class Supplier
    {
        [Key]
        public long SupplierID { get; set; }

        [StringLength(16)]
        public string SupplierCode { get; set; }

        [StringLength(256)]
        public string SupplierName { get; set; }

        public bool IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }
    }
}
