using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;
using Ecom.Core.DTO;

namespace Ecom.Core.Interfaces
{
    public interface ICategoryRepositry: IGenericRepositry<Category>
    {
        Task<CategoryWithProductsDTO?> GetCategoryWithProductsAsync(int id);

    }
}
