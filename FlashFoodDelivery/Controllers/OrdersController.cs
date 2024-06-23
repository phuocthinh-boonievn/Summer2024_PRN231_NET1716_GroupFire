using Business_Layer.Services;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllOrder()
        {
            var result = await _orderService.GetOrdersAsync();
            return Ok(result);
        }

        [HttpGet("{userId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllOrderByUserID(Guid userId)
        {
            var result = await _orderService.GetOrderByUserIDAsync(userId);
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewOrderByID(Guid id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateVM createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var c = await _orderService.CreateOrderAsync(createDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Checkout([FromBody] OrderCreateVM orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest();
            }
            var c = await _orderService.CheckoutAsync(orderDto, orderDto.OrderDetails);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdateVM updateDto)
        {
            var c = await _orderService.UpdateOrderAsync(id, updateDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            var c = await _orderService.CancelOrderAsync(id);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }
}
