using Business_Layer.Repositories;
using Data_Layer.ResourceModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemFoodController : ControllerBase
    {
        public IMenuFoodItemRepository _menuFoodItemRepository;

        public MenuItemFoodController(IMenuFoodItemRepository menuFoodItemRepository)
        {
            _menuFoodItemRepository = menuFoodItemRepository;
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<APIResponseModel> GetAllMenuFoodItem()
        {
            try
            {
                var result = await _menuFoodItemRepository.GetMenuFoodItem();
                return new APIResponseModel()
                {
                    code = 200,
                    message = "Get successful",
                    IsSuccess = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
