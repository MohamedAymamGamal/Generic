using Ecom.Api.Sharing;
using Ecom.Core.Entities.Product;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IRatingRepositry : IGenericRepositry<ProductRating>
    {
        Task<IEnumerable<ProductRating>> GetByProductIdAsync(int productId);
        Task<ProductRating?> GetByIdWithProductAsync(int ratingId);
        Task<double> GetAverageStarsAsync(int productId);
        Task<int> GetTotalRatingsAsync(int productId);
    }
}
