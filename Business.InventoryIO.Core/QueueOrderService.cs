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
    public partial class QueueOrderService
    {
        IProductService _productService;
        ICustomerService _customerService;
        IInventoryIORepository<dbentities.SalesOrder> _salesOrderService;
        IInventoryIORepository<dbentities.SalesOrderDetail> _salesOrderDetailService;


        private dbentities.SalesOrder salesOrder;
        private dbentities.SalesOrderDetail salesOrderDetail;

        public QueueOrderService(
            IProductService productService,
            ICustomerService customerService,
        IInventoryIORepository<dbentities.SalesOrder> _salesOrderService,
        IInventoryIORepository<dbentities.SalesOrderDetail> _salesOrderDetailService)
        {
            this._productService = productService;
            this._customerService = customerService;
            this._salesOrderService = _salesOrderService;
            this._salesOrderDetailService = _salesOrderDetailService;

            this.salesOrder = new dbentities.SalesOrder();
            this.salesOrderDetail = new dbentities.SalesOrderDetail();
        }
    }

    public partial class QueueOrderService : IOrderService
    {
        public long UpdateOrderTransacion(OrderTransactionRequest orderTransactionRequest,
            List<OrderTransactionDetailRequest> orderTransactionDetailRequest)
        {
            decimal totalAmount = 0.00m, totalQuantity = 0.00m;
            long salesOrderId = 0, successReturn = 0;

            ProductDetail productDetailResult = new ProductDetail();

            foreach (var orderDetail in orderTransactionDetailRequest)
            {
                totalAmount += totalAmount + orderDetail.UnitPrice;
                totalQuantity += totalQuantity + orderDetail.Quantity;
            }

            orderTransactionRequest.TotalQuantity = totalQuantity;
            orderTransactionRequest.TotalAmount = (totalAmount * totalQuantity);

            #region Sales Order
            this.salesOrder = new dbentities.SalesOrder()
            {
                SalesOrderID = 0,
                OrderTypeID = LookupKey.OrderType.Single,
                TotalAmount = totalAmount,
                TotalQuantity = totalQuantity,
                SalesNo = "", //to be done
                Messenger = orderTransactionRequest.Messenger,
                PaymentTerms = orderTransactionRequest.PaymentTerms,
                CustomerID = orderTransactionRequest.CustomerId,
                IsOrderQueue = orderTransactionRequest.IsOrderQueue,
                CreatedBy = orderTransactionRequest.CreatedBy,
                CreatedTime = DateTime.Now,
                ModifiedBy = null,
                ModifiedTime = null
            };

            salesOrderId = _salesOrderService.Insert(this.salesOrder).SalesOrderID;

            if (salesOrderId <= 0)
            {
                return successReturn = 1;
            }
            #endregion

            foreach (var orderDetail in orderTransactionDetailRequest)
            {
                long productId = orderDetail.ProductId;

                #region Customer Price
                var getAllCustomerPricingDetailsResult = _customerService.GetAllCustomerPricingDetails().Where(m => m.CustomerId == orderTransactionRequest.CustomerId
                                                                        && m.ProductId == orderDetail.ProductId
                                                                        && m.ProductPrice == orderDetail.UnitPrice).FirstOrDefault();

                var customerPriceDetailRequest = new CustomerPriceDetailRequest();
                if (getAllCustomerPricingDetailsResult.IsNull())
                {

                    customerPriceDetailRequest.CustomerId = orderTransactionRequest.CustomerId;
                    customerPriceDetailRequest.ProductId = orderDetail.ProductId;
                    customerPriceDetailRequest.ProductPrice = orderDetail.UnitPrice;
                    customerPriceDetailRequest.CreatedBy = orderTransactionRequest.CreatedBy;
                    customerPriceDetailRequest.CreatedTime = DateTime.Now;
                    _customerService.SaveCustomerPrice(customerPriceDetailRequest);
                }
                else
                {
                    customerPriceDetailRequest.CustomerId = orderTransactionRequest.CustomerId;
                    customerPriceDetailRequest.ProductId = orderDetail.ProductId;
                    customerPriceDetailRequest.ProductPrice = orderDetail.UnitPrice;
                    customerPriceDetailRequest.CreatedBy = orderTransactionRequest.CreatedBy;
                    customerPriceDetailRequest.CreatedTime = DateTime.Now;

                    _customerService.UpdateCustomerPriceDetails(customerPriceDetailRequest);
                }

                #endregion

                #region Delete Sales Order Details
                _salesOrderService.Delete(m => m.SalesOrderID == salesOrderId);
                #endregion

                #region Sales Order Details
                this.salesOrderDetail = new dbentities.SalesOrderDetail()
                {
                    SalesOrderID = salesOrderId,
                    ProductID = productId,
                    Quantity = Convert.ToDecimal(orderDetail.Quantity),
                    UnitPrice = orderDetail.UnitPrice,
                    UnitID = orderDetail.UnitId,
                    CreatedBy = orderDetail.CreatedBy,
                    CreatedTime = DateTime.Now,
                    ModifiedBy = null,
                    ModifiedTime = null
                };

                var salesOrderDetailId = _salesOrderDetailService.Insert(this.salesOrderDetail).SalesOrderID;

                if (salesOrderDetailId <= 0)
                {
                    return successReturn = 4;
                }
                #endregion
            }

            return successReturn;
        }
    }
}
