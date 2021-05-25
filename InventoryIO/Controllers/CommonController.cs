using System;
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
    public class CommonController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IUnitService _unitService;
        private readonly ISupplierService _supplierService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public CommonController(ICustomerService customerService,
            IUnitService unitService,
            ISupplierService supplierService,
            IUserService userService,
            IProductService productService)
        {
            this._customerService = customerService;
            this._unitService = unitService;
            this._supplierService = supplierService;
            this._userService = userService;
            this._productService = productService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CustomerList()
        {
            List<CustomerDetail> customerDetailResult = new List<CustomerDetail>();

            customerDetailResult = _customerService.GetAllCustomerDetails().Where(m => m.IsActive).ToList();


            var response = new
            {
                result = customerDetailResult
            };
            return Ok(response);
        }

        [HttpGet]
        public ActionResult CustomerListByCode(string customerCode)
        {
            List<CustomerDetail> customerDetailResult = new List<CustomerDetail>();

            customerDetailResult = _customerService.GetAllCustomerDetails().Where(m => m.IsActive && m.CustomerCode == customerCode).ToList();


            var response = new
            {
                result = customerDetailResult
            };
            return Ok(response);
        }

        [HttpGet]
        public ActionResult CustomerAndProductAndPriceList()
        {
            List<CustomerPricingDetail> customerPricingDetailResult = new List<CustomerPricingDetail>();

            customerPricingDetailResult = _customerService.GetAllCustomerPricingDetails().ToList();


            var response = new
            {
                result = customerPricingDetailResult
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("Common/CustomerAndProductPriceDetails/{customerId}/{productId}")]
        public ActionResult CustomerAndProductPriceDetails(long customerId, long productId)
        {
            List<CustomerPricingDetail> customerPricingDetailResult = new List<CustomerPricingDetail>();
            //CustomerPricingDetail customerPricingDetailResult = new CustomerPricingDetail();

            if (productId == 0)
            {
                customerPricingDetailResult = _customerService.GetAllCustomerPricingDetails()
                .Where(m => m.CustomerId == customerId).ToList();
            }
            else
            {
                customerPricingDetailResult = _customerService.GetAllCustomerPricingDetails()
                .Where(m => m.CustomerId == customerId
                        && m.ProductId == productId).ToList();
            }
            


            var response = new
            {
                result = customerPricingDetailResult
            };
            return Ok(response);
        }

        [HttpGet]
        public ActionResult UnitList()
        {
            List<UnitDetail> unitDetailResult = new List<UnitDetail>();


            unitDetailResult = _unitService.GetAllUnitDetails().ToList();


            var response = new
            {
                result = unitDetailResult
            };
            return Ok(response);
        }

        [HttpGet]
        public ActionResult SupplierList()
        {
            List<SupplierDetail> supplierDetailResult = new List<SupplierDetail>();


            supplierDetailResult = _supplierService.GetAllSupplierDetails().ToList();

            var response = new
            {
                result = supplierDetailResult
            };

            return Ok(response);
        }

        [HttpGet]
        public ActionResult ProductList()
        {
            List<ProductDetail> productDetaillResult = new List<ProductDetail>();

            
            productDetaillResult = _productService.GetAllProductDetails().ToList();

            var response = new
            {
                result = productDetaillResult
            };

            return Ok(response);
        }
    }
}