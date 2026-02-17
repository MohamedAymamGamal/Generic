using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepoistry: IGenericRepositry<Product>
    {
        Task<bool> AddAsync(AddProductDto addProductDto);
    }
}
