namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Customers")]
    public partial class Customer
    {
        public Customer()
        {
            CustomerPrices = new HashSet<CustomerPrice>();
        }

        public long CustomerID { get; set; }

        [StringLength(16)]
        public string CustomerCode { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual ICollection<CustomerPrice> CustomerPrices { get; set; }
    }
}
