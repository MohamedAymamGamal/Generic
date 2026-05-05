using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entities.CustomersBasket
{
    public class CustomersBasket
    {
        public CustomersBasket()
        {

        }
        public CustomersBasket(string id)
        {
            Id = id;
        }
        public string Id { get; set; } = string.Empty; //key

        public List<BasketItem> basketItems { get; set; } = new List<BasketItem>(); //value

    }
}
