using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Order;
using Ecom.Core.Service;
using Ecom.infrastructure.Reposities.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.Api.Controllers.v1
{
    [ApiController]
    [Route("api/order")]
    [Authorize]
    public class OrderController : ControllerBase
    {

        private readonly IOrder _orderService;
        public OrderController(IOrder orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create-order")]
        public async Task<ActionResult> create(OrderDto orderDTO)
        {
            try {
                var email = User.FindFirst("email_verified")?.Value == "true"
                ? User.FindFirst(ClaimTypes.Email)?.Value
                 : null;

                if (email is null) return Unauthorized("Email not verified.");
                Order order = await _orderService.CreateOrdersAsync(orderDTO, email);
                return Ok(new ResponseAPI(201, "order has been created"));
                

            }
            catch (Exception ex) {

                return BadRequest(new ResponseAPI(400, "there is no order created"));

            }

        }

    }
}
