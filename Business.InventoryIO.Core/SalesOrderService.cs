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
    public partial class SalesOrderService
    {
        IProductService _productService;
        IInventoryIORepository<dbentities.SalesOrder> _salesOrderService;
        IInventoryIORepository<dbentities.SalesOrderDetail> _salesOrderDetailService;

        private dbentities.SalesOrder salesOrder;
        private dbentities.SalesOrderDetail salesOrderDetail;

        public SalesOrderService(
            IProductService productService,
        IInventoryIORepository<dbentities.SalesOrder> _salesOrderService,
        IInventoryIORepository<dbentities.SalesOrderDetail> _salesOrderDetailService)
        {
            this._productService = productService;
            this._salesOrderService = _salesOrderService;
            this._salesOrderDetailService = _salesOrderDetailService;

            this.salesOrder = new dbentities.SalesOrder();
            this.salesOrderDetail = new dbentities.SalesOrderDetail();
        }
    }

    public partial class SalesOrderService : IOrderService
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
                long productId = 0;

                #region Product
                if (!orderTransactionRequest.IsOrderQueue)
                {
                    var productDetailRequest = new ProductDetailRequest()
                    {
                        ProductId = orderDetail.ProductId,
                        ProductCode = orderDetail.ProductCode,
                        ProductDescription = orderDetail.ProductDescription,
                        Quantity = orderDetail.Quantity,
                        IsActive = orderDetail.IsActive,
                        CreatedBy = orderDetail.CreatedBy,
                        CreatedTime = DateTime.Now,
                        ModifiedBy = null,
                        ModifiedTime = null
                    };
                    productDetailResult = _productService.GetAllProductDetails().Where(p => p.ProductId == orderDetail.ProductId).FirstOrDefault();
                    productDetailRequest = new ProductDetailRequest()
                    {
                        ProductId = productDetailResult.ProductId,
                        ProductCode = productDetailResult.ProductCode,
                        ProductDescription = productDetailResult.ProductDescription,
                        Quantity = (productDetailResult.Quantity - orderDetail.Quantity),
                        IsActive = productDetailResult.IsActive,
                        CreatedBy = productDetailResult.CreatedBy,
                        CreatedTime = productDetailResult.CreatedTime,
                        ModifiedBy = orderDetail.CreatedBy,
                        ModifiedTime = DateTime.Now
                    };
                    _productService.UpdateDetails(productDetailRequest);
                    productId = productDetailResult.ProductId;

                    if (productId <= 0)
                    {
                        return successReturn = 2;
                    }
                }
                #endregion

                #region Product Log
                if (!orderTransactionRequest.IsOrderQueue)
                {
                    var productHistoryDetailRequest = new ProductHistoryDetailRequest()
                    {
                        ProductHistoryId = 0,
                        ProductId = productId,
                        QuantityAmmend = orderDetail.Quantity,
                        QuantityPrevious = productDetailResult.Quantity,
                        QuantityCurrent = orderDetail.Quantity,
                        OrderTransactionTypeId = LookupKey.OrderTransactionType.SalesOrderId,
                        OrderRemarks = string.Empty,
                        CreatedBy = orderDetail.CreatedBy,
                        CreatedTime = DateTime.Now,
                        ModifiedBy = null,
                        ModifiedTime = null
                    };

                    var productHistoryId = _productService.SaveProductHistory(productHistoryDetailRequest);

                    if (productHistoryId <= 0)
                    {
                        return successReturn = 3;
                    }
                }
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
