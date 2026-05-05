using AutoMapper;
using Ecom.Api.Sharing;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams)

        {
            var query = context.Products
                .Include(m => m.Category)
                .Include(m => m.Photos)
                .AsNoTracking();



            //filtering by word
            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWords = productParams.Search.Split(' ');
                query = query.Where(m => searchWords.All(word =>

                m.Name.ToLower().Contains(word.ToLower()) ||
                m.Description.ToLower().Contains(word.ToLower())

                ));
            }



            //filtering by category Id
            if (productParams.CategoryId.HasValue)
                query = query.Where(m => m.CategoryId == productParams.CategoryId);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                query = productParams.Sort switch
                {
                    "PriceAce" => query.OrderBy(m => m.NewPrice),
                    "PriceDce" => query.OrderByDescending(m => m.NewPrice),
                    _ => query.OrderBy(m => m.Name),
                };
            }

            productParams.TotalCount = query.Count();

            query = query.Skip((productParams.pageSize) * (productParams.PageNumber - 1)).Take(productParams.pageSize);


            var result = mapper.Map<List<ProductDto>>(query);

            return result;
        }
        public async Task<bool> AddAsync(AddProductDto addProductDto)
        {
            if (addProductDto == null) return false;
            var product = mapper.Map<Product>(addProductDto);
            
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            var  ImagePath = await imageMangamentService.AddImageAsync(addProductDto.Photo, addProductDto.Name);

            var photo = ImagePath.Select(path => new Photo
            {

                ImageName = path,
                ProductId = product.Id,

            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;

        }

        

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            if(updateProductDto is null) return false;

            var findProduct = await context
                .Products.Include(m => m.Category)
                .Include(m=>m.Category)
                .FirstOrDefaultAsync(m=> m.Id== updateProductDto.Id);

            if(findProduct is null) return false;

            mapper.Map(updateProductDto, findProduct);

            var FindPhoto = await context.
                Photos.Where(m => m.ProductId == updateProductDto.Id)
                .ToListAsync();  

            foreach (var item in FindPhoto)
            {
                imageMangamentService.DeleteImageAsync(item.ImageName);
            }
             context.Photos.RemoveRange(FindPhoto);

            var ImagePath = await imageMangamentService
                .AddImageAsync(updateProductDto.Photo, updateProductDto.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = findProduct.Id,
            }).ToList();
             await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Product id)
        {
            var photo = await context.Photos.Where(m => m.ProductId == id.Id).ToListAsync();
            foreach (var item in photo)
            {
                imageMangamentService.DeleteImageAsync(item.ImageName);
            }
            context.Products.Remove(id);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
