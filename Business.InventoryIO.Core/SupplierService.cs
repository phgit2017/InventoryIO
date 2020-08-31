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
    public partial class SupplierService
    {
        IInventoryIORepository<dbentities.Supplier> _supplierService;

        private dbentities.Supplier suppliers;

        public SupplierService(IInventoryIORepository<dbentities.Supplier> supplierService)
        {
            this._supplierService = supplierService;

            this.suppliers = new dbentities.Supplier();
        }
    }

    public partial class SupplierService : ISupplierService
    {
        public IQueryable<SupplierDetail> GetAllSupplierDetails()
        {
            var result = from det in this._supplierService.GetAll()
                         select new SupplierDetail()
                         {
                             SupplierId = det.SupplierID,
                             SupplierCode = det.SupplierCode,
                             SupplierName = det.SupplierName,
                             IsActive = det.IsActive,
                             CreatedBy = det.CreatedBy,
                             CreatedTime = det.CreatedTime,
                             ModifiedBy = det.ModifiedBy,
                             ModifiedTime = det.ModifiedTime,
                         };

            return result;
        }

        public long SaveSupplierDetails(SupplierDetailRequest request)
        {
            request.SupplierId = 0;
            this.suppliers = request.DtoToEntity();
            
            var codeSupplierDetailResult = GetAllSupplierDetails().Where(u => u.SupplierCode == request.SupplierCode
                                                                                && u.IsActive).FirstOrDefault();

            #region Validate same supplier code
            if (!codeSupplierDetailResult.IsNull())
            {
                return -100;
            }
            #endregion

            var item = this._supplierService.Insert(this.suppliers);
            if (item == null)
            {
                return 0;
            }

            return item.SupplierID;
        }

        public bool UpdateSupplierDetails(SupplierDetailRequest request)
        {
            this.suppliers = request.DtoToEntity();

            var item = _supplierService.Update2(this.suppliers);
            if (item == null)
            {
                return false;
            }

            return true;
        }
    }
}
