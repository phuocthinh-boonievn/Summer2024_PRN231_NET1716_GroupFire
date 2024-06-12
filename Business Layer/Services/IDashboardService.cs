using Data_Layer.ResourceModel.ViewModel.DashboardViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IDashboardService
    {
        public Task<decimal> GetTotalSalesByMonth(int month, int year);
        public Task<decimal> GetTotalSalesByYear(int year);
        public Task<decimal> GetTotalRevenue();
        public Task<decimal> GetTotalSalesByWeek(int year, int weekNumber);
        public Task<List<LoyalCustomer>> GetTopLoyalCustomer();
    }
}
