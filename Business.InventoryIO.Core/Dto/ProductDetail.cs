using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    public class ProductDetail : BaseDetail
    {
        public long ProductId { get; set; }

        public string ProductCode { get; set; }

        public string ProductDescription { get; set; }
        
        public string ProductExtension { get; set; }

        public decimal? Quantity { get; set; }

        public bool IsActive { get; set; }
    }

    public class ProductDetailRequest : BaseDetail
    {
        public long ProductId { get; set; }

        [Required]
        [StringLength(16)]
        public string ProductCode { get; set; }

        [StringLength(256)]
        public string ProductDescription { get; set; }

        [StringLength(32)]
        public string ProductExtension { get; set; }

        public decimal? Quantity { get; set; }

        public bool IsActive { get; set; }

        public string OrderRemarks { get; set; }
    }

    public class ProductHistoryDetail: BaseDetail
    {
        public long ProductHistoryId { get; set; }

        public long ProductId { get; set; }

        public string ProductCode { get; set; }

        public string ProductDescription { get; set; }

        public string ProductExtension { get; set; }

        public decimal? QuantityAmmend { get; set; }

        public decimal? QuantityPrevious { get; set; }

        public decimal? QuantityCurrent { get; set; }

        public int OrderTransactionTypeId { get; set; }

        public string OrderTypeName { get; set; }

        public string OrderRemarks { get; set; }
    }

    public class ProductHistoryDetailRequest : BaseDetail
    {
        public long ProductHistoryId { get; set; }

        public long ProductId { get; set; }

        public decimal? QuantityAmmend { get; set; }

        public decimal? QuantityPrevious { get; set; }

        public decimal? QuantityCurrent { get; set; }

        public int OrderTransactionTypeId { get; set; }

        [StringLength(256)]
        public string OrderRemarks { get; set; }
    }
}
