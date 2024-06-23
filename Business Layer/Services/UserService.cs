using AutoMapper;
using Business_Layer.Commons;
using Business_Layer.Repositories;
using Business_Layer.Utils;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.Enum;
using Data_Layer.ResourceModel.ViewModel.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class UserService : IUserSerivce
    {
        private readonly IUserRepository _userRepository;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, IClaimsService claimsService, IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _claimsService = claimsService;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<UserViewModel> GetUserById(string id)
        {
            var user = await _userRepository.GetUserByID(id);
            if (user == null)
            {
                throw new Exception("User is not existed !");
            }
            var userViewModel = user.ToUserViewModel();
            return userViewModel;
        }

        public async Task<APIResponseModel> UpdateUser(string id, UserViewModel model)
        {
            var response = new APIResponseModel();
            try {
                var user = await _userRepository.GetUserByID(id);
                if (user == null || user.Status.ToString() != UserEnum.Active.ToString())
                {
                    response.IsSuccess = false;
                    response.message = "Account is not exist";
                    
                }else
                {
                    user.FullName = model.FullName;
                    user.Address = model.Address;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Status = UserEnum.Active.ToString();
                    _userRepository.Update(user);
                    bool isSuccessed = await _userRepository.SaveAsync() > 0;
                    if (!isSuccessed)
                    {
                        response.IsSuccess = false;
                        response.message = "Update fail";

                    }
                    else
                    {
                        response.code = 200;
                        response.IsSuccess = true;
                        response.message = "Update Account Successfully";
                    }
                }
                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = $"Update food fail!, exception {ex.Message}";
            }

            return response;
            
        }
        public async Task<APIResponseModel> DeleteUser (string id)
        {
            var reponse = new APIResponseModel();
            try
            {
                var user = await _userRepository.GetUserByID(id);
                if (user == null)
                {
                    throw new Exception("User is not existed !");
                }
                else if (user.Status.ToString() == UserEnum.IsDeleted.ToString())
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Account is Deleted";
                }
                else
                {
                    user = _userRepository.UpdateStatusUser(user);
                    if (await _userRepository.SaveAsync() > 0)
                    {
                        reponse.Data = user;
                        reponse.IsSuccess = true;
                        reponse.message = "Delete User Succefull";
                    }
                    else
                    {
                        reponse.Data = user;
                        reponse.IsSuccess = false;
                        reponse.message = "Delete User fail!";
                    }
                }
            }catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Delete food fail!, exception {e.Message}";
            }

            return reponse;

        }
        public async Task<Pagination<UserViewModel>> GetUserPagingsionsAsync(int pageIndex = 0, int pageSize = 10)
        {
            var users = await _userRepository.ToPagination(pageIndex, pageSize);
            var result = users.ToUserViewModel();
            return result;
        }

        public async Task<APIResponseModel> GetUsersAsync()
        {
            var reponse = new APIResponseModel();
            List<UserViewModel> userDTOs = new List<UserViewModel>();
            try
            {
                var users = await _userRepository.GetUserAccountAll();

                foreach (var user in users)
                {
                    var mapper = _mapper.Map<UserViewModel>(user);

                    var roles = await _userManager.GetRolesAsync(user);
                    mapper.Role = roles.First();
                    userDTOs.Add(mapper);


                }

                if (userDTOs.Count > 0)
                {
                    reponse.code = 200;
                    reponse.Data = userDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {userDTOs.Count} Accounts";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess=false;
                    reponse.message = $"Have {userDTOs.Count} Accounts";
                    return reponse;
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = e.Message;
                return reponse;
            }
        }
        }
    }

