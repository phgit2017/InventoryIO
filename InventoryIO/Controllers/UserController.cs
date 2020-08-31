using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Business.InventoryIO.Core;
using Business.InventoryIO.Core.Dto;
using Business.InventoryIO.Core.Interface;
using Infrastructure.Utilities;
using Newtonsoft.Json;

namespace InventoryIO.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult UserList()
        {
            List<UserDetail> userDetailsResult = new List<UserDetail>();

            #region Authorize
            //var authorizeMenuAccessResult = AuthorizeMenuAccess(LookupKey.Menu.UserMenuId);
            //if (!authorizeMenuAccessResult.IsSuccess)
            //{

            //    return Json(new
            //    {
            //        isSuccess = authorizeMenuAccessResult.IsSuccess,
            //        messageAlert = authorizeMenuAccessResult.MessageAlert,
            //        UserDetailsResult = userDetailsResult
            //    }, JsonRequestBehavior.AllowGet);
            //}
            #endregion

            //var currentUserId = Session[LookupKey.SessionVariables.UserId].IsNull() ? 0 : Convert.ToInt64(Session[LookupKey.SessionVariables.UserId]);
            var currentUserId = 0;

            userDetailsResult = _userService.GetAllUserDetails().Where(m => m.UserId != currentUserId && m.IsActive).ToList();
            var response = new
            {
                UserDetailsResult = userDetailsResult,
                isSuccess = true,
                messageAlert = ""
            };
            return Json(response);

        }

        [HttpPost]
        public JsonResult AddNewUserDetails(UserDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            long userIdResult = 0;

            //var currentUserId = Session[LookupKey.SessionVariables.UserId].IsNull() ? 0 : Convert.ToInt64(Session[LookupKey.SessionVariables.UserId]);
            var currentUserId = 0;

            request.CreatedBy = currentUserId;
            request.CreatedTime = DateTime.Now;

            if (ModelState.IsValid)
            {

                userIdResult = _userService.SaveUserDetails(request);

                if (userIdResult == -100)
                {
                    return Json(new { isSucess = isSucess, messageAlert = Messages.UserNameValidation });
                }
                if (userIdResult == 0)
                {
                    return Json(new { isSucess = isSucess, messageAlert = Messages.ServerError });
                }

                isSucess = true;
                var response = new
                {
                    isSuccess = isSucess,
                    messageAlert = messageAlert
                };

                return Json(response);
            }
            else
            {
                return Json(new { isSucess = isSucess, messageAlert = Messages.ErrorOccuredDuringProcessing });
            }

        }

        [HttpPost]
        public JsonResult UpdateUserDetails(UserDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            bool userIdResult = false;

            //var currentUserId = Session[LookupKey.SessionVariables.UserId].IsNull() ? 0 : Convert.ToInt64(Session[LookupKey.SessionVariables.UserId]);
            var currentUserId = 0;

            var passedUserResult = _userService.GetAllUserDetails().Where(m => m.UserId == request.UserId).FirstOrDefault();

            request.CreatedTime = passedUserResult.CreatedTime;
            request.ModifiedBy = currentUserId;
            request.ModifiedTime = DateTime.Now;



            //if (ModelState.IsValid)
            //{
            #region Validate same username
            var codeUserDetailResult = _userService.GetAllUserDetails().Where(u => u.UserName == request.UserName
                                                                 && u.UserId != request.UserId
                                                                           && u.IsActive).FirstOrDefault();


            if (!codeUserDetailResult.IsNull())
            {
                return Json(new { isSucess = isSucess, messageAlert = Messages.UserNameValidation });
            }
            #endregion

            userIdResult = _userService.UpdateUserDetails(request);

            if (!userIdResult)
            {
                return Json(new { isSucess = isSucess, messageAlert = Messages.ServerError });
            }

            isSucess = true;
            var response = new
            {
                isSuccess = isSucess,
                messageAlert = messageAlert
            };

            return Json(response);
            //}
            //else
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors)
            //               .ToList();
            //    foreach (var err in errors)
            //    {
            //        Logging.Information("(Response-Model-User) UpdateUserDetails : " + err.ErrorMessage);
            //    }
            //    return Json(new { isSucess = isSucess, messageAlert = Messages.ErrorOccuredDuringProcessing }, JsonRequestBehavior.AllowGet);
            //}

        }

        [HttpPost]
        public JsonResult AuthenticateLogin(AuthenticateUserRequest request)
        {
            bool isSuccess = false;
            var authenticateLoginResult = _userService.AuthenticateLogin(request);

            if (authenticateLoginResult == null)
            {
                return Json(new
                {
                    isSucess = isSuccess,
                    messageAlert = "Invalid User Details"
                });
            }

            isSuccess = true;

            HttpContext.Session.SetString(LookupKey.SessionVariables.UserId, authenticateLoginResult.UserId.ToString());

            HttpContext.Response.Cookies.Append(LookupKey.SessionVariables.UserName, authenticateLoginResult.UserName);
            HttpContext.Response.Cookies.Append(LookupKey.SessionVariables.UserRoleName, authenticateLoginResult.UserRoleName);

            var response = new
            {
                userDetailResult = authenticateLoginResult,
                isSuccess = isSuccess
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            HttpContext.Response.Cookies.Delete(LookupKey.SessionVariables.UserName);
            HttpContext.Response.Cookies.Delete(LookupKey.SessionVariables.UserRoleName);
            HttpContext.Session.Clear();

            var response = new { isSuccess = true, messageAlert = string.Empty };
            return Json(response);
        }
    }
}