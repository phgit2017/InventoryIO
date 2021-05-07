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
    public class SupplierController : Controller
    {

        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            this._supplierService = supplierService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SupplierList()
        {
            var result = _supplierService.GetAllSupplierDetails().Where(m => m.IsActive).ToList();
            
            return Ok(result);
        }

        [HttpPost]
        public ActionResult AddNewSupplierDetails([FromBody]SupplierDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            long supplierIdResult = 0;
            
            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));

            request.CreatedBy = currentUserId;
            request.CreatedTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                
                supplierIdResult = _supplierService.SaveSupplierDetails(request);

                if (supplierIdResult == -100)
                {
                    return Ok(new { isSucess = isSucess, messageAlert = Messages.SupplierCodeValidation });
                }
                if (supplierIdResult == 0)
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
        public ActionResult UpdateSupplierDetails([FromBody]SupplierDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            bool supplierIdResult = false;

            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));
            var passedSupplierResult = _supplierService.GetAllSupplierDetails().Where(m => m.SupplierId == request.SupplierId).FirstOrDefault();

            request.CreatedTime = passedSupplierResult.CreatedTime;
            request.ModifiedBy = currentUserId;
            request.ModifiedTime = DateTime.Now;

            #region Validate same supplier code
            var codeSupplierDetailResult = _supplierService.GetAllSupplierDetails().Where(u => u.SupplierCode == request.SupplierCode
                                                                 && u.SupplierId != request.SupplierId
                                                                           && u.IsActive).FirstOrDefault();


            if (!codeSupplierDetailResult.IsNull())
            {
                return Ok(new { isSucess = isSucess, messageAlert = Messages.SupplierCodeValidation });
            }
            #endregion

            supplierIdResult = _supplierService.UpdateSupplierDetails(request);

            if (!supplierIdResult)
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