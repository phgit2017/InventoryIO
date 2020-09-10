using Business.InventoryIO.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.InventoryIO.Core.Interface
{
    public interface IOrderService
    {
        long UpdateOrderTransacion(OrderTransactionRequest orderTransactionRequest,List<OrderTransactionDetailRequest> orderTransactionDetailRequest);
    }
}
