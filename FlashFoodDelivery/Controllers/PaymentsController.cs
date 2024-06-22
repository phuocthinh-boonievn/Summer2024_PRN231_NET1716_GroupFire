﻿using Business_Layer.Services;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Data_Layer.ResourceModel.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Business_Layer.Repositories;
using Business_Layer.Services.VNPay;
using Microsoft.Extensions.Options;
using System.Web;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentZaloService _paymentZaloService;
        private readonly VNPaySettings _vnPaySettings;
        private readonly IOrderRepository _orderRepository;
        public PaymentsController(IPaymentZaloService paymentZaloService, IOptions<VNPaySettings> vnPaySettings, IOrderRepository orderRepository)
        {
            _paymentZaloService = paymentZaloService;
            _vnPaySettings = vnPaySettings.Value;
            _orderRepository = orderRepository;
        }

        //[HttpPost]
        //[EnableCors("CorsPolicy")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> CreateZaloPayment([FromBody] OrderPaymentVM orderCreateVM)
        //{
        //    var paymentUrl = await _paymentZaloService.CreatePaymentRequestAsync(orderCreateVM);
        //    return Ok(new { url = paymentUrl });
        //}

        //[HttpPost]
        //[ProducesResponseType(typeof(BaseResultWithData<PaymentLinkDtos>), 200)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Create([FromBody] CreatePayment request)
        //{
        //    var response = new BaseResultWithData<PaymentLinkDtos>();
        //    response = await mediator.Send(request);
        //    return Ok(response);
        //}
        


        [HttpGet("payment/{amount}/{infor}")]
        /**
         * param amount: số tiền
         * param info: orderId được tạo trong bảng order
         */
        //[EnableCors("CorsPolicy")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult VnPaymentRequest(string amount, string infor)
        {
            // find order in table order and checking exits

            string orderinfor = DateTime.Now.Ticks.ToString();
            string hostName = System.Net.Dns.GetHostName();
            string clientIPAddress = System.Net.Dns.GetHostAddresses(hostName).GetValue(0).ToString();
            VNPayHelper pay = new VNPayHelper();
            amount += "00";
            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode",
                _vnPaySettings
                    .TmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount",
                amount); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            // pay.AddRequestData("vnp_BankCode",
            //     "");
            //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate",
                DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", clientIPAddress); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", infor); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType",
                "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl",
                _vnPaySettings.ReturnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", orderinfor); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(_vnPaySettings.Url, _vnPaySettings.HashSecret);
            return Redirect(paymentUrl);
        }

        [HttpGet("PaymentConfirm")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PaymentConfirm()
        {
            if (Request.QueryString.HasValue)
            {
                //lấy toàn bộ dữ liệu trả về
                var queryString = Request.QueryString.Value;
                var json = HttpUtility.ParseQueryString(queryString);

                long orderId = Convert.ToInt64(json["vnp_TxnRef"]); //mã hóa đơn
                string orderInfor = json["vnp_OrderInfo"].ToString(); //Thông tin giao dịch
                long vnpayTranId = Convert.ToInt64(json["vnp_TransactionNo"]); //mã giao dịch tại hệ thống VNPAY
                string
                    vnp_ResponseCode =
                        json["vnp_ResponseCode"]
                            .ToString(); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = json["vnp_SecureHash"].ToString(); //hash của dữ liệu trả về
                var pos = Request.QueryString.Value.IndexOf("&vnp_SecureHash");

                bool checkSignature = ValidateSignature(Request.QueryString.Value.Substring(1, pos - 1), vnp_SecureHash,
                    _vnPaySettings.HashSecret); //check chữ ký đúng hay không?
                if (checkSignature && _vnPaySettings.TmnCode == json["vnp_TmnCode"].ToString())
                {
                    // Sử dụng orderInfo chứ k phải orderId vì nó là id của vnpay
                    // Demo xử lý Order
                    // Order order = await _orderRepository.GetByOrderIdAsync((int)orderInfor);
                    if (vnp_ResponseCode == "00")
                    {
                        // Payment successful
                        // var transaction = await _transactionRepository.GetByIdAsync((int)orderInfor);
                        // transaction.Status = true;
                        // await _transactionRepository.UpdateAsync(transaction);
                        // order.Status = 1; // assuming '1' is the status code for successful payment
                        // await _orderRepository.UpdateOrderAsync(order);

                        return StatusCode(200, "Ok");
                        // return Redirect("localhosst");
                    }
                    else
                    {
                        // Payment failed
                        // order.Status = 3; // assuming '3' is the status code for failed payment
                        // await _orderRepository.UpdateOrderAsync(order);
                        return StatusCode(402, $"Payment Required. Error Code: {vnp_ResponseCode}");
                    }
                }
                else
                {
                    return Redirect("đường dẫn nếu phản hồi ko hợp lệ");
                }
            }

            //phản hồi không hợp lệ
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }

        private bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = VNPayHelper.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

