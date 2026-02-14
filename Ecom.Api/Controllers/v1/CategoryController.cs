using Ecom.Core.Interfaces;
using Ecom.infrastructure.Reposities;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.v1
{

    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork work) : base(work)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var categories = await work.CategoryRepository.GetAllAsync();
                if (categories == null) { 
                    return BadRequest();
                }

                return Ok(categories);
            }
            catch (Exception ex) { 
                
            return StatusCode(500, ex.Message);

            }
        }

    }
}
