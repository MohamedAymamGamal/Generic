using AutoMapper;
using Ecom.Api.Helper;
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
        public async Task<IActionResult> get()
        {
            try
            {
                var products = await work.ProductRepository.GetAllAsync( x =>x.Category, x => x.Photos);
                if (products == null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return BadRequest(new ResponseAPI(400, $"not found product id={id}"));
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> AddProduct()
        {
            try
            {
                return Ok(new ResponseAPI(201, "Product added successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        public async Task<IActionResult> UpdateProduct()
        {
            try
            {
                return Ok(new ResponseAPI(200, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
