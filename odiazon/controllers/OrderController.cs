using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using odiazon.dtos.d_orderDto;
using odiazon.services.s_order;

namespace odiazon.controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetUserOrders()
        {
            return Ok(await _orderService.GetUserOrders());
        }

        [HttpGet("admin/all")]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOrder(AddOrderParametersDto orderParameters)
        {
            return Ok(await _orderService.AddNewOrder(orderParameters));
        }
    }
}
