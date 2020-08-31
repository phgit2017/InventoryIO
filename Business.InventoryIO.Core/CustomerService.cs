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
    public partial class CustomerService
    {
        IInventoryIORepository<dbentities.Customer> _customerService;
        IInventoryIORepository<dbentities.CustomerPrice> _customerPriceService;

        private dbentities.Customer customers;
        private dbentities.CustomerPrice customerPrices;

        public CustomerService(
            IInventoryIORepository<dbentities.Customer> customerService,
            IInventoryIORepository<dbentities.CustomerPrice> customerPriceService)
        {
            this._customerService = customerService;
            this._customerPriceService = customerPriceService;

            this.customers = new dbentities.Customer();
            this.customerPrices = new dbentities.CustomerPrice();
        }
    }

    public partial class CustomerService : ICustomerService
    {
        public IQueryable<CustomerDetail> GetAllCustomerDetails()
        {
            var result = from det in this._customerService.GetAll()
                         select new CustomerDetail()
                         {
                             CustomerId = det.CustomerID,
                             CustomerCode = det.CustomerCode,
                             Name = det.Name,
                             Address = det.Address,
                             IsActive = det.IsActive,

                             CreatedBy = det.CreatedBy,
                             CreatedTime = det.CreatedTime,
                             ModifiedBy = det.ModifiedBy,
                             ModifiedTime = det.ModifiedTime,
                         };
            
            return result;
        }

        public IQueryable<CustomerPricingDetail> GetAllCustomerPricingDetails()
        {
            var result = from det in this._customerPriceService.GetAll()
                         select new CustomerPricingDetail()
                         {
                             CustomerId = det.CustomerID,
                             CustomerCode = det.Customer.CustomerCode,
                             CustomerName = det.Customer.Name,
                             ProductId = det.Product.ProductID,
                             ProductCode = det.Product.ProductCode,
                             ProductDescription = det.Product.ProductDescription,
                             ProductExtension = det.Product.ProductExtension,
                             ProductPrice = det.ProductPrice,


                             CreatedBy = det.CreatedBy,
                             CreatedTime = det.CreatedTime,
                         };

            return result;
        }

        public long SaveCustomerPrice(CustomerPriceDetailRequest request)
        {
            this.customerPrices = request.DtoToEntity();
            var item = this._customerPriceService.Insert(this.customerPrices);
            if (item == null)
            {
                return 0;
            }

            return item.CustomerID;
        }

        public bool UpdateCustomerPriceDetails(CustomerPriceDetailRequest request)
        {
            this.customerPrices = request.DtoToEntity();

            var item = _customerPriceService.Update2(this.customerPrices);
            if (item == null)
            {
                return false;
            }

            return true;
        }

        public bool DeleteCustomerPriceDetails(CustomerPriceDetailRequest request)
        {
            bool result = _customerPriceService.Delete(m => m.CustomerID == request.CustomerId && m.ProductID == request.ProductId);
            
            return result;
        }

        public long SaveCustomerDetails(CustomerDetailRequest request)
        {
            request.CustomerId = 0;
            this.customers = request.DtoToEntity();
            var item = this._customerService.Insert(this.customers);
            if (item == null)
            {
                return 0;
            }

            return item.CustomerID;
        }

        public bool UpdateCustomerDetails(CustomerDetailRequest request)
        {
            this.customers = request.DtoToEntity();

            var item = _customerService.Update2(this.customers);
            if (item == null)
            {
                return false;
            }

            return true;
        }
    }
}
