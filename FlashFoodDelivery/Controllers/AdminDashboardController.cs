using Business_Layer.Services;
using Data_Layer.ResourceModel.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public AdminDashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("GetTotalRevenue")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalRevenue()
        {
            decimal totalRevenue = await _dashboardService.GetTotalRevenue();
            return Ok(totalRevenue);
        }
        [HttpGet("GetMonthlyRevenue")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMonthlyRevenue(int month, int year)
        {
            decimal monthlyRevenue = await _dashboardService.GetTotalSalesByMonth(month, year);
            return Ok(monthlyRevenue);
        }
        [HttpGet("GetWeeklyRevenue")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWeeklyRevenue(int year, int weekNumber)
        {
            decimal weeklyRevenue = await _dashboardService.GetTotalSalesByWeek(year, weekNumber);
            return Ok(weeklyRevenue);
        }
        [HttpGet("GetRevenueByYear")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRevenueByYear(int year)
        {
            decimal annualYearRevenue = await _dashboardService.GetTotalSalesByYear(year);
            return Ok(annualYearRevenue);
        }
        [HttpGet("GetTopFiveCustomer")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTopLoyalCustomer()
        {
            var loyalCustomers = await _dashboardService.GetTopLoyalCustomer();
            return Ok(loyalCustomers);
        }
    }
}
