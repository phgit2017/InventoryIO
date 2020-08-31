using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    public class SupplierDetail : BaseDetail
    {
        public long SupplierId { get; set; }

        [StringLength(16)]
        public string SupplierCode { get; set; }

        [StringLength(256)]
        public string SupplierName { get; set; }

        public bool IsActive { get; set; }
    }

    public class SupplierDetailRequest : BaseDetail
    {
        public long SupplierId { get; set; }

        [Display(Name = "Supplier Code")]
        [StringLength(16, ErrorMessage = "Up to 16 characters only.")]
        public string SupplierCode { get; set; }

        [Display(Name = "Supplier Name")]
        [StringLength(256, ErrorMessage = "Up to 256 characters only.")]
        public string SupplierName { get; set; }

        public bool IsActive { get; set; }
    }
}
