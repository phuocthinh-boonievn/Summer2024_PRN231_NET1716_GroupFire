﻿using Business_Layer.Services;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Data_Layer.ResourceModel.ViewModel;
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
        public async Task<IActionResult> CreateZaloPayment([FromBody] OrderPaymentVM orderCreateVM)
        {
            var paymentUrl = await _paymentZaloService.CreatePaymentRequestAsync(orderCreateVM);
            return Ok(new { url = paymentUrl });
        }
    }
}
