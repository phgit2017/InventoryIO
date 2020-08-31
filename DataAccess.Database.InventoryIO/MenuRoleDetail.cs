namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MenuRoleDetails")]
    public partial class MenuRoleDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRoleID { get; set; }

        [ForeignKey("MenuID")]
        public virtual MenuDetail MenuDetail { get; set; }

        [ForeignKey("UserRoleID")]
        public virtual UserRoleDetail UserRoleDetail { get; set; }
    }
}
