namespace DataAccess.Database.InventoryIO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("UserDetails")]
    public partial class UserDetail
    {
        [Key]
        public long UserID { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        public int UserRoleID { get; set; }

        public bool IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        [ForeignKey("UserRoleID")]
        public virtual UserRoleDetail UserRoleDetail { get; set; }
    }
}
