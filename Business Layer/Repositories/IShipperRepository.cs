using Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public interface IShipperRepository : IGenericRepository<Shipper>
    {
        public Task<IEnumerable<Order>> GetAllByStatusAsync(string status);
    }
}
