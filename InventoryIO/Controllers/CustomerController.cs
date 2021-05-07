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
        public ActionResult CustomerList()
        {

            var result = _customerService.GetAllCustomerDetails().Where(m => m.IsActive).ToList();
            
            return Ok(result);
        }

        [HttpPost]
        public ActionResult AddNewCustomerDetails([FromBody]CustomerDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            long customerIdResult = 0;

            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));

            request.CreatedBy = currentUserId;
            request.CreatedTime = DateTime.Now;

            if (ModelState.IsValid)
            {

                customerIdResult = _customerService.SaveCustomerDetails(request);

                if (customerIdResult == -100)
                {
                    return Ok(new { isSucess = isSucess, messageAlert = Messages.CustomerCodeValidation });
                }
                if (customerIdResult == 0)
                {
                    return Ok(new { isSucess = isSucess, messageAlert = Messages.ServerError });
                }

                isSucess = true;
                var response = new
                {
                    isSuccess = isSucess,
                    messageAlert = messageAlert
                };

                return Ok(response);
            }
            else
            {
                return Ok(new
                {
                    isSucess = isSucess,
                    messageAlert = Messages.ErrorOccuredDuringProcessing
                });
            }
        }

        

        [HttpPost]
        public ActionResult UpdateCustomerPriceDetails([FromBody]CustomerPriceDetailRequest request)
        {
            bool isSucess = false, customerPriceDeleteResult = false;
            string messageAlert = string.Empty;
            long customerIdResult = 0;

            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));

            request.CreatedBy = currentUserId;
            request.CreatedTime = DateTime.Now;

            var productPriceResult = _customerService.GetAllCustomerPricingDetails().Where(m => m.CustomerId == request.CustomerId && m.ProductId == request.ProductId).FirstOrDefault();
            if (productPriceResult.IsNull())
            {
                customerIdResult = _customerService.SaveCustomerPrice(request);

                isSucess = true;

                return Ok(new
                {
                    isSuccess = isSucess,
                    messageAlert = messageAlert
                });
            }

            customerPriceDeleteResult = _customerService.DeleteCustomerPriceDetails(request);

            if (!customerPriceDeleteResult)
            {
                return Ok(new { isSucess = isSucess, messageAlert = Messages.ServerError });
            }

            customerIdResult = _customerService.SaveCustomerPrice(request);

            if (customerIdResult < 0)
            {
                return Ok(new { isSucess = isSucess, messageAlert = Messages.ServerError });
            }

            isSucess = true;
            var response = new
            {
                isSuccess = isSucess,
                messageAlert = messageAlert
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult UpdateCustomerDetails([FromBody]CustomerDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            bool customerIdResult = false;

            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));
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
                return Ok(new { isSucess = isSucess, messageAlert = Messages.CustomerCodeValidation });
            }
            #endregion

            customerIdResult = _customerService.UpdateCustomerDetails(request);

            if (!customerIdResult)
            {
                return Ok(new { isSucess = isSucess, messageAlert = Messages.ServerError });
            }

            isSucess = true;
            var response = new
            {
                isSuccess = isSucess,
                messageAlert = messageAlert
            };

            return Ok(response);
        }
    }
}