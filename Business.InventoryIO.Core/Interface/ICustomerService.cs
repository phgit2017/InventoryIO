using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using Business.InventoryIO.Core.Dto;

namespace Business.InventoryIO.Core.Interface
{
    public interface ICustomerService
    {
        IQueryable<CustomerDetail> GetAllCustomerDetails();
        IQueryable<CustomerPricingDetail> GetAllCustomerPricingDetails();
        long SaveCustomerPrice(CustomerPriceDetailRequest request);
        bool UpdateCustomerPriceDetails(CustomerPriceDetailRequest request);
        long SaveCustomerDetails(CustomerDetailRequest request);
        bool UpdateCustomerDetails(CustomerDetailRequest request);

        bool DeleteCustomerPriceDetails(CustomerPriceDetailRequest request);
    }
}
