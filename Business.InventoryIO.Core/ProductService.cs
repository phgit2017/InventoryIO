using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using Business.InventoryIO.Core.Dto;
using Business.InventoryIO.Core.Extensions;
using Business.InventoryIO.Core.Interface;
using DataAccess.Repository.InventoryIO.Interface;
using dbentities = DataAccess.Database.InventoryIO;

namespace Business.InventoryIO.Core
{
    public partial class ProductService
    {
        IInventoryIORepository<dbentities.Product> _productService;
        IInventoryIORepository<dbentities.ProductHistory> _productHistoryService;

        private dbentities.Product product;
        private dbentities.ProductHistory productHistory;

        public ProductService(
            IInventoryIORepository<dbentities.Product> productService,
            IInventoryIORepository<dbentities.ProductHistory> productHistoryService)
        {
            this._productService = productService;
            this._productHistoryService = productHistoryService;

            this.product = new dbentities.Product();
            this.productHistory = new dbentities.ProductHistory();
        }
    }

    public partial class ProductService : IProductService
    {
        public IQueryable<ProductDetail> GetAllProductDetails()
        {
            var result = from det in _productService.GetAll()
                         select new ProductDetail()
                         {
                             ProductId = det.ProductID,
                             ProductCode = det.ProductCode,
                             ProductDescription = det.ProductDescription,
                             ProductExtension = det.ProductExtension,
                             Quantity = det.Quantity,
                             IsActive = det.IsActive,

                             CreatedBy = det.CreatedBy,
                             CreatedTime = det.CreatedTime,
                             ModifiedBy = det.ModifiedBy,
                             ModifiedTime = det.ModifiedTime,

                         };

            return result;
        }

        public long SaveProduct(ProductDetailRequest request)
        {
            this.product = request.DtoToEntity();
            var item = this._productService .Insert(this.product);
            if (item == null)
            {
                return 0;
            }

            return item.ProductID;
        }

        public bool UpdateDetails(ProductDetailRequest request)
        {
            this.product = request.DtoToEntity();
            var item = _productService.Update2(this.product);
            if (item == null)
            {
                return false;
            }

            return true;
        }

        public long SaveProductHistory(ProductHistoryDetailRequest request)
        {
            this.productHistory = request.DtoToEntity();
            var item = this._productHistoryService.Insert(this.productHistory);
            if (item == null)
            {
                return 0;
            }

            return item.ProductHistoryID;
        }
    }
}
