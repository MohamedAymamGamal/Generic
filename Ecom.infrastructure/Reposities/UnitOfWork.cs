using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            ProductRepository = new ProductRepositry(_context);

            CategoryRepository = new CategoryRepositry(_context);

            PhotoRepository = new PhotoRepoistory(_context);

        }

        public IProductRepoistry ProductRepository { get; }

        public ICategoryRepositry CategoryRepository { get; }
        public IPhotoRepositry PhotoRepository { get; }
    }
}
