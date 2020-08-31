using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    public class UserDetail : BaseDetail
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        public bool IsActive { get; set; }
    }

    public class UserRoleDetail
    {
        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }
    }

    public class UserDetailRequest : BaseDetail
    {
        public long UserId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(32, ErrorMessage = "Up to 32 characters only.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(128, ErrorMessage = "Up to 128 characters only.")]
        public string Password { get; set; }

        public int UserRoleId { get; set; }

        public bool IsActive { get; set; }
    }

    public class UserMenuAuthorizedRole
    {
        public int MenuId { get; set; }

        public int RoleId { get; set; }

        public long UserId { get; set; }
    }

    public class UserMenuRoleDetail
    {
        public int MenuId { get; set; }

        public int RoleId { get; set; }

    }

    public class MenuDetail
    {
        public int MenuId { get; set; }

        public int? ParentMenuId { get; set; }

        public string MenuName { get; set; }
    }

    public class AuthenticateUserRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
