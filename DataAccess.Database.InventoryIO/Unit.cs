namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Units")]
    public partial class Unit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UnitID { get; set; }

        [Required]
        [StringLength(16)]
        public string UnitDescription { get; set; }

    }
}
