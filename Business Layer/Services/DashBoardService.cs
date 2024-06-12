using Business_Layer.Repositories;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.DashboardViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class DashBoardService : IDashboardService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUserRepository _userRepository;

        public DashBoardService(IOrderRepository order, IOrderDetailRepository orderDetail, IUserRepository userRepository) 
        {
            _orderRepository = order;
            _orderDetailRepository = orderDetail;
            _userRepository = userRepository;
        }

        public async Task<decimal> GetTotalSalesByMonth(int month, int year)
        {
            var orders = await _orderRepository.GetConfirmedOrders();
            var totalSalesByMonth = orders.Where(order => order.OrderDate.Month == month
                                            && order.OrderDate.Year == year)
                                    .Sum(order => order.TotalPrice);
            return totalSalesByMonth.GetValueOrDefault();
        }
        public async Task<decimal> GetTotalSalesByYear(int year)
        {
            var orders = await _orderRepository.GetAllAsync();
            var totalSalesByYear = orders.Where(order => order.OrderDate.Year == year)
                                          .Sum(order => order.TotalPrice);
            return totalSalesByYear.GetValueOrDefault();
        }
        public async Task<decimal> GetTotalRevenue()
        {
            var orders = await _orderRepository.GetConfirmedOrders();
            decimal? totalRevenue = orders.Sum(order => order.TotalPrice);
            return totalRevenue.GetValueOrDefault();
        }
        public async Task<decimal> GetTotalSalesByWeek(int year, int weekNumber)
        {
            var orders = await _orderRepository.GetConfirmedOrders();
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(jan1, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var weekNum = weekNumber;

            // If the first week of the year starts before the first Monday, adjust week number
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            DateTime weekStart = firstMonday.AddDays(weekNum * 7);
            DateTime weekEnd = weekStart.AddDays(7).AddTicks(-1); // End of the week

            var weeklyTotalSales = orders.Where(o => o.OrderDate >= weekStart
            &&  o.OrderDate <= weekEnd).Sum(o => o.TotalPrice);
            return weeklyTotalSales.GetValueOrDefault();
        }
        public async Task<List<LoyalCustomer>> GetTopLoyalCustomer()
        {
            var loyalCustomeList = await _userRepository.GetTopFiveCustomerAsync();
            return loyalCustomeList;
        }
    }
}
