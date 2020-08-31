namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MenuDetails")]
    public partial class MenuDetail
    {
        public MenuDetail()
        {
            UserRoleDetails = new HashSet<UserRoleDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuID { get; set; }

        public int? ParentMenuID { get; set; }

        [Required]
        [StringLength(64)]
        public string MenuName { get; set; }

        public virtual ICollection<UserRoleDetail> UserRoleDetails { get; set; }
    }
}
