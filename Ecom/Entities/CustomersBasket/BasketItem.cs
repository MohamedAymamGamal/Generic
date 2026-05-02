using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entities.CustomersBasket
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }


        public int Quantity { get; set; } = 0;

        public decimal Price { get; set; }
        public string Category { get; set; }

    }
}
