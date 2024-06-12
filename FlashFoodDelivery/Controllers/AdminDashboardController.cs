using Business_Layer.Services;
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
        public async Task<decimal> GetTotalRevenue()
        {
            decimal totalRevenue = await _dashboardService.GetTotalRevenue();
            return totalRevenue;
        }
        [HttpGet("GetMonthlyRevenue")]
        public async Task<decimal> GetMonthlyRevenue(int month, int year)
        {
            decimal monthlyRevenue = await _dashboardService.GetTotalSalesByMonth(month, year);
            return monthlyRevenue;
        }
        [HttpGet("GetWeeklyRevenue")]
        public async Task<decimal> GetWeeklyRevenue(int year, int weekNumber)
        {
            decimal weeklyRevenue = await _dashboardService.GetTotalSalesByWeek(year, weekNumber);
            return weeklyRevenue;
        }
        [HttpGet("GetRevenueByYear")]
        public async Task<decimal> GetRevenueByYear(int year)
        {
            decimal annualYearRevenue = await _dashboardService.GetTotalSalesByYear(year);
            return annualYearRevenue;
        }
    }
}
