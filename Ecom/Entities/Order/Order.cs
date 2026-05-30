using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Ecom.Core.Entities.Order
{
    public class Order:BaseEntity<int>
    {
        public Order() { }
        public Order(string buyerEmail, decimal subTotal, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItems> orderItems)
        {
            BuyerEmail = buyerEmail;
            SubTotal = subTotal;
            this.shippingAddress = shippingAddress;
            this.deliveryMethod = deliveryMethod;
            this.orderItems = orderItems;

        }

        public string BuyerEmail { get; set; }

        public decimal SubTotal { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ShippingAddress shippingAddress {  get; set; }

        public DeliveryMethod deliveryMethod { get; set; }

        //public string PaymentIntentId { get; set; }

        public IReadOnlyList<OrderItems> orderItems { get; set; }

        public Status status { get; set; } = Status.Pending;

        public decimal GetTotal()
        {
            return SubTotal * deliveryMethod.Price;
        }
    }
}
