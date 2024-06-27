using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;

namespace Business_Layer.Repositories
{
    public interface IMenuFoodItem1Repository : IGenericRepository<MenuFoodItem>
    {
        Task<IEnumerable<MenuFoodItem>> GetMenuFoodItemAll();
        Task<List<MostSalesFood>> GetTopSalesFood();
    }
}
