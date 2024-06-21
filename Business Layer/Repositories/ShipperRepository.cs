using AutoMapper;
using Business_Layer.DataAccess;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly FastFoodDeliveryDBContext _context;
        private readonly IMapper _mapper;
        private UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ShipperRepository(FastFoodDeliveryDBContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<ShipperVM>> GetAllShipper()
        {
            var shippers = await _userManager.GetUsersInRoleAsync("Shipper");
            var shipperList = new List<ShipperVM>();
            foreach (var shipper in shippers)
            {
                var shipperVM = new ShipperVM();
                var orders = _context.Orders.Where(x => x.ShipperId.Equals(shipper.Id)).ToList();
                shipperVM.userId = shipper.Id;
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        shipperVM.orderStatusId.Add(order.OrderId);
                    }
                }
                else shipperVM.orderStatusId = null;
                shipperList.Add(shipperVM);
            }
            var result = _mapper.Map<List<ShipperVM>>(shipperList);
            return result;
        }

        public async Task<APIResponseModel> GetOrderStatusByShipperId(string userId)
        {
            var shippers = await _userManager.GetUsersInRoleAsync("Shipper");
            var user = await _userManager.FindByIdAsync(userId);
            if (!shippers.Contains(user))
            {
                return new APIResponseModel()
                {
                    code = 200,
                    message = "This user is not shipper",
                    IsSuccess = false,
                };
            }
            var orderStatuses = _context.OrderStatuses.Where(o => o.ShipperId.Equals(userId)).ToList();
            if (orderStatuses.Count() == 0) return new APIResponseModel()
            {
                code = 200,
                message = "Shipper doesn't have any order",
                IsSuccess = false,
            };
            var result = _mapper.Map<List<OrderStatus>>(orderStatuses);
            return new APIResponseModel()
            {
                code = 200,
                message = "Get successful",
                IsSuccess = true,
                Data = result
            };
        }

    }
}
