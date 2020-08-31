﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Business.InventoryIO.Core;
using Business.InventoryIO.Core.Dto;
using Business.InventoryIO.Core.Interface;
using Infrastructure.Utilities;
using Newtonsoft.Json;

namespace InventoryIO.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult CustomerList()
        {
            List<CustomerDetail> customerDetailResult = new List<CustomerDetail>();

            customerDetailResult = _customerService.GetAllCustomerDetails().Where(m => m.IsActive).ToList();

            var response = new
            {
                CustomerDetailResult = customerDetailResult
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult AddNewCustomerDetails(CustomerDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            long customerIdResult = 0;

            //var currentUserId = Session[LookupKey.SessionVariables.UserId].IsNull() ? 0 : Convert.ToInt64(Session[LookupKey.SessionVariables.UserId]);
            var currentUserId = 0;

            request.CreatedBy = currentUserId;
            request.CreatedTime = DateTime.Now;

            if (ModelState.IsValid)
            {

                customerIdResult = _customerService.SaveCustomerDetails(request);

                if (customerIdResult == -100)
                {
                    return Json(new { isSucess = isSucess, messageAlert = Messages.CustomerCodeValidation });
                }
                if (customerIdResult == 0)
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
                return Json(new
                {
                    isSucess = isSucess,
                    messageAlert = Messages.ErrorOccuredDuringProcessing
                });
            }
        }

        [HttpPost]
        public JsonResult AddNewCustomerPrice(CustomerPriceDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            long customerIdResult = 0;

            //var currentUserId = Session[LookupKey.SessionVariables.UserId].IsNull() ? 0 : Convert.ToInt64(Session[LookupKey.SessionVariables.UserId]);
            var currentUserId = 0;

            request.CreatedBy = currentUserId;
            request.CreatedTime = DateTime.Now;

            if (ModelState.IsValid)
            {

                customerIdResult = _customerService.SaveCustomerPrice(request);
                
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
                return Json(new
                {
                    isSucess = isSucess,
                    messageAlert = Messages.ErrorOccuredDuringProcessing
                });
            }
        }

        [HttpPost]
        public JsonResult UpdateCustomerPriceDetails(CustomerPriceDetailRequest request)
        {
            bool isSucess = false, customerPriceDeleteResult = false;
            string messageAlert = string.Empty;
            long customerIdResult = 0;

            //var currentUserId = Session[LookupKey.SessionVariables.UserId].IsNull() ? 0 : Convert.ToInt64(Session[LookupKey.SessionVariables.UserId]);
            var currentUserId = 0;

            request.CreatedBy = currentUserId;
            request.CreatedTime = DateTime.Now;

            customerPriceDeleteResult = _customerService.DeleteCustomerPriceDetails(request);

            if (!customerPriceDeleteResult)
            {
                return Json(new { isSucess = isSucess, messageAlert = Messages.ServerError });
            }

            customerIdResult = _customerService.SaveCustomerPrice(request);

            if (customerIdResult < 0)
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

        [HttpPost]
        public JsonResult UpdateCustomerDetails(CustomerDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            bool customerIdResult = false;

            //var currentUserId = Session[LookupKey.SessionVariables.UserId].IsNull() ? 0 : Convert.ToInt64(Session[LookupKey.SessionVariables.UserId]);
            var currentUserId = 0;
            var passedUserResult = _customerService.GetAllCustomerDetails().Where(m => m.CustomerId == request.CustomerId).FirstOrDefault();

            request.CreatedTime = passedUserResult.CreatedTime;
            request.ModifiedBy = currentUserId;
            request.ModifiedTime = DateTime.Now;

            #region Validate same customer code
            var codeUserDetailResult = _customerService.GetAllCustomerDetails().Where(u => u.CustomerCode == request.CustomerCode
                                                                 && u.CustomerId != request.CustomerId
                                                                           && u.IsActive).FirstOrDefault();


            if (!codeUserDetailResult.IsNull())
            {
                return Json(new { isSucess = isSucess, messageAlert = Messages.CustomerCodeValidation });
            }
            #endregion

            customerIdResult = _customerService.UpdateCustomerDetails(request);

            if (!customerIdResult)
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
    }
}