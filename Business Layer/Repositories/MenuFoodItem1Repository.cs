using Business_Layer.DataAccess;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.Enum;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
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
        public async Task<List<MostSalesFood>> GetTopSalesFood()
        {
            var menuFoodItems = await _dbContext.MenuFoodItems.Include(m => m.Category)
                .Include(m => m.OrderDetails).ToListAsync();
            var topSalesFood = menuFoodItems.Select(food => new MostSalesFood
            {
                FoodName = food.FoodName,
                Category = food.Category.CategoriesName,
                Quantity = food.OrderDetails.Count(order => order.FoodId == food.FoodId)
            })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToList();
            return topSalesFood;
        }
    }
}
