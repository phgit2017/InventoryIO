using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    class OrderTransactionDetail
    {
    }

    public partial class OrderTransactionRequest : BaseDetail
    {
        public long OrderId { get; set; }

        public int OrderTypeId { get; set; }

        public decimal TotalQuantity { get; set; }

        public decimal TotalAmount { get; set; }
    }

    /// <summary>
    /// SalesOrder
    /// </summary>
    public partial class OrderTransactionRequest
    {
        public string Messenger { get; set; }

        public string PaymentTerms { get; set; }

        public long CustomerId { get; set; }

        public bool IsOrderQueue { get; set; }
    }
    
    public partial class OrderTransactionDetailRequest : BaseDetail
    {
        public long ProductId { get; set; }

        [Required]
        [StringLength(16)]
        public string ProductCode { get; set; }

        [StringLength(256)]
        public string ProductDescription { get; set; }

        [StringLength(32)]
        public string ProductExtension { get; set; }

        public decimal Quantity { get; set; }

        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Purchase Order Detail
    /// </summary>
    public partial class OrderTransactionDetailRequest
    {
        public int? SupplierId { get; set; }
    }

    /// <summary>
    /// Sales Order Detail
    /// </summary>
    public partial class OrderTransactionDetailRequest
    {
        public decimal UnitPrice { get; set; }

        public long UnitId { get; set; }
    }
}
