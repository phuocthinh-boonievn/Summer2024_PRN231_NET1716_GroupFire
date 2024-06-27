using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;
using Data_Layer.ResourceModel.ViewModel.OrderStatusVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        public Task<APIResponseModel> CreateOrderStatusAsync(OrderStatusVM orderStatusVM)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponseModel> GetOrderStatus(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponseModel> UpdateCategoryAsync(Guid id, OrderStatusVM orderStatus)
        {
            throw new NotImplementedException();
        }
    }
}
