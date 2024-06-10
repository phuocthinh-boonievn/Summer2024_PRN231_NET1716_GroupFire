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

        public async Task<IEnumerable<Shipper>> GetAllByStatusAsync(string status)
        {
            var Shippers = await _dbContext.Shippers.Where(o => o.ShipperStatus.ToLower() == status.ToLower()).ToListAsync();
            if (Shippers.Any() == false)
            {
                throw new Exception("User haven't Order");
            }
            return Shippers;
        }
    }
}
