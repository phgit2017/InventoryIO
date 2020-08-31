using System;
using System.Collections.Generic;
using System.Text;

using Business.InventoryIO.Core.Dto;
using DataAccess.Database.InventoryIO;
using dbentities = DataAccess.Database.InventoryIO;

namespace Business.InventoryIO.Core.Extensions
{
    public static class EntityMapper
    {
        public static dbentities.Supplier DtoToEntity(this SupplierDetailRequest request)
        {
            dbentities.Supplier entity = null;

            if (request != null)
            {
                entity = new dbentities.Supplier
                {
                    SupplierID = request.SupplierId,
                    SupplierCode = request.SupplierCode,
                    SupplierName = request.SupplierName,
                    IsActive = request.IsActive,
                    CreatedBy = request.CreatedBy,
                    CreatedTime = request.CreatedTime,
                    ModifiedBy = request.ModifiedBy,
                    ModifiedTime = request.ModifiedTime
                };
            }

            return entity;
        }

        public static dbentities.Customer DtoToEntity(this CustomerDetailRequest request)
        {
            dbentities.Customer entity = null;

            if (request != null)
            {
                entity = new dbentities.Customer
                {
                    CustomerID = request.CustomerId,
                    CustomerCode = request.CustomerCode,
                    Name = request.Name,
                    Address = request.Address,
                    IsActive = request.IsActive,
                    CreatedBy = request.CreatedBy,
                    CreatedTime = request.CreatedTime,
                    ModifiedBy = request.ModifiedBy,
                    ModifiedTime = request.ModifiedTime
                };
            }

            return entity;
        }


        public static dbentities.CustomerPrice DtoToEntity(this CustomerPriceDetailRequest request)
        {
            dbentities.CustomerPrice entity = null;

            if (request != null)
            {
                entity = new dbentities.CustomerPrice
                {
                    CustomerID = request.CustomerId,
                    ProductID = request.ProductId,
                    ProductPrice = request.ProductPrice,
                    CreatedBy = request.CreatedBy,
                    CreatedTime = request.CreatedTime,
                };
            }

            return entity;
        }

        public static dbentities.UserDetail DtoToEntity(this UserDetailRequest request)
        {
            dbentities.UserDetail entity = null;

            if (request != null)
            {
                entity = new dbentities.UserDetail
                {
                    UserID = request.UserId,
                    UserName = request.UserName,
                    Password = request.Password,
                    UserRoleID = request.UserRoleId,
                    IsActive = request.IsActive,
                    CreatedBy = request.CreatedBy,
                    CreatedTime = request.CreatedTime,
                    ModifiedBy = request.ModifiedBy,
                    ModifiedTime = request.ModifiedTime
                };
            }

            return entity;
        }
    }
}
