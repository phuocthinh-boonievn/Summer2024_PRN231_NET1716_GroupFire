using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;

namespace Business_Layer.Services
{
    public interface IOrderService
    {
        Task<APIResponseModel> GetOrdersAsync();
        Task<APIResponseModel> GetOrderByIdAsync(Guid orderId);
        Task<APIResponseModel> GetOrderByUserIDAsync(Guid userId);
        Task<APIResponseModel> CreateOrderAsync(OrderCreateVM createdto);
        Task<APIResponseModel> UpdateOrderAsync(Guid id, OrderUpdateVM updatedto);
        Task<APIResponseModel> CancelOrderAsync(Guid id);
        Task<APIResponseModel> GetSortedOrdersAsync(string sortName);
        Task<APIResponseModel> CheckoutAsync(OrderCreateVM orderdto, List<OrderDetaiCreateVM> orderDetaildto);
    }
}
