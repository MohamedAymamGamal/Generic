using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Ecom.Core.DTO;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Reposities
{
    public class CategoryRepositry : GenericRepositry<Category>, ICategoryRepositry
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepositry(ApplicationDbContext context,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryWithProductsDTO> GetCategoryWithProductsAsync(int id)
        {
         

            var category = await _context.Categories.
                Include(x => x.Products).
                ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (category is null) return null;

            return _mapper.Map<CategoryWithProductsDTO>(category);
        }
    }
    
}
