using Business_Layer.Repositories;
using Data_Layer.ResourceModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<APIResponseModel> GetAllCategories()
        {
            var data = await _repository.GetAllCategory();
            try
            {
                return new APIResponseModel()
                {
                    code = 200,
                    message = "Get All Category successful",
                    IsSuccess = true,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                return new APIResponseModel()
                {
                    code = 400,
                    IsSuccess = false,
                    message = ex.Message,
                };
            }
        }
    }
}
