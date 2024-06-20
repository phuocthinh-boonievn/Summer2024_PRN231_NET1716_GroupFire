using Business_Layer.Services;
using Data_Layer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly IPaymentZaloService _paymentZaloService;

        public PaymentsController(IPaymentZaloService paymentZaloService)
        {
            _paymentZaloService = paymentZaloService;
        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateZaloPayment([FromBody] Order order)
        {
            var payUrl = await _paymentZaloService.CreatePaymentRequestAsync(order);
            return Ok(new { payUrl });
        }
    }
}
