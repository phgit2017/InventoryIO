using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using Business.InventoryIO.Core.Dto;
using Business.InventoryIO.Core.Extensions;
using Business.InventoryIO.Core.Interface;
using DataAccess.Repository.InventoryIO.Interface;
using dbentities = DataAccess.Database.InventoryIO;
using Infrastructure.Utilities;

namespace Business.InventoryIO.Core
{
    public partial class UserService
    {
        IInventoryIORepository<dbentities.UserDetail> _userDetailService;
        IInventoryIORepository<dbentities.UserRoleDetail> _userRoleDetailService;
        IInventoryIORepository<dbentities.MenuDetail> _menuDetailService;
        IInventoryIORepository<dbentities.MenuRoleDetail> _menuRoleDetailService;

        private dbentities.UserDetail userDetails;
        private dbentities.UserRoleDetail userRoleDetails;
        private dbentities.MenuDetail menuDetails;
        private dbentities.MenuRoleDetail menuRoleDetails;

        public UserService(
            IInventoryIORepository<dbentities.UserDetail> userDetailService,
            IInventoryIORepository<dbentities.UserRoleDetail> userRoleDetailService,
            IInventoryIORepository<dbentities.MenuDetail> menuDetailService,
            IInventoryIORepository<dbentities.MenuRoleDetail> menuRoleDetailService)
        {
            this._userDetailService = userDetailService;
            this._userRoleDetailService = userRoleDetailService;
            this._menuDetailService = menuDetailService;
            this._menuRoleDetailService = menuRoleDetailService;

            this.userDetails = new dbentities.UserDetail();
            this.userRoleDetails = new dbentities.UserRoleDetail();
            this.menuDetails = new dbentities.MenuDetail();
            this.menuRoleDetails = new dbentities.MenuRoleDetail();
        }
    }

    public partial class UserService : IUserService
    {
        public UserDetail AuthenticateLogin(AuthenticateUserRequest request)
        {
            Dto.UserDetail result = new Dto.UserDetail();

            result = GetAllUserDetails().Where(m => m.UserName == request.UserName
                                                    && m.Password == request.Password
                                                    && m.IsActive).FirstOrDefault();

            if (result.IsNull())
            {
                return null;
            }

            return result;
        }

        public IQueryable<UserDetail> GetAllUserDetails()
        {
            var result = from det in this._userDetailService.GetAll()
                         select new Dto.UserDetail()
                         {
                             UserId = det.UserID,
                             UserName = det.UserName,
                             IsActive = det.IsActive,
                             Password = det.Password,
                             UserRoleId = det.UserRoleID,
                             CreatedBy = det.CreatedBy,
                             CreatedTime = det.CreatedTime,
                             ModifiedBy = det.ModifiedBy,
                             ModifiedTime = det.ModifiedTime,

                             UserRoleName = det.UserRoleDetail.UserRoleName

                         };

            return result;
        }

        public long SaveUserDetails(UserDetailRequest request)
        {
            request.UserId = 0;
            this.userDetails = request.DtoToEntity();
            var item = this._userDetailService.Insert(this.userDetails);
            if (item == null)
            {
                return 0;
            }

            return item.UserID;
        }

        public bool UpdateUserDetails(UserDetailRequest request)
        {
            this.userDetails = request.DtoToEntity();

            var item = _userDetailService.Update2(this.userDetails);
            if (item == null)
            {
                return false;
            }

            return true;
        }

        public IQueryable<UserRoleDetail> GetAllUserRoles()
        {
            var result = from det in this._userRoleDetailService.GetAll()
                         select new Dto.UserRoleDetail()
                         {
                             UserRoleId = det.UserRoleID,
                             UserRoleName = det.UserRoleName

                         };

            return result;
        }
    }
}
