using AutoMapper;
using Business_Layer.DataAccess;
using Data_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public class ShipperRepository : GenericRepository<Shipper>, IShipperRepository
    {
        private readonly IMapper _mapper;
        private readonly FastFoodDeliveryDBContext _dbContext;
        public ShipperRepository(FastFoodDeliveryDBContext context, FastFoodDeliveryDBContext dbContext) : base(context)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Order>> GetAllByStatusAsync(string status)
        {
            throw new NotImplementedException();
        }
    }
}
