using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Api.Sharing;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.v1
{
    [Route("api/v1/products")]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet]
        public async Task<IActionResult> get([FromQuery] ProductParams productParams)
        {
            try
            {
                var products = await work.ProductRepository.GetAllAsync(productParams);

                return Ok(new Pagination<ProductDto>(productParams.PageNumber, productParams.MaxPageSize, products.TotalCount, products.products));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));

            }

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x => x.Category, x => x.Photos);
                var result = mapper.Map<ProductDto>(product);
                if (product == null)
                {
                    return BadRequest(new ResponseAPI(400, $"not found product id={id}"));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));

            }
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromForm] AddProductDto addProductDto)
        {
            try
            {
                await work.ProductRepository.AddAsync(addProductDto);
                return Ok(new ResponseAPI(201, "Product added successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                await work.ProductRepository.UpdateAsync(updateProductDto);
                return Ok(new ResponseAPI(200, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x => x.Photos, x => x.Category);
                await work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));

            }
        }
    }

}
