using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using Business.InventoryIO.Core.Dto;

namespace Business.InventoryIO.Core.Interface
{
    public interface ISupplierService
    {
        IQueryable<SupplierDetail> GetAllSupplierDetails();
        long SaveSupplierDetails(SupplierDetailRequest request);
        bool UpdateSupplierDetails(SupplierDetailRequest request);
    }
}
