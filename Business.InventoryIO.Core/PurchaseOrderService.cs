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
    public partial class PurchaseOrderService
    {
        IProductService _productService;
        IInventoryIORepository<dbentities.PurchaseOrder> _purchaseOrderService;
        IInventoryIORepository<dbentities.PurchaseOrderDetail> _purchaseOrderDetailService;

        private dbentities.PurchaseOrder purchaseOrder;
        private dbentities.PurchaseOrderDetail purchaseOrderDetail;

        public PurchaseOrderService(
            IProductService productService,
            IInventoryIORepository<dbentities.PurchaseOrder> purchaseOrderService,
            IInventoryIORepository<dbentities.PurchaseOrderDetail> purchaseOrderDetailService)
        {
            this._productService = productService;
            this._purchaseOrderService = purchaseOrderService;
            this._purchaseOrderDetailService = purchaseOrderDetailService;

            this.purchaseOrder = new dbentities.PurchaseOrder();
            this.purchaseOrderDetail = new dbentities.PurchaseOrderDetail();
        }
    }

    public partial class PurchaseOrderService : IOrderService
    {
        public long UpdateOrderTransacion(
            OrderTransactionRequest orderTransactionRequest,
            List<OrderTransactionDetailRequest> orderTransactionDetailRequest)
        {
            decimal totalAmount = 0.00m, totalQuantity = 0.00m;
            long purchaseOrderId = 0, successReturn = 0;

            ProductDetail productDetailResult = new ProductDetail();

            foreach (var orderDetail in orderTransactionDetailRequest)
            {
                totalAmount += totalAmount + orderDetail.UnitPrice;
                totalQuantity += totalQuantity + orderDetail.Quantity;
            }

            orderTransactionRequest.TotalQuantity = totalQuantity;
            orderTransactionRequest.TotalAmount = (totalAmount * totalQuantity);

            #region Validate if Product Code is existing

            foreach (var orderDetail in orderTransactionDetailRequest)
            {
                if (orderDetail.ProductId == 0)
                {
                    var codeProductDetailResult = _productService.GetAllProductDetails().Where(p => p.ProductCode == orderDetail.ProductCode
                                                                                                    && p.IsActive).FirstOrDefault();

                    #region Validate same product code
                    if (!codeProductDetailResult.IsNull())
                    {
                        return successReturn = -100;
                    }
                    #endregion
                }
                else
                {
                    var codeProductDetailResult = _productService.GetAllProductDetails().Where(p => p.ProductCode == orderDetail.ProductCode
                                                                                && p.IsActive
                                                                                && p.ProductId != orderDetail.ProductId).FirstOrDefault();

                    #region Validate same product code
                    if (!codeProductDetailResult.IsNull())
                    {
                        return successReturn = -100;
                    }
                    #endregion
                }

            }

            #endregion

            #region Purchase Order
            this.purchaseOrder = new dbentities.PurchaseOrder()
            {
                PurchaseOrderID = 0,
                OrderTypeID = LookupKey.OrderType.Single,
                TotalAmount = totalAmount,
                TotalQuantity = totalQuantity,
                CreatedBy = orderTransactionRequest.CreatedBy,
                CreatedTime = DateTime.Now,
                ModifiedBy = null,
                ModifiedTime = null
            };

            purchaseOrderId = _purchaseOrderService.Insert(this.purchaseOrder).PurchaseOrderID;

            if (purchaseOrderId <= 0)
            {
                return successReturn = 1;
            }
            #endregion

            foreach (var orderDetail in orderTransactionDetailRequest)
            {
                long productId = 0;

                #region Product
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

                if (orderDetail.ProductId == 0)
                {
                    productId = _productService.SaveProduct(productDetailRequest);
                }
                else
                {

                    productDetailResult = _productService.GetAllProductDetails().Where(p => p.ProductId == orderDetail.ProductId).FirstOrDefault();

                    productDetailRequest = new ProductDetailRequest()
                    {
                        ProductId = productDetailResult.ProductId,
                        ProductCode = productDetailResult.ProductCode,
                        ProductDescription = productDetailResult.ProductDescription,
                        Quantity = (productDetailResult.Quantity + orderDetail.Quantity),
                        IsActive = productDetailResult.IsActive,
                        CreatedBy = productDetailResult.CreatedBy,
                        CreatedTime = productDetailResult.CreatedTime,
                        ModifiedBy = orderDetail.CreatedBy,
                        ModifiedTime = DateTime.Now
                    };



                    _productService.UpdateDetails(productDetailRequest);
                    productId = productDetailResult.ProductId;
                }


                if (productId <= 0)
                {
                    return successReturn = 2;
                }
                #endregion

                #region Product Log
                var productHistoryDetailRequest = new ProductHistoryDetailRequest()
                {
                    ProductHistoryId = 0,
                    ProductId = productId,
                    QuantityAmmend = orderDetail.Quantity,
                    QuantityPrevious = orderDetail.ProductId == 0 ? orderDetail.Quantity : productDetailResult.Quantity,
                    QuantityCurrent = orderDetail.ProductId == 0 ? orderDetail.Quantity : productDetailResult.Quantity,
                    OrderTransactionTypeId = LookupKey.OrderTransactionType.PurchaseOrderId,
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
                #endregion

                #region Purchase Order Details
                this.purchaseOrderDetail = new dbentities.PurchaseOrderDetail()
                {
                    PurchaseOrderID = purchaseOrderId,
                    ProductID = productId,
                    Quantity = Convert.ToDecimal(orderDetail.Quantity),
                    SupplierID = orderDetail.SupplierId,
                    CreatedBy = orderDetail.CreatedBy,
                    CreatedTime = DateTime.Now,
                    ModifiedBy = null,
                    ModifiedTime = null
                };
                
                var purchaseOrderDetailId = _purchaseOrderDetailService.Insert(this.purchaseOrderDetail).PurchaseOrderID;

                if (purchaseOrderDetailId <= 0)
                {
                    return successReturn = 4;
                }
                #endregion
            }

            return successReturn;
        }
    }
}
