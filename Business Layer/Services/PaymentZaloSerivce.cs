using Business_Layer.Utils;
using Data_Layer.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class PaymentZaloSerivce : IPaymentZaloService
    {
        private readonly ZaloPaySettings _settings;

        public PaymentZaloSerivce(IOptions<ZaloPaySettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<string> CreatePaymentRequestAsync(Order order)
        {
            var appTransId = DateTime.Now.ToString("yyyyMMddHHmmss");
            var amount = order.TotalPrice ?? 0;
            var orderInfo = GenerateOrderInfo(order);

            var requestData = new
            {
                app_id = _settings.AppId,
                app_user = order.MemberId ?? "anonymous",
                app_time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                amount = (long)(amount * 100),
                app_trans_id = appTransId,
                embed_data = "{}",
                item = JsonConvert.SerializeObject(order.OrderDetails.Select(od => new { od.MenuFoodItem.FoodName, od.Quantity, od.UnitPrice })),
                description = orderInfo,
                bank_code = "zalopayapp",
                mac = GenerateMac(appTransId, amount)
            };

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(_settings.Endpoint, new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json"));
            var responseData = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseData);
            return result.order_url;
        }

        private string GenerateOrderInfo(Order order)
        {
            var orderDetails = order.OrderDetails.Select(od => $"{od.MenuFoodItem.FoodName} (x{od.Quantity})").ToList();
            var orderInfo = $"Order {order.OrderId}: {string.Join(", ", orderDetails)}";
            return orderInfo;
        }

        private string GenerateMac(string appTransId, decimal amount)
        {
            var rawData = $"{_settings.AppId}|{appTransId}|{(long)(amount * 100)}|{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}|{_settings.Key1}";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_settings.Key2)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }

}
