using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageMangamentService _imageMangamentService;
        private readonly IMapper _mapper;
        public UnitOfWork(ApplicationDbContext context, IMapper mapper , IImageMangamentService imageMangamentService)
        {
            _context = context;
            _mapper = mapper;
            _imageMangamentService = imageMangamentService;
            ProductRepository = new ProductRepositry(_context,_mapper ,_imageMangamentService);

            CategoryRepository = new CategoryRepositry(_context);

            PhotoRepository = new PhotoRepoistory(_context);

            
        }

        public IProductRepoistry ProductRepository { get; }

        public ICategoryRepositry CategoryRepository { get; }
        public IPhotoRepositry PhotoRepository { get; }
    }
}
