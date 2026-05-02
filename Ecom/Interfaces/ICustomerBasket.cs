using Ecom.Core.Entities.CustomersBasket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface ICustomerBasketRepository
    {
        Task<CustomersBasket?> GetBasketAsync(string id);

        Task<CustomersBasket?> UpdateBasketAsync(CustomersBasket basket);

        Task<bool> DeleteBasketAsync(string id);

    }
}
