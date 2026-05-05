using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.Entities.CustomersBasket;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.v1
{
    [Route("api/v1/basket")]

    public class BasketController : BaseController
    {

        public BasketController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasketItem(string id)
        {
            var result = await work.CustomerBasketRepository.GetBasketAsync(id);
            if (result == null)
            {
                return Ok(new CustomersBasket());
            }

            return Ok(result);
        }
        [HttpPost()]

        public async Task<IActionResult> add(CustomersBasket basket)
        {
            var result = await work.CustomerBasketRepository.UpdateBasketAsync(basket);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
            var result = await work.CustomerBasketRepository.DeleteBasketAsync(id);
            return result ? Ok(new ResponseAPI(200, "item deleted successfully")) : BadRequest(new ResponseAPI(400, "item not found"));


        }

    }
}
