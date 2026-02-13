using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public class CategoryRepositry : GenericRepositry<Category>, ICategoryRepositry
    {
        public CategoryRepositry(ApplicationDbContext context) : base(context)
        {
        }

        public Task AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Category>> GetAllAsync(params Expression<Func<Category, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id, params Expression<Func<Category, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Category>> IGenericRepositry<Category>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Category> IGenericRepositry<Category>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
