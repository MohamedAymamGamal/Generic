using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public class ProductRepositry : GenericRepositry<Product>, IProductRepoistry
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly IImageMangamentService imageMangamentService;
        public ProductRepositry(ApplicationDbContext context, IMapper mapper, IImageMangamentService imageMangamentService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageMangamentService = imageMangamentService;
        }

        public async Task<bool> AddAsync(AddProductDto addProductDto)
        {
            if (addProductDto == null) return false;
            var product = mapper.Map<Product>(addProductDto);
            
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            var  ImagePath = imageMangamentService.SaveImage(addProductDto.Photo);
            return true;

        }
    }
}
