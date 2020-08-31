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
    public partial class UnitService
    {
        IInventoryIORepository<dbentities.Unit> _unitService;

        private dbentities.Unit units;

        public UnitService(IInventoryIORepository<dbentities.Unit> unitService)
        {
            this._unitService = unitService;

            this.units = new dbentities.Unit();
        }
    }

    public partial class UnitService : IUnitService
    {
        public IQueryable<UnitDetail> GetAllUnitDetails()
        {
            var result = from det in this._unitService.GetAll()
                         select new UnitDetail()
                         {
                             UnitId = det.UnitID,
                             UnitDescription = det.UnitDescription
                         };

            return result;
        }
    }
}
