using AutoMapper;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public class RatingRepositry : GenericRepositry<ProductRating>, IRatingRepositry
    {
        private readonly ApplicationDbContext _context;

        public RatingRepositry(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

     
        public async Task<double> GetAverageStarsAsync(int productId)
        {
            var hasRatings = await _context.ProductRatings
                .AnyAsync(r => r.ProductId == productId);
        
            return hasRatings
                ? Math.Round(await _context.ProductRatings
                    .Where(r => r.ProductId == productId)
                    .AverageAsync(r => (double)r.Stars), 1)
                : 0;
        }

      
        public async Task<ProductRating?> GetByIdWithProductAsync(int ratingId) =>
            await _context.ProductRatings
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == ratingId);
        public async Task<IEnumerable<ProductRating>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductRatings
              .Where(r => r.ProductId == productId)
              .OrderByDescending(r => r.CreatedAt).ToListAsync();
        }
        public async Task<int> GetTotalRatingsAsync(int productId) =>
              await _context.ProductRatings
                  .CountAsync(r => r.ProductId == productId);

     

       

    }
}
