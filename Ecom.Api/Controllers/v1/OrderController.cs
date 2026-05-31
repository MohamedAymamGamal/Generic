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

        [HttpPost]
        public async Task<IActionResult> create(OrderDto orderDTO)
        {
            try {
                var email = User.FindFirst("email_verified")?.Value == "true"
                ? User.FindFirst(ClaimTypes.Email)?.Value
                 : null;

                if (email is null) return Unauthorized("Email not verified.");
                Order order = await _orderService.CreateOrdersAsync(orderDTO, email);
                return Ok(new ResponseAPI(201, "order has been created"));
                

            }
            catch (Exception) {

                return BadRequest(new ResponseAPI(400, "there is no order created"));

            }

        }



        [HttpGet]

        public async Task<IActionResult> getAll() 
        {
            try {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var order = await _orderService.GetAllOrdersForUserAsync(email);
                if (order == null) return null;
                return Ok( order);
            

            }

            catch (Exception) {
                return BadRequest(new ResponseAPI(400, "there is no orders "));

            }

        
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (email is null) return NotFound();
                var order = await _orderService.GetOrderByIdAsync(id, email);
                return Ok(order);
                    
            } catch (Exception) {
                return BadRequest(new ResponseAPI(400, "there is no order "));

            }


        }

        [HttpGet("get-delivery")]
        public async Task<IActionResult> getDelivery()
        {
           try
            {
                var result = await _orderService.GetDeliveryMethodAsync();
                return Ok(result);
            }catch
            {
                return BadRequest(new ResponseAPI(400, "there is Delivery "));

            }
        }
    }
}
