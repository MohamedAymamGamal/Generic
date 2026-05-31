using Ecom.Core.DTO;
using Ecom.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Service
{
    public interface IOrder
    {
        Task<Order> CreateOrdersAsync(OrderDto orderDTO, string BuyerEmail);
        Task<IReadOnlyList<OrderToReturnDTO>> GetAllOrdersForUserAsync(string BuyerEmail);
        Task<Order?> GetOrderByIdAsync(int Id, string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();

    }
}
