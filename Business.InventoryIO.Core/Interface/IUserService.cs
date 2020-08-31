using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using Business.InventoryIO.Core.Dto;

namespace Business.InventoryIO.Core.Interface
{
    public interface IUserService
    {
        UserDetail AuthenticateLogin(AuthenticateUserRequest request);
        IQueryable<UserDetail> GetAllUserDetails();
        long SaveUserDetails(UserDetailRequest request);
        bool UpdateUserDetails(UserDetailRequest request);
    }
}
