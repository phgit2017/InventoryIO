﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using Business.InventoryIO.Core.Dto;

namespace Business.InventoryIO.Core.Interface
{
    public interface IProductService
    {
        IQueryable<ProductDetail> GetAllProductDetails();
        long SaveProduct(ProductDetailRequest request);
        bool UpdateDetails(ProductDetailRequest request);
        long SaveProductHistory(ProductHistoryDetailRequest request);
    }
}
