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

        private dbentities.Product product;

        public ProductService(
            IInventoryIORepository<dbentities.Product> productService)
        {
            this._productService = productService;

            this.product = new dbentities.Product();
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
    }
}
