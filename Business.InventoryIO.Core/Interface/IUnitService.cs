using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using Business.InventoryIO.Core.Dto;

namespace Business.InventoryIO.Core.Interface
{
    public interface IUnitService
    {
        IQueryable<UnitDetail> GetAllUnitDetails();
    }
}
