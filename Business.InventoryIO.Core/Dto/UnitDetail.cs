using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    public class UnitDetail
    {
        public long UnitId { get; set; }

        [StringLength(16)]
        public string UnitDescription { get; set; }
    }
}
