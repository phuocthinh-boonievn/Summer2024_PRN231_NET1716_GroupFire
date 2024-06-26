using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories.Interfaces
{
    public interface IOrderStatusRepository
    {
        Task<APIResponseModel> CreateOrderStatus(OrderStatus orderStatus);
        Task<APIResponseModel> GetOrderStatusByShipperId(string userId);
        Task<APIResponseModel> ChangeOrderStatus(string orderStatusId);
    }
}
