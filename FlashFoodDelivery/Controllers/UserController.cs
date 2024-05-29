using Business_Layer.Repositories;
using Business_Layer.Services;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSerivce _userSerivce;

        public UserController(IUserRepository userRepository, IUserSerivce userSerivce)
        {
            _userRepository = userRepository;
            _userSerivce = userSerivce;
        }

        [HttpPost("login")]
        public async Task<APIResponseModel> Login([FromBody] LoginVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return new APIResponseModel
                    {
                        code = 400,
                        Data = errors,
                        IsSuccess = false,
                        message = string.Join(";", errors)
                    };


                }

                var result = await _userRepository.Login(model);
                return result;

            }
            catch (Exception ex)
            {
                return new APIResponseModel()
                {
                    code = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                    Data = ex,
                    IsSuccess = false
                };
            }
        }

        [HttpPost("register")]
        public async Task<APIResponseModel> Register([FromBody] RegisterVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return new APIResponseModel
                    {
                        code = 400,
                        Data = errors,
                        IsSuccess = false,
                        message = string.Join(";", errors)
                    };


                }

                var result = await _userRepository.Register(model);
                return result;

            }
            catch (Exception ex)
            {
                return new APIResponseModel()
                {
                    code = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                    Data = ex,
                    IsSuccess = false
                };
            }
        }
        [HttpGet]
        public async Task<APIResponseModel> GetUserById(Guid id)
        {
            try
            {
                var user = await _userSerivce.GetUserById(id);
                return new APIResponseModel
                {
                    code = 200,
                    IsSuccess = true,
                    Data = user,
                    message = "User Founded !",
                };
            }
            catch(Exception ex) 
            {
                return new APIResponseModel()
                {
                    code = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                    Data = ex,
                    IsSuccess = false
                };
            }
        }
        [HttpPost]
        public async Task <APIResponseModel> UpdateUser(Guid id, [FromBody] UserViewModel model)
        {
            try
            {
                var user = await _userSerivce.UpdateUser(id, model);
                return new APIResponseModel
                {
                    code = 200,
                    IsSuccess = true,
                    Data = user,
                    message = "Update User success !",
                };
            }
            catch (Exception ex)
            {
                return new APIResponseModel()
                {
                    code = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                    Data = ex,
                    IsSuccess = false
                };
            }
        }
        [HttpDelete]
        public async Task<APIResponseModel> DeleteUser(Guid id)
        {
            bool deleteSuccess = await _userSerivce.DeleteUser(id);
            if(!deleteSuccess)
            {
                return new APIResponseModel()
                {
                    code = StatusCodes.Status400BadRequest,
                    message = "Delete User Failed !",
                    IsSuccess = false
                };
            }
            return new APIResponseModel
            {
                code = 200,
                IsSuccess = true,
                message = "Delete User success !",
            };
        }
    }
}
