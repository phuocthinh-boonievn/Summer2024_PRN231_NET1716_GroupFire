using Business_Layer.DataAccess;
using Data_Layer.Models;

namespace Business_Layer.Repositories
{
    public class MenuFoodItem1Repository : GenericRepository<MenuFoodItem>, IMenuFoodItem1Repository
    {
        private readonly FastFoodDeliveryDBContext _dbContext;
        public MenuFoodItem1Repository(FastFoodDeliveryDBContext context, FastFoodDeliveryDBContext dbContext) : base(context)
        {
            _dbContext = dbContext;
        }
    }
}
