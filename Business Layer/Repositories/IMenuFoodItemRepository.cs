using Data_Layer.ResourceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public interface IMenuFoodItemRepository
    {
        Task<List<MenuFoodItemVM>> GetMenuFoodItem();
        Task<MenuFoodItemVM> GetMenuFoodItemById(Guid Id);
        Task<bool> AddProduct(MenuFoodItemVM menuFoodItemVM);

    }
}
