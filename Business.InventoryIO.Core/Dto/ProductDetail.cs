using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    public class ProductDetail : BaseDetail
    {
        public long ProductId { get; set; }

        [Required]
        [StringLength(16)]
        public string ProductCode { get; set; }

        [StringLength(32)]
        public string ProductDescription { get; set; }

        [StringLength(32)]
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

        [StringLength(32)]
        public string ProductDescription { get; set; }

        [StringLength(32)]
        public string ProductExtension { get; set; }

        public decimal? Quantity { get; set; }

        public bool IsActive { get; set; }
    }
}
