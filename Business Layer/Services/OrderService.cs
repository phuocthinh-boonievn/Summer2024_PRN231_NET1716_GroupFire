using AutoMapper;
using Business_Layer.Repositories;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    //Pending = 0,
    //Confirmed = 1,
    //Processing = 2,
    //Shipped = 3,
    //Delivered = 4,
    //Cancelled = 5,
    //Returned = 6,
    //Failed = 7
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<APIResponseModel> CancelOrderAsync(Guid id)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(id);

                if (orderChecked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found order, you are sure input";
                }
                else if (orderChecked.StatusOrder == "Cancelled")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Order are cancel, can not cancel order again.";
                }
                else if (orderChecked.StatusOrder == "Confirmed")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Order is confirm, can not cancel order.";
                }
                else
                {
                    
                        orderChecked.StatusOrder = "Cancelled";
                        var orderFofUpdate = _mapper.Map<OrderViewVM>(orderChecked);
                        var orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderFofUpdate);
                        if (await _orderRepository.SaveAsync() > 0)
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = true;
                            reponse.message = "Update order successfully";
                        }
                        else
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = false;
                            reponse.message = "Update order fail!";
                        }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update order fail!, exception {e.Message}";
            }

            return reponse;
        }

    
        public async Task<APIResponseModel> CreateOrderAsync(OrderCreateVM createdto)
        {
            var reponse = new APIResponseModel();

            try
            {
                var orderentity = _mapper.Map<Order>(createdto);
                orderentity.StatusOrder = "Pending";
                await _orderRepository.AddAsync(orderentity);

                if (await _orderRepository.SaveAsync() > 0)
                {
                    reponse.Data = _mapper.Map<OrderViewVM>(orderentity);
                    reponse.IsSuccess = true;
                    reponse.message = "Create new order successfully";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }

            return reponse;
        }

        public async Task<APIResponseModel> GetOrderByIdAsync(Guid orderId)
        {
            var _response = new APIResponseModel();
            try
            {
                var c = await _orderRepository.GetByIdAsync(orderId);
                if (c == null)
                {
                    _response.IsSuccess = false;
                    _response.message = "Don't Have Any Order ";
                }
                else
                {
                    _response.Data = _mapper.Map<OrderViewVM>(c);
                    _response.IsSuccess = true;
                    _response.message = "Order Retrieved Successfully";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.message = ex.Message;
            }

            return _response;
        }

        public async Task<APIResponseModel> GetOrderByUserIDAsync(Guid userId)
        {
            var reponse = new APIResponseModel();
            List<OrderViewVM> OrderDTOs = new List<OrderViewVM>();
            try
            {
                List<Order> orders = (await _orderRepository.GetAllOrderByUserIdAsync(userId.ToString())).ToList();
                foreach (var order in orders)
                {
                    OrderDTOs.Add(_mapper.Map<OrderViewVM>(order));
                }
                if (OrderDTOs.Count > 0)
                {
                    reponse.Data = OrderDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {OrderDTOs.Count} order.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {OrderDTOs.Count} order. Order is null, not found";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = "Exception";
                return reponse;
            }
        }

        public async Task<APIResponseModel> GetOrdersAsync()
        {
            var reponse = new APIResponseModel();
            List<OrderViewVM> OrderDTOs = new List<OrderViewVM>();
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                foreach (var order in orders)
                {
                    OrderDTOs.Add(_mapper.Map<OrderViewVM>(order));
                }
                if (OrderDTOs.Count > 0)
                {
                    reponse.Data = OrderDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {OrderDTOs.Count} order.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {OrderDTOs.Count} order.";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }
        }

        public Task<APIResponseModel> GetSortedOrdersAsync(string sortName)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponseModel> UpdateOrderAsync(Guid id, OrderUpdateVM updatedto)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(id);

                if (orderChecked == null && orderChecked.StatusOrder == "Cancelled")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found order, you are sure input";
                }
                else
                {
                        var orderFofUpdate = _mapper.Map(updatedto, orderChecked);
                        var orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderFofUpdate);
                        if (await _orderRepository.SaveAsync() > 0)
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = true;
                            reponse.message = "Update order successfully";
                        }
                        else
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = false;
                            reponse.message = "Update order fail!";
                        }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update order fail!, exception {e.Message}";
            }

            return reponse;
        }
    }
}
