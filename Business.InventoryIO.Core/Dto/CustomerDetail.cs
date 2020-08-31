using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    public class CustomerDetail : BaseDetail
    {
        public long CustomerId { get; set; }

        [StringLength(16)]
        public string CustomerCode { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        public bool IsActive { get; set; }
    }

    public class CustomerPricingDetail : BaseDetail
    {
        public long CustomerId { get; set; }

        public long ProductId { get; set; }

        public string ProductCode { get; set; }

        public string ProductDescription { get; set; }

        public string ProductExtension { get; set; }

        public decimal ProductPrice { get; set; }

        [StringLength(16)]
        public string CustomerCode { get; set; }

        [StringLength(128)]
        public string CustomerName { get; set; }

        [StringLength(256)]
        public string Address { get; set; }
    }

    public class CustomerDetailRequest : BaseDetail
    {
        public long CustomerId { get; set; }

        [StringLength(16)]
        public string CustomerCode { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        public bool IsActive { get; set; }
    }

    public class CustomerPriceDetailRequest : BaseDetail
    {
        public long CustomerId { get; set; }

        public long ProductId { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
