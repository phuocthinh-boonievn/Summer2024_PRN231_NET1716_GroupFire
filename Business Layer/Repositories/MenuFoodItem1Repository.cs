using Business_Layer.DataAccess;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.Enum;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer.Repositories
{
    public class MenuFoodItem1Repository : GenericRepository<MenuFoodItem>, IMenuFoodItem1Repository
    {
        private readonly FastFoodDeliveryDBContext _dbContext;
        public MenuFoodItem1Repository(FastFoodDeliveryDBContext context, FastFoodDeliveryDBContext dbContext) : base(context)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MenuFoodItem>> GetMenuFoodItemAll()
        {
            var menuFoodItemlists = await _dbContext.MenuFoodItems.Where(x => x.FoodStatus == MenuFoodItemStatusEnum.Active.ToString()).ToListAsync();
            return menuFoodItemlists;
        }
    }
}
