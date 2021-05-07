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
using dbentities = DataAccess.Database.InventoryIO;
using DataAccess.Repository.InventoryIO.Interface;
using Newtonsoft.Json;


namespace InventoryIO.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        IProductService _productService;
        ICustomerService _customerService;

        IInventoryIORepository<dbentities.PurchaseOrder> _purchaseOrderService;
        IInventoryIORepository<dbentities.PurchaseOrderDetail> _purchaseOrderDetailService;
        IInventoryIORepository<dbentities.SalesOrder> _salesOrderService;
        IInventoryIORepository<dbentities.SalesOrderDetail> _salesOrderDetailService;

        public OrderController(
            IProductService productService,
            ICustomerService customerService,
            IInventoryIORepository<dbentities.PurchaseOrder> purchaseOrderService,
        IInventoryIORepository<dbentities.PurchaseOrderDetail> purchaseOrderDetailService,
        IInventoryIORepository<dbentities.SalesOrder> salesOrderService,
            IInventoryIORepository<dbentities.SalesOrderDetail> salesOrderDetailService)
        {
            this._productService = productService;
            this._customerService = customerService;
            this._salesOrderService = salesOrderService;
            this._salesOrderDetailService = salesOrderDetailService;
            this._purchaseOrderService = purchaseOrderService;
            this._purchaseOrderDetailService = purchaseOrderDetailService;
        }

        #region Order
        public IActionResult UpdatePurchaseOrder([FromBody]PurchaseOrderRequest request)
        {

            bool isSucess = false;
            string messageAlert = string.Empty;
            long updateOrderTransactionResult = 0;

            OrderTransactionRequest orderTransactionRequest = new OrderTransactionRequest();
            List<OrderTransactionDetailRequest> orderTransactionDetailRequest = new List<OrderTransactionDetailRequest>();

            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));

            if (ModelState.IsValid)
            {
                #region Service implementation

                orderTransactionRequest.CreatedBy = currentUserId;

                orderTransactionDetailRequest.Add(new OrderTransactionDetailRequest()
                {
                    ProductId = request.ProductId,
                    ProductCode = request.ProductCode,
                    ProductDescription = request.ProductDescription,
                    ProductExtension = request.ProductExtension,
                    Quantity = request.Quantity,
                    IsActive = request.IsActive,
                    CreatedBy = currentUserId,
                    SupplierId = request.SupplierId

                });
                var type = Type.GetType(string.Format("{0}.{1}, {0}", "Business.InventoryIO.Core", "PurchaseOrderService"));
                IOrderService order = (IOrderService)Activator.CreateInstance(type,
                    _productService,
                    _purchaseOrderService,
                    _purchaseOrderDetailService);

                updateOrderTransactionResult = order.UpdateOrderTransacion(
                    orderTransactionRequest,
                    orderTransactionDetailRequest);

                #endregion

                //IOrderTransactionalServices x = new PurchaseOrderService(_productServices, _orderServices);
                //var type = Type.GetType("Business.AAA.Core.PurchaseOrderService, Business.AAA.Core");
                //updateOrderTransactionResult = x.UpdateOrderTransaction(orderTransactionRequest, orderTransactionDetailRequest);

                if (updateOrderTransactionResult == -100)
                {
                    return Ok(new { isSucess = isSucess, messageAlert = Messages.ProductCodeValidation });
                }
                else if (updateOrderTransactionResult == 0)
                {
                    isSucess = true;
                }

                var response = new
                {
                    isSucess = isSucess,
                    messageAlert = messageAlert
                };
                return Ok(response);
            }
            else
            {

                return Ok(new { isSucess = isSucess, messageAlert = Messages.ErrorOccuredDuringProcessing });
            }


        }

        public IActionResult UpdateSalesOrder([FromBody]SalesOrderRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            long updateOrderTransactionResult = 0;

            OrderTransactionRequest orderTransactionRequest = new OrderTransactionRequest();
            List<OrderTransactionDetailRequest> orderTransactionDetailRequest = new List<OrderTransactionDetailRequest>();

            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));

            #region Service implementation

            orderTransactionRequest.CreatedBy = currentUserId;
            orderTransactionRequest.Messenger = request.Messenger;
            orderTransactionRequest.PaymentTerms = request.PaymentTerms;
            orderTransactionRequest.CustomerId = request.CustomerId;
            orderTransactionRequest.IsOrderQueue = false;


           var type = Type.GetType(string.Format("{0}.{1}, {0}", "Business.InventoryIO.Core", "SalesOrderService"));
            IOrderService order = (IOrderService)Activator.CreateInstance(type,
                _productService,
                _customerService,
                _salesOrderService,
                _salesOrderDetailService);

            updateOrderTransactionResult = order.UpdateOrderTransacion(
                orderTransactionRequest,
                request.OrderTransactionDetailRequest);

            #endregion

            //IOrderTransactionalServices x = new PurchaseOrderService(_productServices, _orderServices);
            //var type = Type.GetType("Business.AAA.Core.PurchaseOrderService, Business.AAA.Core");
            //updateOrderTransactionResult = x.UpdateOrderTransaction(orderTransactionRequest, orderTransactionDetailRequest);


            if (updateOrderTransactionResult == 0)
            {
                isSucess = true;
            }

            var response = new
            {
                isSucess = isSucess,
                messageAlert = messageAlert
            };
            return Ok(response);

        }

        public IActionResult UpdateQueueOrder([FromBody]SalesOrderRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            long updateOrderTransactionResult = 0;

            OrderTransactionRequest orderTransactionRequest = new OrderTransactionRequest();
            List<OrderTransactionDetailRequest> orderTransactionDetailRequest = new List<OrderTransactionDetailRequest>();

            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));

            #region Service implementation

            orderTransactionRequest.CreatedBy = currentUserId;
            orderTransactionRequest.Messenger = request.Messenger;
            orderTransactionRequest.PaymentTerms = request.PaymentTerms;
            orderTransactionRequest.CustomerId = request.CustomerId;
            orderTransactionRequest.IsOrderQueue = true;
            
            var type = Type.GetType(string.Format("{0}.{1}, {0}", "Business.InventoryIO.Core", "QueueOrderService"));
            IOrderService order = (IOrderService)Activator.CreateInstance(type,
                _productService,
                _customerService,
                _salesOrderService,
                _salesOrderDetailService);

            updateOrderTransactionResult = order.UpdateOrderTransacion(
                orderTransactionRequest,
                request.OrderTransactionDetailRequest);

            #endregion

            //IOrderTransactionalServices x = new PurchaseOrderService(_productServices, _orderServices);
            //var type = Type.GetType("Business.AAA.Core.PurchaseOrderService, Business.AAA.Core");
            //updateOrderTransactionResult = x.UpdateOrderTransaction(orderTransactionRequest, orderTransactionDetailRequest);


            if (updateOrderTransactionResult == 0)
            {
                isSucess = true;
            }

            var response = new
            {
                isSucess = isSucess,
                messageAlert = messageAlert
            };
            return Ok(response);

        }
        #endregion

        #region Product
        [HttpPost]
        public IActionResult UpdateProductDetails(ProductDetailRequest request)
        {
            bool isSucess = false;
            string messageAlert = string.Empty;
            bool productUpdateResult = false;

            var currentUserId = HttpContext.Session.GetString(LookupKey.SessionVariables.UserId).IsNull() ? 0 : Convert.ToInt64(HttpContext.Session.GetString(LookupKey.SessionVariables.UserId));
            var passedProductResult = _productService.GetAllProductDetails().Where(m => m.ProductId == request.ProductId).FirstOrDefault();

            request.CreatedTime = passedProductResult.CreatedTime;
            request.ModifiedBy = currentUserId;
            request.ModifiedTime = DateTime.Now;

            var codeProductDetailResult = _productService.GetAllProductDetails().Where(p => p.ProductCode == request.ProductCode
                                                                            && p.IsActive
                                                                            && p.ProductId != request.ProductId).FirstOrDefault();

            #region Validate same product code
            if (!codeProductDetailResult.IsNull())
            {
                return Ok(new { isSucess = isSucess, messageAlert = Messages.ProductCodeValidation });
            }
            #endregion


            //Update Product Details
            productUpdateResult = _productService.UpdateDetails(request);

            if (!productUpdateResult)
            {
                return Ok(new { isSucess = isSucess, messageAlert = Messages.ServerError });
            }

            var productHistoryDetailRequest = new ProductHistoryDetailRequest()
            {
                ProductHistoryId = 0,
                ProductId = request.ProductId,
                QuantityAmmend = 0,
                QuantityPrevious = codeProductDetailResult.Quantity,
                QuantityCurrent = codeProductDetailResult.Quantity,
                OrderTransactionTypeId = LookupKey.OrderTransactionType.UpdateProductDetailsId,
                OrderRemarks = request.OrderRemarks,
                CreatedBy = currentUserId,
                CreatedTime = DateTime.Now,
                ModifiedBy = null,
                ModifiedTime = null
            };

            var productHistoryId = _productService.SaveProductHistory(productHistoryDetailRequest);

            if (productHistoryId <= 0)
            {
                return Ok(new { isSucess = isSucess, messageAlert = Messages.ServerError });
            }

            isSucess = true;
            var response = new
            {
                isSucess = isSucess,
                messageAlert = messageAlert
            };
            return Ok(response);
            



        }
        
        #endregion
    }
}