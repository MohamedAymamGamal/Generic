using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Order;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.infrastructure.Data;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities.Service
{
    public class OrderServices : IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderServices(IUnitOfWork unitOfWork, ApplicationDbContext context ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        public async Task<Order> CreateOrdersAsync(OrderDto orderDTO, string BuyerEmail)
        {
            var basket = await _unitOfWork.CustomerBasketRepository.GetBasketAsync(orderDTO.BasketId);

           List<OrderItems> orderItems = new List<OrderItems>();

            foreach (var item in basket.basketItems )
            {
                var Product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                var orderItem = new OrderItems
                (Product.Id, item.Image, Product.Name, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            var deliverMethod = await _context.DeliveryMethods.FirstOrDefaultAsync(m => m.Id == orderDTO.deliveryMethodId);
            var subTotal = orderItems.Sum(m=>m.Price*m.Quntity);

            var ship = _mapper.Map<ShippingAddress>(orderDTO.shipAddress);

            //var ExisitOrder = await _context.Orders.Where(m => m.PaymentIntentId == basket.PaymentIntentId).FirstOrDefaultAsync();

            //if(ExisitOrder != null)
            //{
            //    _context.Orders.Remove(ExisitOrder);

            //}
            var order = new Order(BuyerEmail, subTotal, ship, deliverMethod, orderItems);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> GetOrderByIdAsync(int Id, string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
