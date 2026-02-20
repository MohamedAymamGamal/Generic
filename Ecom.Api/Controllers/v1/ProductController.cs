using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Api.Sharing;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.v1
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> get([FromQuery] ProductParams productParams )
        {
            try
            {
                var products = await work.ProductRepository.GetAllAsync(productParams);
               
              
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));

            }

        }

        [HttpGet("get-by-id/{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id,x => x.Category, x=> x.Photos);
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
        [HttpPost("add-product")]

        public async Task<IActionResult> Add(AddProductDto addProductDto)
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


        [HttpPut("update-product")]

        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
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
        [HttpDelete("delete-product/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x=>x.Photos , x=>x.Category);
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
