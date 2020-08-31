namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("UserRoleDetails")]
    public partial class UserRoleDetail
    {
        public UserRoleDetail()
        {
            UserDetails = new HashSet<UserDetail>();
            MenuRoleDetails = new HashSet<MenuRoleDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRoleID { get; set; }

        [Required]
        [StringLength(32)]
        public string UserRoleName { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }

        public virtual ICollection<MenuRoleDetail> MenuRoleDetails { get; set; }
    }
}
