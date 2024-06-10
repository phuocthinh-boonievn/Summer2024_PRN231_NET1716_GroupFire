using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class PaymentSerivce
    {
        public async Task<PaymentIntent> CreatePaymentIntentAsync(decimal amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100),
                Currency = currency,
            };

            var service = new PaymentIntentService();
            return await service.CreateAsync(options);
        }
        //public async Task<IActionResult> ProcessPayment(Order order)
        //{
        //    try
        //    {
        //        var stripeOptions = new ChargeCreateOptions
        //        {
        //            Amount = Convert.ToInt3  // Convert order total to cents
        //          (order.Total * 100),
        //            Currency = "usd", // Update with your currency code
        //            Source = order.StripeToken,
        //            Description = $"Order #{order.Id}"
        //        };

        //        var stripeService = new StripeService();
        //        var charge = await stripeService.ChargeAsync(stripeOptions);

        //        if (charge.Paid)
        //        {
        //            // Payment successful, update order status and return confirmation
        //            return Ok("Payment successful!");
        //        }
        //        else
        //        {
        //            // Payment failed, return error message
        //            return BadRequest(charge.FailureMessage);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle other exceptions
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
